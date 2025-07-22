import type { Hint } from './Hint';
import type { Solution } from './Solution';

/**
 * Represents an assignment in a hunt.
 */
export interface Assignment {
	/** The unique identifier of the assignment. */
	id: number;
	/** The hint associated with the assignment. */
	hint: Hint;
	/** The solution associated with the assignment. */
	solution: Solution;
}
