import type { HuntLoginResponse } from "$lib/dtos/login/huntLoginResponse";
import { writable } from "svelte/store";

export const playingHunt = writable<HuntLoginResponse>();

playingHunt.subscribe((value) => {
    console.log(value);
});
