<script lang="ts">
	// Import necessary components
	import { Alert } from 'flowbite-svelte';

	/**
	 * The hintData prop holds the hint text to be displayed.
	 * @type {string}
	 */
	export let hintData: string;

	/**
	 * The type prop determines the type of hint to be displayed.
	 * @type {number}
	 */
	export let type: number;
</script>

<Alert color="red">
	<p>Unfortunately the submission isn't correct!</p>
	<br />
	{#if type === 1}
		<!-- Type 1 means Text -->
		<p>
			Hint: Your submission is off by {hintData} character(s)! Try again!
		</p>
	{:else if type === 2}
		<!-- Type 2 means Location -->
		<p>Hint: Your location is off by {hintData.split('.').at(0)} meter(s)! Try again!</p>
	{/if}
</Alert>
