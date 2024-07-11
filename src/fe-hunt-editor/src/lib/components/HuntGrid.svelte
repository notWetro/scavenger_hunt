<script lang="ts">
	import HuntCard from './HuntCard.svelte';
	import { onMount } from 'svelte';
	import type { Hunt } from '$lib/models/Hunt';

	import { PUBLIC_API_URL } from '$env/static/public';

	let hunts: Hunt[] = [];

	onMount(async () => {
		const response = await fetch(`${PUBLIC_API_URL}/hunts`);
		hunts = await response.json();
		console.log('received hunts from backend', hunts);
	});
</script>

<div class="grid p-10 gap-5 grid-cols-3">
	{#each hunts as hunt (hunt.id)}
		<HuntCard {hunt} />
	{/each}
</div>
