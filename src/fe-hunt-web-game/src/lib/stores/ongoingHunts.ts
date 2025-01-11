import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
import { writable } from 'svelte/store';

export const ongoingHunts = writable<HuntLoginResponse[]>([]);

ongoingHunts.subscribe((value) => {
	console.log("123", value);
});
