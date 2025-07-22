<script lang="ts">
	import { Progressbar } from 'flowbite-svelte';
	import type { Writable } from 'svelte/store';

	/** Text descriptions for each step in the progress bar. */
	const stepText: { [key: number]: string } = {
		1: 'Enter Basic data.',
		2: 'Add hints and solutions.',
		3: 'Overview.',
		4: 'Done!'
	};

	/** The current step in the progress bar. */
	export let currentStep: Writable<number>;
	/** The maximum number of steps in the progress bar. */
	export let maximumSteps: number;
</script>

<div class="my-4">
	<div class="mb-1 text-base font-medium text-base-700">
		{stepText[$currentStep]}
	</div>
	<Progressbar progress={($currentStep / maximumSteps) * 100} color="blue" animate />
</div>
