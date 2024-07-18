import type { HuntLoginResponse } from "$lib/dtos/login/huntLoginResponse";
import { writable } from "svelte/store";

export const registeredHunts = writable<HuntLoginResponse[]>([]);

registeredHunts.subscribe((value) => {
    console.log(value);
});
