<script lang="ts">
	import { ArrowBigLeft, ArrowBigRight } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let isPreviousEnabled: boolean = true;
	export let isNextEnabled: boolean = true;
</script>

<div class="flex flex-row gap-2 justify-center max-w-lg">
	<button
		class="btn btn-primary btn-lg w-full"
		disabled={!isPreviousEnabled}
		on:click={() => dispatch('Previous')}
	>
		<ArrowBigLeft />
		Zurück
	</button>

	<button
		class="btn btn-primary btn-lg w-full disabled"
		disabled={!isNextEnabled}
		on:click={() => dispatch('Next')}
	>
		Weiter
		<ArrowBigRight />
	</button>
</div>
