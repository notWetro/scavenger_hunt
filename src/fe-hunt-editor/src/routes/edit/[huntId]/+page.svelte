<script lang="ts">
	import type { PageData } from './$types';
	import type { Hunt } from '$lib/models/Hunt';
	import { huntStore } from '$lib/stores/huntStore.js';
	import AddBasicData from '$lib/components/AddBasicData.svelte';
	import { writable } from 'svelte/store';
	import { Button } from 'flowbite-svelte';
	import EditTasks from '$lib/components/EditTasks.svelte';
	import ProgressBar from '$lib/components/Progressbar.svelte';
	import OverviewEdit from '$lib/components/OverviewEdit.svelte';

	export let data: PageData;

	$: huntStore.update(() => {
		return { ...(data.hunt as Hunt) };
	});

	let currentStep = writable(1);

	function advanceToNextStep() {
		currentStep.update((n) => n + 1);
	}
</script>

<ProgressBar {currentStep} maximumSteps={4} />
{#if $currentStep === 1}
	<AddBasicData on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 2}
	<EditTasks on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 3}
	<OverviewEdit on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 4}
	<div class="flex flex-col items-center justify-center">
		<h1 class="text-3xl font-bold mb-5">Schnitzeljagd erfolgreich aktualisiert!</h1>
		<!--        TODO: Check if store is reset here-->
		<Button on:click={() => currentStep.set(1)}>Neue Schnitzeljagd erstellen</Button>
	</div>
{/if}
