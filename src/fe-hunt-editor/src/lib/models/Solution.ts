/**
 * Represents a solution for an assignment.
 */
export interface Solution {
	/** The type of the solution. */
	solutionType: SolutionType;
	/** The data associated with the solution. */
	data: string;
}

/**
 * Enum for the different types of solutions.
 */
export enum SolutionType {
	QRCode,
	Text,
	Location
}
