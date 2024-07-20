<script lang="ts">
	import HuntSelectorCard from '$lib/components/HuntSelectorCard.svelte';
	import { registeredHunts } from '$lib/stores/registeredHunts';
	import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
	import { playingHunt } from '$lib/stores/playingHunt';
	import { goto } from '$app/navigation';
	import { TabItem, Tabs } from 'flowbite-svelte';
	import { PackageCheckIcon, PackageMinusIcon, PackageOpenIcon } from 'lucide-svelte';

	function startGame(hunt: HuntLoginResponse) {
		playingHunt.set(hunt);
		goto('/home/game');
	}
</script>

<Tabs tabStyle="underline" contentClass="p-4 mt-4 h-screen">
	<TabItem open>
		<div slot="title" class="flex items-center gap-2">
			<PackageOpenIcon />
			Ongoing
		</div>

		<h1 class="text-xl font-semibold mb-4 text-center">Ongoing Scavenger-Hunts</h1>

		<div class="flex flex-col gap-4">
			{#each $registeredHunts as hunt}
				<HuntSelectorCard {hunt} on:huntPressed={() => startGame(hunt)} />
			{/each}
		</div>
	</TabItem>
	<TabItem>
		<div slot="title" class="flex items-center gap-2">
			<PackageCheckIcon />
			Complete
		</div>

		<h1 class="text-xl font-semibold mb-4 text-center">Your Completed Scavenger-Hunts</h1>

		<div>No completed Scavenger-Hunts were found.</div>
	</TabItem>
	<TabItem>
		<div slot="title" class="flex items-center gap-2">
			<PackageMinusIcon />
			Expired
		</div>
		<h1 class="text-xl font-semibold mb-4 text-center">Expired Scavenger-Hunts</h1>

		<div>No expired Scavenger-Hunts were found.</div>
	</TabItem>
</Tabs>
