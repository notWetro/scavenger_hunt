import type { HuntLoginResponse } from './huntLoginResponse';

/**
 * Represents the response received after a login attempt.
 */
export interface LoginResponse {
	/**
	 * The authentication token.
	 */
	token: string;

	/**
	 * The list of hunts associated with the login.
	 */
	hunts: HuntLoginResponse[];
}
