<script lang="ts">
	import StepsButtons from './StepsButtons.svelte';
	import { createEventDispatcher } from 'svelte';
	import TaskList from './TaskList.svelte';
	import HintCreator from './HintCreator.svelte';
	import SolutionCreator from './SolutionCreator.svelte';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import type { Hunt } from '$lib/models/Hunt';
	import type { Assignment } from '$lib/models/Assignment';

	const dispatch = createEventDispatcher();

	export let hunt: Hunt;
	let currentAssignment: Assignment;
	$: {
		console.log(currentAssignment);
	}
</script>

<div class="grid gap-4 grid-cols-5">
	<div class="col-start-1 row-start-1 row-span-2">
		<TaskList
			assignments={hunt.assignments}
			on:NewAssignment={() => {
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

				currentAssignment = newAssignment;
				hunt.assignments = [...hunt.assignments, newAssignment];
				console.log(hunt);
			}}
		/>
	</div>
	<div class="col-start-2 col-span-4 row-start-1">
		{#if currentAssignment}
			<HintCreator {currentAssignment} />
		{/if}
	</div>
	<div class="col-start-2 col-span-4 row-start-2">
		<SolutionCreator />
	</div>
</div>

<StepsButtons
	isNextEnabled={hunt.assignments.length >= 1}
	on:Previous={() => dispatch('Previous')}
	on:Next={() => dispatch('Next')}
/>
