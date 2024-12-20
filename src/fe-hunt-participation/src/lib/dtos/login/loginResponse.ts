import type { HuntLoginResponse } from './huntLoginResponse';

export interface LoginResponse {
	token: string;
	hunts: HuntLoginResponse[];
}
