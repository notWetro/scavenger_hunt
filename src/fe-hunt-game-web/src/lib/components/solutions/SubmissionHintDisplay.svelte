<script lang="ts">
	import { Alert } from 'flowbite-svelte';

	export let hintData: string;
	export let type: number;
</script>

<Alert color="red">
	<p>Unfortunately the submission isn't correct!</p>
	<br />
	{#if type === 1}
		<!-- Type 1 means Text -->
		<p>Hint: Your submission is off by {hintData} characters! Try again!</p>
	{:else if type === 2}
		<!-- Type 2 means Location -->
		<p>Hint: Your location is off by {hintData.split('.').at(0)} meters! Try again!</p>
	{/if}
</Alert>
