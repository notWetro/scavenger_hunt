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

// Function to reverse map the assignments to the original format so that strings are displayed instead of numbers
export function reverseMapAssignments(assignments: any) {
	return assignments.map((assignment: any) => ({
		...assignment,
		solution: {
			...assignment.solution,
			solutionType: reverseSolutionTypeMapping[assignment.solution.solutionType]
		},
		hint: {
			...assignment.hint,
			hintType: reverseHintTypeMapping[assignment.hint.hintType]
		}
	}));
}
