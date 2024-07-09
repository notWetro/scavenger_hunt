<script lang="ts">
	import HuntCard from './HuntCard.svelte';
	import { onMount } from 'svelte';
	import type { Hunt } from '$lib/models/Hunt';

	let hunts: Hunt[] = [];

	onMount(async () => {
		// TODO: fetch data from API instead of local file
		const response = await fetch('/sampleHunts.json');
		hunts = await response.json();
	});
</script>

<div class="grid p-10 gap-10 grid-cols-3">
	{#each hunts as hunt (hunt.title)}
		<HuntCard {hunt} />
	{/each}
</div>
