import { writable } from 'svelte/store';

/**
 * A writable store that holds the token string.
 * 
 * @remarks
 * The token is used for authentication purposes.
 */
export const token = writable('');

token.subscribe((value) => {
	console.log(value);
});
