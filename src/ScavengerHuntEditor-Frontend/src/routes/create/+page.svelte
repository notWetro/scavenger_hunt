<script lang="ts">
	import StepsBar from './StepsBar.svelte';
	import AddBasicInfosStep from './AddBasicInfosStep.svelte';
	import AddAssignmentsStep from './AddAssignmentsStep.svelte';
	import { ArrowBigRight } from 'lucide-svelte';
	import { postHunt } from '$lib/services/hunt-api';
	import { _huntStore } from './+page';

	let counter: number = 1;

	function submutHunt(): any {
		postHunt($_huntStore).catch(() => console.error('Error-Case not implemented.'));
	}
</script>

<StepsBar bind:counter />

<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch w-full">
	{#if counter === 1}
		<AddBasicInfosStep hunt={$_huntStore} on:Next={() => (counter += 1)} />
	{/if}

	{#if counter === 2}
		<AddAssignmentsStep
			bind:assignments={$_huntStore.assignments}
			on:Previous={() => (counter -= 1)}
			on:Next={() => (counter += 1)}
		/>
	{/if}

	{#if counter === 3}
		<div class="flex flex-row gap-2 justify-center max-w-lg">
			<button class="btn btn-primary btn-lg w-full disabled" on:click={submutHunt}>
				Submit
				<ArrowBigRight />
			</button>
		</div>
	{/if}
</div>
