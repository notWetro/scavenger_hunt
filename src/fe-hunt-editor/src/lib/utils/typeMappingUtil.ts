import type { HintType } from '$lib/models/Hint';
import type { SolutionType } from '$lib/models/Solution';

// Mapping of the solutionType strings to numbers
const reverseSolutionTypeMapping = {
	0: 'QRCode',
	1: 'Text',
	2: 'Location'
};

// Mapping of the hintType strings to numbers
const reverseHintTypeMapping = {
	0: 'Text',
	1: 'Image',
	2: 'Video',
	3: 'Audio'
};

/**
 * Converts a SolutionType to its corresponding text.
 * 
 * @param solutionType - The SolutionType to be converted.
 * @returns The corresponding text for the SolutionType.
 */
export function mapSolutionTypeToText(solutionType: SolutionType): string {
	return reverseSolutionTypeMapping[solutionType];
}

/**
 * Converts a HintType to its corresponding text.
 * 
 * @param hintType - The HintType to be converted.
 * @returns The corresponding text for the HintType.
 */
export function mapHintTypeToText(hintType: HintType): string {
	return reverseHintTypeMapping[hintType];
}
