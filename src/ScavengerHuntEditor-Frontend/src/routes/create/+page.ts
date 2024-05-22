import type { Hunt } from '$lib/models/Hunt';
import { writable } from 'svelte/store';

let hunt: Hunt = {
	title: '',
	description: '',
	assignments: []
};

export const _huntStore = writable(hunt);
_huntStore.subscribe((value) => {
	hunt = value;
});
