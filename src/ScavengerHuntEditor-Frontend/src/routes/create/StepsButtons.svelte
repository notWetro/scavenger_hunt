<script lang="ts">
	import { ArrowBigLeft, ArrowBigRight } from 'lucide-svelte';

	export let counter: number;
	export let isNextEnabled: boolean;

	function onPreviousStep() {
		if (counter > 1) {
			counter -= 1;
		}
	}

	function onNextStep() {
		if (counter <= 3) {
			counter += 1;
		}
	}
</script>

<div class="flex flex-row gap-2 justify-center max-w-lg">
	<button class="btn btn-primary btn-lg w-full" on:click={onPreviousStep}>
		<ArrowBigLeft />
		Zurück
	</button>

	<button
		class="btn btn-primary btn-lg w-full disabled"
		disabled={!isNextEnabled}
		on:click={onNextStep}
	>
		Weiter
		<ArrowBigRight />
	</button>
</div>
