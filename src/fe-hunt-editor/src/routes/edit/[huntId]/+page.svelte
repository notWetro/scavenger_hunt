<script lang="ts">
	import type { PageData } from './$types';
	import type { Hunt } from '$lib/models/Hunt';
	import { huntStore } from '$lib/stores/huntStore.js';
	import AddBasicData from '$lib/components/AddBasicData.svelte';
	import { writable } from 'svelte/store';
	import { Button } from 'flowbite-svelte';
	import ProgressBar from '$lib/components/Progressbar.svelte';
	import OverviewEdit from '$lib/components/OverviewEdit.svelte';
	import AssignmentsEditor from '$lib/components/assignments/AssignmentsEditor.svelte';

	// needed for huntId from params
	export let data: PageData;

	// trigger update of huntStore with the current hunt data
	$: huntStore.update(() => {
		return { ...(data.hunt as Hunt) };
	});

	let currentStep = writable(1);

	// gets called when the current step is finished and advances to the next step
	function advanceToNextStep() {
		currentStep.update((n) => n + 1);
	}

	// resets the current stored huntData and goes back to the first step (BasicData)
	function createNewHunt() {
		huntStore.set({
			id: 0,
			title: '',
			description: '',
			assignments: []
		});
		currentStep.set(1);
	}
</script>

<ProgressBar {currentStep} maximumSteps={4} />
{#if $currentStep === 1}
	<AddBasicData on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 2}
	<AssignmentsEditor assignments={$huntStore.assignments} on:assignmentsSaved={advanceToNextStep} />
{/if}

{#if $currentStep === 3}
	<OverviewEdit on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 4}
	<div class="flex flex-col items-center justify-center">
		<h1 class="text-3xl font-bold mb-5">Schnitzeljagd erfolgreich aktualisiert!</h1>
		<Button on:click={createNewHunt}>Neue Schnitzeljagd erstellen</Button>
	</div>
{/if}
