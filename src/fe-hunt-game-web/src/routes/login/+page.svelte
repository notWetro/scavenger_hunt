<script lang="ts">
	import { goto } from '$app/navigation';
	import LoginForm from '$lib/components/LoginForm.svelte';

	function navigateToHome() {
		goto('/home');
	}
</script>

<div class="flex flex-col gap-4">
	<h1>Anmeldung</h1>
	<span>
		Melde dich bitte mit deinem Account an, den du bei der Registrierung für eine Schnitzeljagd
		verwendet hast.
	</span>
</div>

<LoginForm on:loginSuccess={navigateToHome} />
