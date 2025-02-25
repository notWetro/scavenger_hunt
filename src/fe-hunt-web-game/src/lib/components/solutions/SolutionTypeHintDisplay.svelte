<script lang="ts">
	/**
	 * Import necessary components from external libraries.
	 */
	import { Popover } from 'flowbite-svelte';
	import { CircleHelpIcon } from 'lucide-svelte';

	/**
	 * Export a prop 'type' to determine the type of hint to display.
	 */
	export let type: number;
</script>

<!-- Display a help button with a popover containing hint information based on the type -->
<div class="flex items-center p-4 text-sm font-light text-gray-500">
	Need help?
	<button id="b3">
		<CircleHelpIcon class="w-5 h-5 ms-1.5" />
		<span class="sr-only">Show information</span>
	</button>
</div>
<Popover
	triggeredBy="#b3"
	class="w-72 text-sm font-light text-gray-500 bg-white"
	placement="bottom-start"
>
	<div class="p-4">
		{#if type === 0}
			<!-- Type 0 means Qr-Code -->
			QR-Code
		{:else if type === 1}
			<!-- Type 1 means Text -->
			Text
		{:else if type === 2}
			<!-- Type 2 means Location -->
			Location
		{:else}
			<!-- This Type shouldn't exist -->
			No hint could be given.
		{/if}
	</div>
</Popover>
