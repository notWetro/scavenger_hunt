<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import type { Hunt } from '$lib/models/Hunt';
	import { hintTypeToString, solutionTypeToString } from '$lib/utils';
	import { PlusIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let assignments: Assignment[];
	$: assignments;
</script>

<div
	class="overflow-x-auto scroll-m-0 card bg-base-100 justify-items-stretch h-full w-full shadow-xl"
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
				<tr class="hover">
					<th>1</th>
					<td>{hintTypeToString(assignment.hint.hintType)}</td>
					<td>{solutionTypeToString(assignment.solution.solutionType)}</td>
				</tr>
			{/each}
		</tbody>
	</table>

	<button class="btn btn-ghost" on:click={() => dispatch('NewAssignment')}>
		Neue Aufgabe
		<PlusIcon />
	</button>
</div>
