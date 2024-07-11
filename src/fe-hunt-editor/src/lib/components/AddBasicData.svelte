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

	// update the huntStore with the new title and description
	function updateWithBasicData() {
		huntStore.update((currentHunt) => {
			return { ...currentHunt, title, description };
		});
		dispatch('Finished');
	}
</script>

<div class="flex flex-col">
	<div class="mb-3">
		<Label for="title">Titel der Schnitzeljagd</Label>
		<Input bind:value={title} id="title" type="text" placeholder="Titel eingeben" />
	</div>

	<div class="mb-5">
		<Label for="description">Beschreibung der Schnitzeljagd</Label>
		<Input
			bind:value={description}
			id="description"
			type="text"
			placeholder="Beschreibung eingeben"
		/>
	</div>

	<Button on:click={updateWithBasicData} disabled={!title || !description}>
		Weiter
		<ArrowRight class="ml-2" />
	</Button>
</div>
