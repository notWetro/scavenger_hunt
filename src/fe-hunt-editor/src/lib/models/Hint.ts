/**
 * Represents a hint for an assignment.
 */
export interface Hint {
	/** The type of the hint. */
	hintType: HintType;
	/** The data associated with the hint. */
	data: string;
	/** Optional additional data for the hint. */
	additionalData?: string;
}

/**
 * Enum for the different types of hints.
 */
export enum HintType {
	Text,
	Image,
	Video,
	Audio
}
