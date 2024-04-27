<script lang="ts">
	import { ArrowBigRight, ArrowBigLeft } from 'lucide-svelte';
	import StepsBar from './StepsBar.svelte';
	import StepsButtons from './StepsButtons.svelte';
	import TaskList from './TaskList.svelte';
	import HintCreator from './HintCreator.svelte';
	import SolutionCreator from './SolutionCreator.svelte';
	let counter: number = 1;
</script>

<StepsBar bind:counter />

<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch w-full">

	{#if counter == 1}
	<input type="text" placeholder="Titel der Schnitzeljagd" class="input input-bordered w-full max-w-lg input-lg shadow-xl" />
	<textarea placeholder="Beschreibung der Schnitzeljagd" class="textarea textarea-bordered textarea-lg w-full max-w-lg shadow-xl" ></textarea>
	{/if}

	{#if counter === 2}
	<div class="grid gap-4 grid-cols-3">
		<div class="col-start-1 row-start-1 row-span-2">
			<TaskList />
		</div>
		<div class="col-start-2 col-span-2 row-start-1">
			<HintCreator />
		</div>
		<div class="col-start-2 col-span-2 row-start-2">
			<SolutionCreator />
		</div>
	</div>
	{/if}

	<StepsButtons bind:counter />

</div>