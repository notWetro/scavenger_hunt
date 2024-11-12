<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { mapHintTypeToText, mapSolutionTypeToText } from '$lib/utils/typeMappingUtil';
	import { Button } from 'flowbite-svelte';
	import { ArrowDownFromLine, ArrowUpFromLine, ChevronDown, ChevronUp, Trash } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';
	
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

		<!-- New: Add a text box for Hint Type and solution type -->
    <div style="display: flex; gap: 20px; justify-content: flex-end; align-items: right; width: 100%;">
      <div class="box">
        <strong>Hint Type:</strong>
        <p>{mapHintTypeToText(assignment.hint.hintType)}</p>
      </div>
      <div class="box">
        <strong>Solution Type:</strong>
        <p>{mapSolutionTypeToText(assignment.solution.solutionType)}</p>
      </div>
    </div>
  </div>

  <!-- Expand Button and Delete Button -->
  <div class="flex items-center space-x-2">
    <button class="p-1 bg-gray-200 rounded" on:click={() => dispatch('toggleExpanded')}>
      {#if isExpanded}
        <ArrowUpFromLine />
      {:else}
        <ArrowDownFromLine />
      {/if}
    </button>
    <Button class="p-2 bg-red-500" on:click={() => dispatch('deleteAssignment')}>
      <Trash />
    </Button>
  </div>
</div>


<!-- New: Style for the Solution and Hint Type box -->
<style>
  /* Styling für das Box-Layout */
  .box {
    padding: 2px;
    border: 1px solid #3498db; /* Blaues Border */
    border-radius: 5px;
    background-color: #f0f8ff; /* Hellblauer Hintergrund */
    width: 200px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
  }

  .box p {
    margin: 0;
    font-size: 1rem;
    color: #333;
  }

  strong {
    font-size: 1.1rem;
    color: #3498db; /* Farbe für den Text "Hint Type" und "Solution Type" */
    margin-bottom: 1px;
  }
</style>