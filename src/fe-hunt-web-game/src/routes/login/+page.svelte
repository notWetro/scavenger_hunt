<script lang="ts">
	import { goto } from '$app/navigation';
	import LoginForm from '$lib/components/LoginForm.svelte';
	import { Button} from 'flowbite-svelte';


	function navigateToHome() {
		goto('/home');
	}
</script>

<main>

	<!-- New: Home Button -->
	<Button 
		on:click={() => goto('/')}
		class="absolute top-8 right-8"
		>
    	Home 🏠
  	</Button>

	<div class="flex flex-col gap-4 items-center justify-center">
		<h1 class="text-xl font-bold">Log in</h1>
		<p class="text-center m-4">
			Please log in with the account credentials you used for the registration for a Scavenger Hunt.
		</p>
	</div>
</main>

<LoginForm on:loginSuccess={navigateToHome} />
