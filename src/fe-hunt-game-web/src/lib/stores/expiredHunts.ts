import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
import { writable } from 'svelte/store';

export const expiredHunts = writable<HuntLoginResponse[]>([]);

expiredHunts.subscribe((value) => {
	console.log(value);
});
