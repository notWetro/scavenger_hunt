import type { Assignment } from './Assignment';

/**
 * Represents a hunt with multiple assignments.
 */
export interface Hunt {
	/** The unique identifier of the hunt. */
	id: number;
	/** The title of the hunt. */
	title: string;
	/** The description of the hunt. */
	description: string;
	/** The list of assignments in the hunt. */
	assignments: Assignment[];
}
