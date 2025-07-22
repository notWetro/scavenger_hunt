import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
import { writable } from 'svelte/store';

/**
 * A writable store that holds an array of ongoing hunts' login responses.
 * 
 * @remarks
 * This store is used to manage the state of all ongoing hunts.
 */
export const ongoingHunts = writable<HuntLoginResponse[]>([]);

ongoingHunts.subscribe((value) => {
	console.log("123", value);
});
