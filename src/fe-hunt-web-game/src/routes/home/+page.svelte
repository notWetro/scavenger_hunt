<script lang="ts">
	import HuntSelectorCard from '$lib/components/HuntSelectorCard.svelte';
	import { ongoingHunts } from '$lib/stores/ongoingHunts';
	import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
	import { playingHunt } from '$lib/stores/playingHunt';
	import { goto } from '$app/navigation';
	import { TabItem, Tabs } from 'flowbite-svelte';
	import { PackageCheckIcon, PackageMinusIcon, PackageOpenIcon } from 'lucide-svelte';
	import { completeHunts } from '$lib/stores/completeHunts';
	import { expiredHunts } from '$lib/stores/expiredHunts';

	function startGame(hunt: HuntLoginResponse) {
		playingHunt.set(hunt);
		goto('/home/game');
	}
</script>

<Tabs tabStyle="underline" class="-m-4" contentClass="p-4 mt-4 h-screen">
	<TabItem open>
		<div slot="title" class="flex items-center gap-2">
			<PackageOpenIcon />
			Ongoing
		</div>

		<h1 class="text-xl font-semibold mb-4 text-center">Ongoing Scavenger-Hunts</h1>

		{#if $ongoingHunts.length <= 0}
			<div>No ongoing Scavenger-Hunts were found.</div>
		{/if}

		<div class="flex flex-col gap-4">
			{#each $ongoingHunts as hunt}
				<HuntSelectorCard {hunt} type="ongoing" on:huntPressed={() => startGame(hunt)} />
			{/each}
		</div>
	</TabItem>

	<TabItem>
		<div slot="title" class="flex items-center gap-2">
			<PackageCheckIcon />
			Complete
		</div>

		<h1 class="text-xl font-semibold mb-4 text-center">Your Completed Scavenger-Hunts</h1>

		{#if $completeHunts.length <= 0}
			<div>No completed Scavenger-Hunts were found.</div>
		{/if}

		<div class="flex flex-col gap-4 pointer-events-none">
			{#each $completeHunts as hunt}
				<HuntSelectorCard {hunt} type="completed" />
			{/each}
		</div>
	</TabItem>

	<TabItem>
		<div slot="title" class="flex items-center gap-2">
			<PackageMinusIcon />
			Expired
		</div>
		<h1 class="text-xl font-semibold mb-4 text-center">Expired Scavenger-Hunts</h1>

		{#if $expiredHunts.length <= 0}
			<div>No expired Scavenger-Hunts were found.</div>
		{/if}
		
		<div class="flex flex-col gap-4 pointer-events-none">
			{#each $expiredHunts as hunt}
				<HuntSelectorCard {hunt} type="expired" />
			{/each}
		</div>
	</TabItem>
</Tabs>
