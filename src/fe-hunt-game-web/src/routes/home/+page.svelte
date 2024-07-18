<script lang="ts">
	import HuntSelectorCard from '$lib/components/HuntSelectorCard.svelte';
	import { registeredHunts } from '$lib/stores/registeredHunts';
	import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
	import { playingHunt } from '$lib/stores/playingHunt';
	import { goto } from '$app/navigation';

	function startGame(hunt: HuntLoginResponse) {
		playingHunt.set(hunt);
		goto('/home/game');
	}
</script>

<div class="flex flex-col w-full items-center gap-4">
	<hr class="border-solid border-2 rounded-sm" />

	<h1 class="text-xl font-semibold">Angemeldete Schnitzeljagden</h1>

	<div class="flex flex-col gap-4">
		{#each $registeredHunts as hunt}
			<HuntSelectorCard {hunt} on:huntPressed={() => startGame(hunt)} />
		{/each}
	</div>

	<hr class="border-solid border-2 w-full rounded-sm" />

	<h1 class="text-xl font-semibold">Erledigte Schnitzeljagden</h1>

	<div>Es wurden keine erledigten Schnitzeljagden gefunden</div>

	<hr class="border-solid border-2 w-full rounded-sm" />

	<h1 class="text-xl font-semibold">Vergangene Schnitzeljagden</h1>

	<div>Es wurden keine vergangenen Schnitzeljagden gefunden</div>

	<hr class="border-solid border-2 w-full rounded-sm" />
</div>
