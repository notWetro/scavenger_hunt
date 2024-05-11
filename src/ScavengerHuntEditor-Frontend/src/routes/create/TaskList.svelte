<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { HintType } from '$lib/models/Hint';
	import type { Hunt } from '$lib/models/Hunt';
	import { SolutionType } from '$lib/models/Solution';
	import { hintTypeToString, solutionTypeToString } from '$lib/utils';
	import { PlusIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let assignments: Assignment[];
	export let selectedAssignment: Assignment;
	$: {
		console.log(selectedAssignment);
	
	}
</script>

<div
	class="overflow-x-auto scroll-m-0 card bg-base-200 justify-items-stretch h-full w-full shadow-xl"
>
	<table class="table">
		<!-- head -->
		<thead>
			<tr>
				<th>Nr</th>
				<th>Hinweis</th>
				<th>Lösung</th>
			</tr>
		</thead>
		<tbody>
			{#each assignments as assignment}
				<tr class={`${assignment == selectedAssignment ? 'bg-base-300' : ''}`} on:click={() => selectedAssignment=assignment} class:selected={selectedAssignment === assignment}>
					<th>{assignments.indexOf(assignment)}</th>
					<td>{hintTypeToString(assignment.hint.hintType)}</td>
					<td>{solutionTypeToString(assignment.solution.solutionType)}</td>
				</tr>
			{/each}
		</tbody>
	</table>

	<button class="btn btn-ghost" on:click={() => {
				const newAssignment = {
							hint: {
						hintType: HintType.Text,
						data: ''
					},
					solution: {
						solutionType: SolutionType.Text,
						data: ''
					}
				};

				assignments = [...assignments, newAssignment];
				selectedAssignment = newAssignment;
				console.log(assignments);
	}}>
		Neue Aufgabe
		<PlusIcon />
	</button>
</div>
