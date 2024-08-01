<script lang="ts">
	import HuntCard from './HuntCard.svelte';
	import { onMount } from 'svelte';
	import type { Hunt } from '$lib/models/Hunt';

	import { PUBLIC_API_URL } from '$env/static/public';

	let hunts: Hunt[] = [];

	onMount(async () => {
		await reloadHunts();
		console.log('received hunts from backend:', hunts);
	});

	async function reloadHunts() {
		const response = await fetch(`${PUBLIC_API_URL}/hunts`);
		hunts = await response.json();
	}
</script>

<div class="grid gap-x-6 gap-y-10 grid-cols-1 md:grid-cols-2 lg:grid-cols-4">
	{#each hunts as hunt (hunt.id)}
		<HuntCard {hunt} on:HuntDeleted={reloadHunts} />
	{/each}
</div>
