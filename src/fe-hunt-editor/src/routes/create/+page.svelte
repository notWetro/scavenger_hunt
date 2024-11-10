<script lang="ts">
	import ProgressBar from '$lib/components/Progressbar.svelte';
	import { writable } from 'svelte/store';
	import AddBasicData from '$lib/components/AddBasicData.svelte';
	import Overview from '$lib/components/Overview.svelte';
	import { Button } from 'flowbite-svelte';
	import { huntStore } from '$lib/stores/huntStore';
	import AssignmentsEditor from '$lib/components/assignments/AssignmentsEditor.svelte';
	import { goto } from '$app/navigation';

	// make sure that huntStore is reset before trying to create a new hunt
	huntStore.set({
		id: 0,
		title: '',
		description: '',
		assignments: []
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
	<AssignmentsEditor on:assignmentsSaved={advanceToNextStep} />
{/if}

{#if $currentStep === 3}
	<Overview on:Finished={advanceToNextStep} />
{/if}

{#if $currentStep === 4}
	<div class="flex flex-col items-center justify-center">
		<h1 class="text-3xl font-bold mb-5">Scavenger hunt successfully created!</h1>
		
		<!-- Container für die Buttons -->
		<div class="flex space-x-4">
			<Button on:click={createNewHunt}>Create a new scavenger hunt</Button>
			<Button on:click={() => goto('/')}>Home 🏠</Button>
		</div>
	</div>
{/if}
