/**
 * Represents the response for a hunt assignment.
 */
export interface HuntAssignmentResponse {
	/**
	 * The type of hint provided.
	 */
	hintType: number;

	/**
	 * The data associated with the hint.
	 */
	hintData: string;

	/**
	 * Any additional data related to the assignment.
	 */
	additionalData: string;

	/**
	 * The type of solution expected.
	 */
	solutionType: number;
}
