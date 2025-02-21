<script lang="ts">
	import { goto } from '$app/navigation';
	import LoginForm from '$lib/components/LoginForm.svelte';
	import { Button } from 'flowbite-svelte';

	/**
	 * Navigates the user to the home page.
	 */
	function navigateToHome() {
		goto('/home');
	}
</script>

<div class="relative w-full bg-gray-100">
	<main>
		<div class="flex flex-col gap-4 items-center justify-center">
			<h1 class="text-xl font-bold">Log in</h1>
			<p class="text-center m-4">
				Please log in with the account credentials you used for the registration for a Scavenger Hunt.
			</p>
		</div>
	</main>

	<LoginForm on:loginSuccess={navigateToHome} />
</div>