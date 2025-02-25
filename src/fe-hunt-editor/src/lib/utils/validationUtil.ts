import type { Assignment } from '$lib/models/Assignment';
import { HintType } from '$lib/models/Hint';
import { SolutionType } from '$lib/models/Solution';

/**
 * Checks if the given assignments contain valid data.
 * 
 * @param items - An array of assignments to be validated.
 * @returns A boolean indicating whether all assignments contain valid data.
 */
export function checkValidData(items: Assignment[]): boolean {
	return (
		items.length > 0 &&
		items.every((item) => {
			let isValidHint = false;
			let isValidSolution = false;

			// Validate hint based on its type
			if (item.hint.hintType === HintType.Text) {
				isValidHint = item.hint.data.trim() !== '';
			} else if (item.hint.hintType === HintType.Image) {
				isValidHint = item.hint.data.startsWith('data:image/');
			}
			// New Add Video Validation
			else if(item.hint.hintType === HintType.Video){
				isValidHint = item.hint.data.startsWith('data:video/')
			}
			// New Add Audio Validation
			else if(item.hint.hintType === HintType.Audio){
				isValidHint = item.hint.data.startsWith('data:audio/')
			}

			// Validate solution based on its type
			if (item.solution.solutionType === SolutionType.Text) {
				isValidSolution = item.solution.data.trim() !== '';
			} else if (
				item.solution.solutionType === SolutionType.QRCode ||
				item.solution.solutionType === SolutionType.Location
			) {
				// QRCode does not require validation here
				isValidSolution = true;
			}

			return isValidHint && isValidSolution;
		})
	);
}
