<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { HintType } from '$lib/models/Hint';
	import { hintTypeToString } from '$lib/utils';

	export let assignment: Assignment;

	function addImageDataToAssignment(
		event: Event & { currentTarget: EventTarget & HTMLInputElement }
	) {
		assignment.hint.data = 'TODO: ADD BASE64 IMAGE HERE';
		throw new Error('Function not implemented.');
	}

	function addTextDataToAssignment(
		event: Event & { currentTarget: EventTarget & HTMLTextAreaElement }
	) {
		assignment.hint.data = (event.currentTarget as HTMLTextAreaElement)?.value ?? '';
		console.log(assignment.hint.data);
	}
</script>

<div
	class="overflow-x-auto scroll-m-0 card bg-base-200 justify-items-stretch h-full w-full gap-2 p-2 shadow-xl"
>
	<label class="hidden" for="select">Hinweis</label>

	<select bind:value={assignment.hint.hintType} class="select select-bordered select-lg w-full">
		<option disabled selected>Hinweis-Typ</option>
		{#each Object.values(HintType) as type (type)}
			{#if typeof type === 'number'}
				<option value={type}>{hintTypeToString(type)}</option>
			{/if}
		{/each}
	</select>

	<!-- I dont care what you say, I'll do it anyway! -->
	{#if assignment.hint.hintType === HintType.Image}
		<input
			on:input={(event) => addImageDataToAssignment(event)}
			type="file"
			class="file-input file-input-bordered file-input-primary w-full"
		/>
	{/if}

	<!-- I dont care what you say, I'll do it anyway! -->
	{#if assignment.hint.hintType === HintType.Text}
		<textarea
			on:input={(event) => addTextDataToAssignment(event)}
			class="textarea textarea-bordered textarea-lg w-full"
			placeholder="Hinweis hier eingeben"
		></textarea>
	{/if}
</div>
