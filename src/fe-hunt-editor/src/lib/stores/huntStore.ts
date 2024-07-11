import { writable } from 'svelte/store';
import type { Hunt } from '$lib/models/Hunt';

export const huntStore = writable<Hunt>();

// Function to load hunt data for editing
export function loadHuntForEditing(huntData: Hunt) {
	huntStore.set(huntData);
}
