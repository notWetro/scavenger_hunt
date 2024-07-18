<script lang="ts">
	import { onMount } from 'svelte';
	import { token } from '$lib/stores/tokenStore';
	import { Button } from 'flowbite-svelte';

	let isLoggedIn: boolean = false;

	onMount(() => {
		const savedToken = $token;
		if (savedToken !== '') {
			isLoggedIn = true;
		}
	});
</script>

<!-- TODO: THIS IS SHIT CODE AND SHOULD WORK DIFFERENTLY BUT APPARENTLY THIS IS THE WAY???! -->

<div
	class={`flex flex-col justify-center items-center h-screen ${isLoggedIn ? 'visible' : 'hidden'}`}
>
	<slot />
</div>

{#if !isLoggedIn}
	<div class="flex flex-col justify-center items-center h-screen">
		<span>Bitte melde dich erst an, bevor du an einer Schnitzeljagd teilnehmen willst.</span>
		<Button href="/login">Zur Anmeldung</Button>
	</div>
{/if}
