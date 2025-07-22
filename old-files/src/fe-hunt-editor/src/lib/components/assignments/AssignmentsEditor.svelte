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
	import { onMount } from 'svelte';

	const dispatch = createEventDispatcher();
	let counter: number;
	export let assignments: Assignment[] = [];

	$: isValidData = checkValidData(assignments);

	onMount(() => {
		huntStore.subscribe((hunt) => {
			assignments = [...hunt.assignments];
			counter = assignments.length > 0 ? Math.max(...assignments.map((a) => a.id)) : 0;
		})();
	})

	/**
	 * Creates an empty assignment with default hint and solution types.
	 * @returns A new empty assignment.
	 */
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

	/**
	 * Updates the hunt store with the current assignments.
	 */
	function updateStore() {
		counter = 0;
		huntStore.update((currentHunt) => {
			return { ...currentHunt, assignments: assignments };
		});
	}

	/**
	 * Navigates to the previous step in the hunt creation process.
	 */
	function goBackToPreviousStep() {
		updateStore();
  		dispatch('goBack');
	}

	/**
	 * Adds a new assignment to the list.
	 */
	function addAssignment() {
		assignments = [...assignments, createEmptyAssignment()];
	}

	/**
	 * Saves the current assignments to the store and dispatches an event.
	 */
	function saveAssignmentsToStore(): void {
		updateStore();
		dispatch('assignmentsSaved');
	}

	/**
	 * Moves an assignment up in the list.
	 * @param assignment - The assignment to move up.
	 */
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

	/**
	 * Moves an assignment down in the list.
	 * @param assignment - The assignment to move down.
	 */
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

	/**
	 * Deletes an assignment from the list and updates the IDs of the remaining assignments.
	 * @param assignment - The assignment to delete.
	 */
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
		assignmentsLength={assignments.length}
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
