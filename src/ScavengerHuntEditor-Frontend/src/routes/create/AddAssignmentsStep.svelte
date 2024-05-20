<script lang="ts">
	import StepsButtons from './StepsButtons.svelte';
	import { createEventDispatcher } from 'svelte';
	import TaskList from './TaskList.svelte';
	import HintCreator from './HintCreator.svelte';
	import type { Assignment } from '$lib/models/Assignment';
	import SolutionCreator from './SolutionCreator.svelte';
	import { _huntStore } from './+page';

	const dispatch = createEventDispatcher();

	export let assignments: Assignment[] = [];
	let selectedAssignment: Assignment;
	$: {
		console.log(selectedAssignment);
		console.log(assignments);
	}

	let isNextEnabled: boolean = false;
	$: {
		if (assignments.length >= 1) {
			isNextEnabled = true;
		} else {
			isNextEnabled = false;
		}
	}

	// This function should be called when the assignments are ready to be added to the hunt
	function addAssignmentsToHunt() {
		console.log('Assignments: ', assignments);
		$_huntStore.assignments = [...assignments];
		console.log('Stored: ', $_huntStore.assignments);
	}
</script>

<div class="grid gap-4 grid-cols-5">
	<div class="col-start-1 row-start-1 row-span-2">
		<TaskList {assignments} bind:selectedAssignment />
	</div>
	<div class="col-start-2 col-span-4 row-start-1">
		{#if selectedAssignment}
			<HintCreator bind:assignment={selectedAssignment} />
		{/if}
	</div>
	<div class="col-start-2 col-span-4 row-start-2">
		{#if selectedAssignment}
			<SolutionCreator bind:assignment={selectedAssignment} />
		{/if}
	</div>
</div>

<StepsButtons
	on:Previous={() => dispatch('Previous')}
	on:Next={() => {
		addAssignmentsToHunt();
		dispatch('Next');
	}}
/>
