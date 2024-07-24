<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { Button } from 'flowbite-svelte';
	import SingleAssignmentEditor from './SingleAssignmentEditor.svelte';
	import { ArrowRight, Plus } from 'lucide-svelte';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import { huntStore } from '$lib/stores/huntStore';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let assignments: Assignment[] = [];

	let isValidData = true;

	// This has to be -1 so that the increment in createEmptyAssignment maps the id to the index of the array
	let counter = assignments.length > 0 ? Math.max(...assignments.map((a) => a.id)) : -1;

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

	function addAssignment() {
		assignments = [...assignments, createEmptyAssignment()];
	}

	function saveAssignmentsToStore(): void {
		huntStore.update((currentHunt) => {
			return { ...currentHunt, assignments: assignments };
		});
		dispatch('assignmentsSaved');
	}

	// Function to move an item up in the list
	function moveUp(assignment: Assignment) {
		let index = assignments.findIndex((x) => x.id === assignment.id);

		if (index > 0) {
			[assignments[index - 1], assignments[index]] = [assignments[index], assignments[index - 1]];
		}
	}

	// Function to move an item down in the list
	function moveDown(assignment: Assignment) {
		let index = assignments.findIndex((x) => x.id === assignment.id);

		if (index < assignments.length - 1) {
			[assignments[index + 1], assignments[index]] = [assignments[index], assignments[index + 1]];
		}
	}

	// Function to delete an item from the list
	function deleteAssignment(assignment: Assignment) {
		assignments = assignments.filter((a) => a.id !== assignment.id);
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

<Button class="mt-5" on:click={saveAssignmentsToStore} disabled={!isValidData}>
	Next
	<ArrowRight class="ml-2" />
</Button>
