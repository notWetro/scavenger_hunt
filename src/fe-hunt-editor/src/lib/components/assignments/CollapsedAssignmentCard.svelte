<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { mapHintTypeToText, mapSolutionTypeToText } from '$lib/utils/typeMappingUtil';
	import { Button } from 'flowbite-svelte';
	import { ArrowDownFromLine, ArrowUpFromLine, ChevronDown, ChevronUp, Trash } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';
	import { assignmentCount } from '$lib/stores/assignmentStore';
	
	const dispatch = createEventDispatcher();

	export let assignment: Assignment;
	export let isExpanded: boolean;
	isExpanded = true;
</script>

<div class="flex items-center justify-between">
	<div class="flex items-center space-x-2">
		<!-- TODO: For both Buttons: Add a disabled check if it's the first or last assignment -->
		<Button class="p-1 rounded" on:click={() => dispatch('moveUp')}><ChevronUp /></Button>
		<Button class="p-1 rounded" on:click={() => dispatch('moveDown')}><ChevronDown /></Button>

		<!-- TODO : Display Assingment Count Hint Type and Solution Type
		<div style="display: flex; gap: 20px;">
			<p> Assignment {$assignmentCount}</p>
  			Hint Type =<p>{mapHintTypeToText(assignment.hint.hintType)}</p>
  			Solution Type =<p>{mapSolutionTypeToText(assignment.solution.solutionType)}</p>
		</div>
		-->

	</div>
	<div class="flex items-center space-x-2">
		<button class="p-1 bg-gray-200 rounded" on:click={() => dispatch('toggleExpanded')}>
			{#if isExpanded}
				<ArrowUpFromLine />
			{:else}
				<ArrowDownFromLine />
			{/if}
		</button>
		<Button class="p-2 bg-red-500" on:click={() => dispatch('deleteAssignment')}><Trash /></Button>
	</div>
</div>