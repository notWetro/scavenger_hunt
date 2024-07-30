import type { HuntLoginResponse } from "$lib/dtos/login/huntLoginResponse";
import { writable } from "svelte/store";

export const completeHunts = writable<HuntLoginResponse[]>([]);

completeHunts.subscribe((value) => {
    console.log(value);
});