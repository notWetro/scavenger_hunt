import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
import { writable } from 'svelte/store';

/**
 * A writable store that holds the current playing hunt's login response.
 * 
 * @remarks
 * This store is used to manage the state of the currently playing hunt.
 */
export const playingHunt = writable<HuntLoginResponse>();

playingHunt.subscribe((value) => {
	console.log(value);
});
