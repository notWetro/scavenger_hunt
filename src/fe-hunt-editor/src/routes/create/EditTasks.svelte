<script lang="ts">
	import { Button, Dropdown, DropdownItem, Fileupload, Input, Label } from 'flowbite-svelte';
	import {
		ChevronDown,
		ChevronUp,
		Plus,
		Trash,
		ArrowDownFromLine,
		ArrowUpFromLine,
		ArrowRight
	} from 'lucide-svelte';
	import type { Assignment } from '$lib/models/Assignment';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import { createEventDispatcher } from 'svelte';
	import { huntStore } from '$lib/stores/huntStore';

	let items: Assignment[] = [
		{
			id: 1,
			hint: { hintType: HintType.Text, data: 'Hint data 1' },
			solution: { solutionType: SolutionType.QRCode, data: 'Solution data 1' }
		},
		{
			id: 2,
			hint: { hintType: HintType.Image, data: 'Hint data 2' },
			solution: { solutionType: SolutionType.Text, data: 'Solution data 2' }
		},
		{
			id: 3,
			hint: { hintType: HintType.Text, data: 'Hint data 3' },
			solution: { solutionType: SolutionType.Location, data: 'Solution data 3' }
		},
		{
			id: 4,
			hint: { hintType: HintType.Text, data: 'Hint data 4' },
			solution: { solutionType: SolutionType.QRCode, data: 'Solution data 4' }
		},
		{
			id: 5,
			hint: { hintType: HintType.Image, data: 'Hint data 5' },
			solution: { solutionType: SolutionType.Location, data: '1313131343;9848349' }
		}
	];

	let expandedItem: number = 0;

	let nextId = items.length + 1;

	let file: File | null;

	// TODO: Implement validation logic
	let isValidData: boolean;

	$: isValidData = true;

	const dispatch = createEventDispatcher();

	function toggleExpand(id: number) {
		expandedItem = expandedItem === id ? 0 : id;
	}

	function moveUp(index: number) {
		if (index > 0) {
			[items[index - 1], items[index]] = [items[index], items[index - 1]];
		}
	}

	function moveDown(index: number) {
		if (index < items.length - 1) {
			[items[index + 1], items[index]] = [items[index], items[index + 1]];
		}
	}

	function deleteItem(id: number) {
		items = items.filter((item) => item.id !== id);
		if (expandedItem === id) {
			expandedItem = 0;
		}
	}

	function addAssignment() {
		const newAssignment: Assignment = {
			id: nextId,
			hint: { hintType: HintType.Text, data: '' },
			solution: { solutionType: SolutionType.Text, data: '' }
		};
		items = [...items, newAssignment];
		nextId++;
	}

	function setHintType(index: number, type: HintType) {
		items[index].hint.hintType = type;
	}

	function setSolutionType(index: number, type: SolutionType) {
		items[index].solution.solutionType = type;
	}

	const onFileSelected = (e: Event) => {
		const input = e.target as HTMLInputElement;
		if (input.files && input.files.length > 0) {
			file = input.files[0];
			const reader = new FileReader();

			reader.onload = (e) => {
				const base64String = e.target.result;
				console.log(base64String); // Use this base64 string as needed
			};

			reader.readAsDataURL(file);
		}
	};

	function saveAssignmentsToStore() {
		huntStore.update((currentHunt) => {
			return { ...currentHunt, assignments: items };
		});

		dispatch('Finished');
	}
</script>

<div class="space-y-4">
	{#each items as item, index (item.id)}
		<div class="border rounded-lg p-2 space-y-2">
			<div class="flex items-center justify-between">
				<div class="flex items-center space-x-2">
					<Button class="p-1 rounded" disabled={index === 0} on:click={() => moveUp(index)}
						><ChevronUp /></Button
					>
					<Button
						class="p-1 rounded"
						disabled={index === items.length - 1}
						on:click={() => moveDown(index)}><ChevronDown /></Button
					>
					<div>{item.hint.hintType}</div>
				</div>
				<div class="flex items-center space-x-2">
					<div>{item.solution.solutionType}</div>
					<button class="p-1 bg-gray-200 rounded" on:click={() => toggleExpand(item.id)}>
						{#if expandedItem === item.id}
							<ArrowUpFromLine />
						{:else}
							<ArrowDownFromLine />
						{/if}
					</button>
				</div>
			</div>
			{#if expandedItem === item.id}
				<div class="border rounded-lg p-2 space-y-2 bg-gray-100">
					<div class="flex items-center justify-between">
						<Button>{item.hint.hintType} <ChevronDown class="w-6 h-6 ms-2" /></Button>
						<Dropdown>
							{#each Object.values(HintType) as hintType}
								<DropdownItem on:click={() => setHintType(index, hintType)}>
									{hintType}
								</DropdownItem>
							{/each}
						</Dropdown>
						<Button class="p-2 bg-red-500" on:click={() => deleteItem(item.id)}><Trash /></Button>
					</div>
					<div>
						{#if item.hint.hintType === HintType.Text}
							<Input bind:value={item.hint.data} placeholder="Enter hint data" class="mt-2" />
						{:else if item.hint.hintType === HintType.Image}
							<Label class="space-y-2 mb-2">
								<span class="font-semibold">Bild hochladen</span>
								<Fileupload
									accept="image/png, image/jpeg"
									on:change={(e) => onFileSelected(e)}
									bind:file
								/>
							</Label>
						{/if}
					</div>
					<div class="flex items-center justify-between">
						<Button>{item.solution.solutionType} <ChevronDown class="w-6 h-6 ms-2" /></Button>
						<Dropdown>
							{#each Object.values(SolutionType) as solutionType}
								<DropdownItem on:click={() => setSolutionType(index, solutionType)}>
									{solutionType}
								</DropdownItem>
							{/each}
						</Dropdown>
					</div>
					<div>
						{#if item.solution.solutionType === SolutionType.QRCode}
							<p>The QR Code will be displayed later for you to print out.</p>
						{:else if item.solution.solutionType === SolutionType.Text}
							<Input
								bind:value={item.solution.data}
								placeholder="Enter solution data"
								class="mt-2"
							/>
						{:else if item.solution.solutionType === SolutionType.Location}
							<div class="flex space-x-2">
								<!--                                TODO: fix displaying of latitude and longitude by splitting data at ";"-->
								<Input bind:value={item.solution.data} placeholder="Latitude" class="mt-2" />
								<Input bind:value={item.solution.data} placeholder="Longitude" class="mt-2" />
							</div>
						{/if}
					</div>
				</div>
			{/if}
		</div>
	{/each}
</div>

<Button class="mt-5" on:click={addAssignment}>
	<Plus class="mr-2" />
	Aufgabe hinzufügen
</Button>

<Button class="mt-5" on:click={saveAssignmentsToStore} disabled={!isValidData}>
	Weiter
	<ArrowRight class="ml-2" />
</Button>
