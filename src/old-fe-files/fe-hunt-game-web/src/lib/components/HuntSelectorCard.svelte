<script lang="ts">
	import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
	import { Button, Card } from 'flowbite-svelte';
	import { DicesIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	let dispatch = createEventDispatcher();

	function dispatchHunt() {
		dispatch('huntPressed', hunt);
	}

	export let hunt: HuntLoginResponse;
</script>

{#if hunt}
	<Card>
		<div class="flex flex-col items-center gap-4">
			<h3 class="break-words">
				{hunt.title}
			</h3>
			<Button class="place-self-end" on:click={dispatchHunt}>
				<DicesIcon />
			</Button>
		</div>
	</Card>
{/if}
