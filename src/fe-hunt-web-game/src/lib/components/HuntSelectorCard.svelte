<script lang="ts">
	import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
	import { Button, Card } from 'flowbite-svelte';
	import { DicesIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';
	

	let dispatch = createEventDispatcher();
	export let type: 'expired' | 'completed' | 'ongoing';

	function dispatchHunt() {
		dispatch('huntPressed', hunt);
	}

	export let hunt: HuntLoginResponse;
</script>

{#if hunt}
	<Card
		class="
			text-white shadow-lg rounded-xl p-4 hover:shadow-xl transition-shadow duration-300 ease-in-out 
			{type === 'ongoing' ? 'bg-gradient-to-r from-blue-500 to-purple-400' : ''}
			{type === 'completed' ? 'bg-gradient-to-r from-blue-500 to-teal-400' : ''}
			{type === 'expired' ? 'bg-gradient-to-r from-blue-500 to-orange-400' : ''}"
	>
		<div class="flex flex-col items-center gap-4">
			<h3 class="text-2xl font-bold text-center break-words">
				{hunt.title}
			</h3>

			{#if type === 'ongoing'}
				<p class="text-sm text-center text-gray-200">
					Ready to discover the university? Click below to start the hunt!
				</p>

				<Button
					class="bg-white text-purple-500 font-semibold py-2 px-4 rounded-full shadow-md hover:bg-purple-100 
						focus:ring-2 focus:ring-purple-300 transition-all duration-300"
					on:click={dispatchHunt}
				>
					<DicesIcon class="inline-block mr-2" />
					<span>Play Hunt</span>
				</Button>
			{:else if type === 'completed'}
				<p class="text-sm text-center text-gray-300 italic">
					You successfully completed this hunt. Well done!
				</p>
			{:else if type === 'expired'}
				<p class="text-sm text-center text-gray-300 italic">
					This hunt has expired. Try another one!
				</p>
			{/if}
		</div>
	</Card>
{/if}

