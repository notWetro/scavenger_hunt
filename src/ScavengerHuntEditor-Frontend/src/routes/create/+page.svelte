<script lang="ts">
	import { writable } from 'svelte/store';
	import type { Hunt } from '$lib/models/Hunt'; // assuming Hunt interface is defined in types.ts

	import StepsBar from './StepsBar.svelte';
	import AddBasicInfosStep from './AddBasicInfosStep.svelte';
	import AddAssignmentsStep from './AddAssignmentsStep.svelte';

	let hunt: Hunt = {
		title: '',
		description: '',
		assignments: []
	};

	const huntStore = writable(hunt);

	huntStore.subscribe((value) => {
		hunt = value;
	});

	let counter: number = 1;
</script>

<StepsBar bind:counter />

<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch w-full">
	{#if counter === 1}
		<AddBasicInfosStep {hunt} on:Next={() => (counter += 1)} />
	{/if}

	{#if counter === 2}
		<AddAssignmentsStep bind:assignments={hunt.assignments} on:Previous={() => (counter -= 1)} on:Next={() => (counter += 1)} />
	{/if}
</div>
