import { writable } from 'svelte/store';
import type { Hunt } from '$lib/models/Hunt';

export const huntStore = writable<Hunt>();
