<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import { Button, Dropdown, DropdownItem, Fileupload, Input, Label } from 'flowbite-svelte';
	import { ChevronDown } from 'lucide-svelte';
	import { onMount } from 'svelte';
	import MapSelector from '../MapSelector.svelte';
	import { mapHintTypeToText, mapSolutionTypeToText } from '$lib/utils/typeMappingUtil';

	export let assignment: Assignment;
	let hintTypes: string[] = [];
	let solutionTypes: string[] = [];

	onMount(() => {
		// Get the string keys of the enums
		hintTypes = Object.keys(HintType).filter((key) => isNaN(Number(key)));
		solutionTypes = Object.keys(SolutionType).filter((key) => isNaN(Number(key)));
	});

	function setHintType(hintType: string) {
		const selectedHintType = HintType[hintType as keyof typeof HintType];
		assignment.hint.hintType = selectedHintType;
		assignment.hint.data = '';
	}

	function setSolutionType(solutionType: string) {
		const selectedSolutionType = SolutionType[solutionType as keyof typeof SolutionType];
		assignment.solution.solutionType = selectedSolutionType;
		assignment.solution.data = '';
	}

	const onFileSelected = (e: Event) => {
		const input = e.target as HTMLInputElement;
		if (input.files && input.files.length > 0) {
			const file = input.files[0];
			const reader = new FileReader();

			reader.onload = (e: ProgressEvent<FileReader>) => {
				const base64String = e.target?.result as string;
				assignment.hint.data = base64String;
			};

			reader.readAsDataURL(file);
		}
	};
</script>

<div class="border rounded-lg p-2 space-y-2 bg-gray-100">
	<div class="flex items-center justify-between">
		<Button
			>{mapHintTypeToText(assignment.hint.hintType)}<ChevronDown class="w-6 h-6 ms-2" /></Button
		>
		<Dropdown>
			{#each hintTypes as hintType}
				<DropdownItem on:click={() => setHintType(hintType)}>
					{hintType}
				</DropdownItem>
			{/each}
		</Dropdown>
	</div>

	<!-- Hint-Selector -->
	<div>
		{#if assignment.hint.hintType === HintType.Text}
			<Input bind:value={assignment.hint.data} placeholder="Enter hint data" class="mt-2" />
		{:else if assignment.hint.hintType === HintType.Image}
			<Label class="space-y-2 mb-2">
				<span class="font-semibold">Upload Image</span>
				<Fileupload accept="image/png, image/jpeg" on:change={(e) => onFileSelected(e)} />
			</Label>
		{:else if assignment.hint.hintType === HintType.Video}
   			<Label class="space-y-2 mb-2">
      			<span class="font-semibold">Upload Video</span>
      			<Fileupload accept="video/mp4" on:change={(e) => onFileSelected(e)} />
   			</Label>
		{/if}
	</div>

	<!-- Solution Selector -->
	<div class="flex items-center justify-between">
		<Button
			>{mapSolutionTypeToText(assignment.solution.solutionType)}<ChevronDown
				class="w-6 h-6 ms-2"
			/></Button
		>
		<Dropdown>
			{#each solutionTypes as solutionType}
				<DropdownItem on:click={() => setSolutionType(solutionType)}>
					{solutionType}
				</DropdownItem>
			{/each}
		</Dropdown>
	</div>
	<div>
		{#if assignment.solution.solutionType === SolutionType.QRCode}
			<p>The QR Code will be displayed later for you to print out.</p>
		{:else if assignment.solution.solutionType === SolutionType.Text}
			<Input bind:value={assignment.solution.data} placeholder="Enter solution data" class="mt-2" />
		{:else if assignment.solution.solutionType === SolutionType.Location}
			<MapSelector bind:textValue={assignment.solution.data} />
		{/if}
	</div>
</div>
