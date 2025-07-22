import { writable } from 'svelte/store';
import type { Hunt } from '$lib/models/Hunt';

/**
 * A Svelte store that holds the data of a hunt, making it accessible across multiple components.
 */
export const huntStore = writable<Hunt>();
