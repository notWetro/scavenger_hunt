<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	import { Input, Label, Button } from 'flowbite-svelte';
	import { ArrowRight } from 'lucide-svelte';
	import { huntStore } from '$lib/stores/huntStore';

	// create a dispatcher to emit finished event
	const dispatch = createEventDispatcher();

	// either use the current title and description (for editing) or an empty string (for creating)
	let title = $huntStore.title || '';
	let description = $huntStore.description || '';

	const titleMaxLength = 40;
	const descriptionMaxLength = 100;

	/**
	 * Updates the huntStore with the new title and description, then dispatches the Finished event.
	 */
	function updateWithBasicData() {
		huntStore.update((currentHunt) => {
			return { ...currentHunt, title, description };
		});
		dispatch('Finished');
	}
</script>

<div class="flex flex-col">
	<div class="mb-3">
		<Label for="title">Title of the scavenger hunt</Label>
		<Input 
			bind:value={title} 
			id="title" 
			type="text" 
			placeholder="Enter title" 
			maxlength={titleMaxLength}
		/>
		<p class="text-sm text-gray-500">{title.length} / {titleMaxLength}</p>
	</div>

	<div class="mb-5">
		<Label for="description">Description of the scavenger hunt</Label>
		<Input 
			bind:value={description} 
			id="description" 
			type="text" 
			placeholder="Enter description" 
			maxlength={descriptionMaxLength}
		/>
		<p class="text-sm text-gray-500">{description.length} / {descriptionMaxLength}</p>
	</div>

	<Button on:click={updateWithBasicData} disabled={!title || !description}>
		Next
		<ArrowRight class="ml-2" />
	</Button>
</div>
