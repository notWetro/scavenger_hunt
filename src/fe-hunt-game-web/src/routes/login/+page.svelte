<script lang="ts">
	import { goto } from '$app/navigation';
	import LoginForm from '$lib/components/LoginForm.svelte';

	function navigateToHome() {
		goto('/home');
	}
</script>

<div class="flex flex-col gap-4 items-center justify-center">
	<h1 class="text-xl font-bold">Anmeldung</h1>
	<p class="text-center m-4">
		Melde dich bitte mit deinem Account an, den du bei der Registrierung für eine Schnitzeljagd
		verwendet hast.
	</p>
</div>

<LoginForm on:loginSuccess={navigateToHome} />
