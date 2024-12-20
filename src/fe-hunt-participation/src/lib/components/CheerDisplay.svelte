<script lang="ts">
	import { Button } from 'flowbite-svelte';
	import { Confetti } from 'svelte-confetti';
	import { fade } from 'svelte/transition';
</script>

<div class="flex flex-col justify-center items-center text-center w-full">
	<h3 class="text-primary-600 font-semibold underline-offset-1">Congratulations!</h3>

	<p>You completed the Scavenger Hunt!</p>
	<p>You are the best!</p>

	<Confetti colorArray={['#6D49FC', '#00AAF9', '#939FFC']} amount={150} fallDistance="900px" cone />

	<div class="flex flex-col" transition:fade={{ delay: 2000, duration: 250 }}>
		<Button href="/home">Back to home!</Button>
	</div>
</div>
