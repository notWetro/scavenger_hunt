<script lang="ts">
	/**
	 * This component represents an assignment card that can be expanded or collapsed.
	 * It uses the CollapsedAssignmentCard and ExpandedAssignmentCard components.
	 */

	import type { Assignment } from '$lib/models/Assignment';
	import { createEventDispatcher } from 'svelte';
	import CollapsedAssignmentCard from './CollapsedAssignmentCard.svelte';
	import ExpandedAssignmentCard from './ExpandedAssignmentCard.svelte';

	const dispatch = createEventDispatcher();

	export let assignment: Assignment;
	let isExpanded = false;
	export let assignmentsLength: number;

	/**
	 * Toggles the expanded state of the assignment card.
	 */
	function toggleExpanded() {
		isExpanded = !isExpanded;
	}
</script>

<div class="border rounded-lg p-2 space-y-2">
	<CollapsedAssignmentCard
		bind:assignment
		bind:isExpanded
		assignmentsLength={assignmentsLength}
		on:toggleExpanded={toggleExpanded}
		on:moveDown={() => dispatch('moveDown')}
		on:moveUp={() => dispatch('moveUp')}
		on:deleteAssignment={() => dispatch('deleteAssignment')}
	/>
	{#if isExpanded}
		<ExpandedAssignmentCard bind:assignment />
	{/if}
</div>
