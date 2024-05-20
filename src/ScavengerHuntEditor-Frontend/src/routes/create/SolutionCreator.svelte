<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { SolutionType } from '$lib/models/Solution';
	import { solutionTypeToString } from '$lib/utils';
	import { ChevronsLeftRightIcon, ChevronsUpDownIcon } from 'lucide-svelte';

	export let assignment: Assignment;
</script>

<div
	class="overflow-x-auto scroll-m-0 card bg-base-200 justify-items-stretch h-full w-full gap-2 p-2 shadow-xl"
>
	<label class="hidden" for="select">Lösung</label>

	<select
		bind:value={assignment.solution.solutionType}
		class="select select-bordered select-lg w-full"
	>
		<option disabled selected>Lösungs-Typ</option>
		{#each Object.values(SolutionType) as type (type)}
			{#if typeof type === 'number'}
				<option value={type}>{solutionTypeToString(type)}</option>
			{/if}
		{/each}
	</select>

	{#if assignment.solution.solutionType === SolutionType.Text}
		<textarea
			class="textarea textarea-bordered textarea-lg w-full"
			placeholder="Gewünschte Lösung hier eingeben"
		></textarea>
	{/if}

	{#if assignment.solution.solutionType === SolutionType.Location}
		<label class="input input-bordered flex items-center gap-2">
			<ChevronsLeftRightIcon />
			<input type="number" class="grow" placeholder="Latitude" />
		</label>

		<label class="input input-bordered flex items-center gap-2">
			<ChevronsUpDownIcon />
			<input type="number" class="grow" placeholder="Longitude" />
		</label>
	{/if}

	{#if assignment.solution.solutionType === SolutionType.QRCode}
		<div class="flex items-center justify-center flex-col">
			<strong>WICHTIG!</strong>
			<p>Ein Qr-Code wird beim Erstellen der Aufgabe generiert und gespeichert.</p>
			<p>
				Du solltest den QR-Code anschließend ausdrucken und an der hingewiesenen Stelle verstecken.
			</p>
		</div>
	{/if}
</div>
