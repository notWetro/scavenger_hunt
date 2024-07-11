import { writable } from 'svelte/store';
import type { Hunt } from '$lib/models/Hunt';

// huntStore needed for storing data when creating or editing a hunt, so that the data can be accessed from multiple
// components
export const huntStore = writable<Hunt>();
