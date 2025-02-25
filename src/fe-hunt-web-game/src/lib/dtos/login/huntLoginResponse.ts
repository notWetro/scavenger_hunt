/**
 * Represents the response for a hunt after a login attempt.
 */
export interface HuntLoginResponse {
	/**
	 * The unique identifier of the hunt.
	 */
	id: number;

	/**
	 * The title of the hunt.
	 */
	title: string;

	/**
	 * The participation status of the hunt.
	 */
	participationStatus: number;
}
