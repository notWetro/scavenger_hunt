<script lang="ts">
	import { goto } from '$app/navigation';
	import LoginForm from '$lib/components/LoginForm.svelte';

	function navigateToHome() {
		goto('/home');
	}
</script>

<div class="flex flex-col gap-4 items-center justify-center">
	<h1 class="text-xl font-bold">Log in</h1>
	<p class="text-center m-4">
		Plase log in with the account credentials you used for the registration for a Scavenger Hunt.
	</p>
</div>

<LoginForm on:loginSuccess={navigateToHome} />
