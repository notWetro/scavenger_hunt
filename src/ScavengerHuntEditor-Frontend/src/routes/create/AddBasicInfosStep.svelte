<script lang="ts">
	import StepsButtons from './StepsButtons.svelte';
	import { createEventDispatcher } from 'svelte';
	import type { Hunt } from '$lib/models/Hunt';

	const dispatch = createEventDispatcher();

	export let hunt: Hunt;
</script>

<input
	bind:value={hunt.title}
	type="text"
	placeholder="Titel der Schnitzeljagd"
	class="input input-bordered w-full max-w-lg input-lg shadow-xl"
/>
<textarea
	bind:value={hunt.description}
	placeholder="Beschreibung der Schnitzeljagd"
	class="textarea textarea-bordered textarea-lg w-full max-w-lg shadow-xl"
/>

<StepsButtons
	isPreviousEnabled={false}
	isNextEnabled={hunt.title.trim() !== '' && hunt.description.trim() !== ''}
	on:Next={() => dispatch('Next')}
/>
