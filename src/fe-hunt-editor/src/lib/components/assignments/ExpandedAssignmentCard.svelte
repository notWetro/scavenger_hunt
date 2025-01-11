<script lang="ts">
	import type { Assignment } from '$lib/models/Assignment';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import { Button, Dropdown, DropdownItem, Fileupload, Input, Label, Helper } from 'flowbite-svelte';
	import { ChevronDown } from 'lucide-svelte';
	import { onMount } from 'svelte';
	import MapSelector from '../MapSelector.svelte';
	import { mapHintTypeToText, mapSolutionTypeToText } from '$lib/utils/typeMappingUtil';
	import { huntStore } from '$lib/stores/huntStore';

	export let assignment: Assignment;
	let hintTypes: string[] = [];
	let solutionTypes: string[] = [];
	let uploadedFileName: string | null = null;

	onMount(() => {
		// Get the string keys of the enums
		hintTypes = Object.keys(HintType).filter((key) => isNaN(Number(key)));
		solutionTypes = Object.keys(SolutionType).filter((key) => isNaN(Number(key)));
	});

	function setHintType(hintType: string) {
		const selectedHintType = HintType[hintType as keyof typeof HintType];
		assignment.hint.hintType = selectedHintType;
		assignment.hint.data = '';
		uploadedFileName = null;
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
			uploadedFileName = file.name;

			const reader = new FileReader();

			reader.onload = (e: ProgressEvent<FileReader>) => {
				const base64String = e.target?.result as string;
				assignment.hint.data = base64String;
			};

			reader.readAsDataURL(file);
		}
	};

	function isBase64(data: string): boolean {
		return data.startsWith('data:image') || data.startsWith('data:video') || data.startsWith('data:audio');
	}

</script>

<div class="border rounded-lg p-2 space-y-2 bg-gray-100">
	<div class="flex items-center justify-between">
		<Button
			>{mapHintTypeToText(assignment.hint.hintType)}<ChevronDown class="w-6 h-6 ms-2" />
		</Button>
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
			<Input bind:value={assignment.hint.additionalData} placeholder="Enter additional hint Text" class="mt-2" />
			<Label class="space-y-2 mb-2">
				<span class="font-semibold">Upload Image</span>
				{#if assignment.hint.data}
					<input id="image" type="file" accept="image/png, image/jpeg" 
					class="hidden" on:change={(e) => onFileSelected(e)} />
					<label
						for="image"
						class="block w-full text-sm text-gray-900 text-left border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 
						focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 px-4 py-2"
						>
						{'Choose a different Image ?'}
					</label>
				{:else}	
					<Fileupload accept="image/png, image/jpeg" on:change={(e) => onFileSelected(e)} />
				{/if}
				<Helper>PNG or JPG</Helper>
			</Label>
		{:else if assignment.hint.hintType === HintType.Video}
			<Input bind:value={assignment.hint.additionalData} placeholder="Enter additional hint Text" class="mt-2" />
   			<Label class="space-y-2 mb-2">
      			<span class="font-semibold">Upload Video</span>
				{#if assignment.hint.data}
						<input id="video" type="file" accept="video/mp4"
						class="hidden" on:change={(e) => onFileSelected(e)} />
						<label
							for="video"
							class="block w-full text-sm text-gray-900 text-left border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 
							focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 px-4 py-2"
							>
							{'Choose a different Video ?'}
						</label>
				{:else}	
      				<Fileupload accept="video/mp4" on:change={(e) => onFileSelected(e)} />
				{/if}
				<Helper>MP4</Helper>
   			</Label>
		{:else if assignment.hint.hintType === HintType.Audio}
    		<Label class="space-y-2 mb-2">
        	<span class="font-semibold">Upload Audio</span>
			{#if assignment.hint.data}
				<input id="audio" type="file" accept="audio/mp3"
				class="hidden" on:change={(e) => onFileSelected(e)} />
				<label
					for="audio"
					class="block w-full text-sm text-gray-900 text-left border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 
					focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 px-4 py-2"
					>
					{'Choose a different Audio ?'}
				</label>
			{:else}
        		<Fileupload accept="audio/mp3" on:change={(e) => onFileSelected(e)} />
			{/if}
			<Helper>MP3</Helper>
    		</Label>
		{/if}
	</div>

	<!-- Solution Selector -->
	<div class="flex items-center justify-between">
		<Button
			>{mapSolutionTypeToText(assignment.solution.solutionType)}<ChevronDown
				class="w-6 h-6 ms-2"
			/>
		</Button>
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
