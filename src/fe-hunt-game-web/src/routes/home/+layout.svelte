<script lang="ts">
	import { onMount } from 'svelte';
	import { token } from '$lib/stores/tokenStore';
	import { Button } from 'flowbite-svelte';
	import { ArrowRightIcon } from 'lucide-svelte';

	let isLoggedIn: boolean = false;

	onMount(() => {
		if ($token !== '') {
			isLoggedIn = true;
		}
	});
</script>

<!-- TODO: THIS IS SHIT CODE AND SHOULD WORK DIFFERENTLY BUT APPARENTLY THIS IS THE WAY???! -->
<div class="flex flex-col justify-center items-center h-screen">
	{#if isLoggedIn}
		<slot />
	{:else}
		<h1 class="text-xl font-bold m-2">Welcome!</h1>

		<p class="text-center m-4">
			In order to participate to open Scavenger-Hunts you need to be logged in!
		</p>
		<Button href="/login">Go to Log in<ArrowRightIcon class="ml-4" /></Button>
	{/if}
</div>
