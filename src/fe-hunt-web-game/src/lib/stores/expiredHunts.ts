import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
import { writable } from 'svelte/store';

/**
 * A writable store that holds an array of expired hunts' login responses.
 * 
 * @remarks
 * This store is used to manage the state of all expired hunts.
 */
export const expiredHunts = writable<HuntLoginResponse[]>([]);

expiredHunts.subscribe((value) => {
	console.log(value);
});
