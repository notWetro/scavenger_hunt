import type { HintType } from '$lib/models/Hint';
import type { SolutionType } from '$lib/models/Solution';

// Mapping of the solutionType and hintType strings to numbers
const reverseSolutionTypeMapping = {
	0: 'QRCode',
	1: 'Text',
	2: 'Location'
};

// Mapping of the solutionType and hintType strings to numbers
const reverseHintTypeMapping = {
	0: 'Text',
	1: 'Image'
};

export function mapSolutionTypeToText(solutionType: SolutionType): string {
	return reverseSolutionTypeMapping[solutionType];
}

export function mapHintTypeToText(hintType: HintType): string {
	return reverseHintTypeMapping[hintType];
}
