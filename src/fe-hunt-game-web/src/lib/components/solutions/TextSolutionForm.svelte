<script lang="ts">
	import { Button, Card, Input } from 'flowbite-svelte';
	import { TextCursorInputIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let data: string;

	async function submitSolution() {
		dispatch('SubmitData');
	}
</script>

<Card class="p-4 mb-4">
	<div class="flex flex-row items-center text-center">
		<TextCursorInputIcon class="w-8 h-8 mb-2 mr-2 text-primary-800" />
		<h5
			class="mb-2 text-xl font-semibold underline underline-offset-4 tracking-tight text-primary-800 dark:text-white"
		>
			Submit Solution
		</h5>
	</div>

	<p class="p-4">Submit your answer when ready.</p>
	<Input class="mb-2" placeholder="your answer here" bind:value={data} />
	<Button on:click={submitSolution}>Submit</Button>
</Card>
