<script lang="ts">
	import ProgressBar from '$lib/components/Progressbar.svelte';
	import { writable } from 'svelte/store';
	import AddBasicData from '$lib/components/AddBasicData.svelte';
	import EditTasks from '$lib/components/EditTasks.svelte';
	import Overview from '$lib/components/Overview.svelte';
	import { Button } from 'flowbite-svelte';
	import { huntStore } from '$lib/stores/huntStore';

	// make sure that huntStore is reset before trying to create a new hunt
	huntStore.set({
		id: 0,
		title: '',
		description: '',
		assignments: []
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
	<Overview on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 4}
	<div class="flex flex-col items-center justify-center">
		<h1 class="text-3xl font-bold mb-5">Schnitzeljagd erfolgreich erstellt!</h1>
		<!--        TODO: Check if store is reset here-->
		<Button on:click={() => currentStep.set(1)}>Neue Schnitzeljagd erstellen</Button>
	</div>
{/if}
