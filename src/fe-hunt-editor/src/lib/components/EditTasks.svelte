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

	// Mapping of the solutionType and hintType strings to numbers
	const reverseSolutionTypeMapping = {
		0: 'QRCode',
		1: 'Text',
		2: 'Location'
	};

	// Mapping of the solutionType and hintType strings to numbers
	const reverseHintTypeMapping = {
		0: 'Text',
		1: 'Image'
	};

	// Function to reverse map the assignments to the original format so that strings are displayed instead of numbers
	function reverseMapAssignments(assignments) {
		return assignments.map((assignment) => ({
			...assignment,
			solution: {
				...assignment.solution,
				solutionType: reverseSolutionTypeMapping[assignment.solution.solutionType]
			},
			hint: {
				...assignment.hint,
				hintType: reverseHintTypeMapping[assignment.hint.hintType]
			}
		}));
	}
	let updatedHuntStoreWithNames = reverseMapAssignments($huntStore.assignments);
	// either use the current assignments (editing) or an empty array (creating)
	let items: Assignment[] = updatedHuntStoreWithNames || [];

	let expandedItem: number = 0;

	let nextId = items.length + 1;

	let file: File | null;

	// TODO: Implement validation logic
	let isValidData: boolean;

	$: isValidData = true;

	const dispatch = createEventDispatcher();

	// Function to toggle the expanded state of an item in the list
	function toggleExpand(id: number) {
		expandedItem = expandedItem === id ? 0 : id;
	}

	// Function to move an item up in the list
	function moveUp(index: number) {
		if (index > 0) {
			[items[index - 1], items[index]] = [items[index], items[index - 1]];
		}
	}

	// Function to move an item down in the list
	function moveDown(index: number) {
		if (index < items.length - 1) {
			[items[index + 1], items[index]] = [items[index], items[index + 1]];
		}
	}

	// Function to delete an item from the list
	function deleteItem(id: number) {
		items = items.filter((item) => item.id !== id);
		if (expandedItem === id) {
			expandedItem = 0;
		}
	}

	// Function to add a new assignment to the list
	function addAssignment() {
		const newAssignment: Assignment = {
			id: nextId,
			hint: { hintType: HintType.Text, data: '' },
			solution: { solutionType: SolutionType.Text, data: '' }
		};
		items = [...items, newAssignment];
		nextId++;
	}

	// Function to set the hint type of an item
	function setHintType(index: number, type: HintType) {
		items[index].hint.hintType = type;
	}

	// Function to set the solution type of an item
	function setSolutionType(index: number, type: SolutionType) {
		items[index].solution.solutionType = type;
	}

	// Function to handle the file upload
	// TODO: write base64 string to hint.data
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
								<!--  TODO: fix displaying of latitude and longitude by splitting data at ";"-->
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
