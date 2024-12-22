import { writable } from 'svelte/store';

export const token = writable('');

token.subscribe((value) => {
	console.log(value);
});
