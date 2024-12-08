<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { Button } from 'flowbite-svelte';
	import SingleAssignmentEditor from './SingleAssignmentEditor.svelte';
	import { ArrowRight, Plus , ArrowLeft } from 'lucide-svelte';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import { huntStore } from '$lib/stores/huntStore';
	import { createEventDispatcher } from 'svelte';
	import { checkValidData } from '$lib/utils/validationUtil';

	const dispatch = createEventDispatcher();

	export let assignments: Assignment[] = [];

	$: isValidData = checkValidData(assignments);

	// This has to be 0 so that the increment in createEmptyAssignment maps the id to the index of the array
	let counter = assignments.length > 0 ? Math.max(...assignments.map((a) => a.id)) : 0;

	function createEmptyAssignment(): Assignment {
		return {
			id: ++counter,
			hint: {
				hintType: HintType.Text,
				data: ''
			},
			solution: {
				solutionType: SolutionType.Text,
				data: ''
			}
		};
	}

	// New: Function to go to the previous step on the hunt creation 
	function goBackToPreviousStep() {
  		dispatch('goBack');
	}

	function addAssignment() {
		assignments = [...assignments, createEmptyAssignment()];
	}

	function saveAssignmentsToStore(): void {
		counter = 0;
		huntStore.update((currentHunt) => {
			return { ...currentHunt, assignments: assignments };
		});
		dispatch('assignmentsSaved');
	}

	// Function to move an item up in the list
	function moveUp(assignment: Assignment) {
		let index = assignments.findIndex((x) => x.id === assignment.id);
		let dummy = 0;
		if (index > 0) {

			[assignments[index - 1], assignments[index]] = [assignments[index], assignments[index - 1]];
			dummy = assignments[index - 1].id;	
			assignments[index - 1].id = assignments[index].id;
			assignments[index].id = dummy;
		}
	}

	// Function to move an item down in the list
	function moveDown(assignment: Assignment) {
		let index = assignments.findIndex((x) => x.id === assignment.id);
		let dummy = 0;

		if (index < assignments.length - 1) {
			[assignments[index + 1], assignments[index]] = [assignments[index], assignments[index + 1]];
			dummy = assignments[index + 1].id;	
			assignments[index + 1].id = assignments[index].id;
			assignments[index].id = dummy;
		}
	}

	// Function to delete an item from the list 
	// New: Reduces the assignment count and updates the ID of the following assignments
	function deleteAssignment(assignment: Assignment) {
		let index = assignments.findIndex((x) => x.id === assignment.id);
		assignments = assignments.filter((a) => a.id !== assignment.id);

    	for (let i = index; i < assignments.length; i++) {
        	assignments[i].id -= 1;
    	}
    	counter -= 1;
    	assignments = [...assignments];
	}
</script>

{#each assignments as assignment}
	<SingleAssignmentEditor
		bind:assignment
		on:moveDown={() => moveDown(assignment)}
		on:moveUp={() => moveUp(assignment)}
		on:deleteAssignment={() => deleteAssignment(assignment)}
	/>
{/each}

<Button class="mt-5" on:click={addAssignment}>
	<Plus class="mr-2" />
	Add assignment
</Button>

<!-- New: Adds two buttons to the create Hunt editor -->
<div style="display: flex; gap: 10px; justify-content: center; width: 100%;">
	<Button class="mt-5" on:click={goBackToPreviousStep} style="flex: 1;">
		Previous
		<ArrowLeft class="ml-2" />
	</Button>
	<Button class="mt-5" on:click={saveAssignmentsToStore} disabled={!isValidData} style="flex: 1;">
		Next
		<ArrowRight class="ml-2" />
	</Button>
</div>
