import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
import { writable } from 'svelte/store';

/**
 * A writable store that holds an array of completed hunts' login responses.
 * 
 * @remarks
 * This store is used to manage the state of all completed hunts.
 */
export const completeHunts = writable<HuntLoginResponse[]>([]);

completeHunts.subscribe((value) => {
	console.log(value);
});
