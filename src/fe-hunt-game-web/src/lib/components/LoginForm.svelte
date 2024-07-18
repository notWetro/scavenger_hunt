<script lang="ts">
	import { PUBLIC_API_URL } from '$env/static/public';
	import type { LoginResponse } from '$lib/dtos/login/loginResponse';
	import { registeredHunts } from '$lib/stores/registeredHunts';
	import { token } from '$lib/stores/tokenStore';
	import { wait } from '$lib/utils/wait';
	import { Button, Input } from 'flowbite-svelte';
	import { UserPenIcon, KeySquareIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	let dispatch = createEventDispatcher();
	let username: string = '';
	let password: string = '';

	let isLastFail: boolean = false;
	$: {
		if (isLastFail) {
			wait(2000).then(() => {
				isLastFail = false;
			});
		}
	}

	async function loginPressed() {
		const response = await fetch(`${PUBLIC_API_URL}/Participants/Login`, {
			method: 'POST',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify({ username, password })
		});

		if (response.ok) {
			const responseData = (await response.json()) as LoginResponse;
			token.set(responseData.token);
			registeredHunts.set(responseData.hunts);
			dispatch('loginSuccess');
		} else {
			isLastFail = true;
			dispatch('loginFail');
		}
	}
</script>

<div class="flex flex-col gap-4">
	<Input placeholder="Benutzername" bind:value={username}>
		<UserPenIcon slot="left" />
	</Input>

	<Input type="password" placeholder="Passwort" bind:value={password}>
		<KeySquareIcon slot="left" />
	</Input>

	<Button color={isLastFail ? 'red' : 'primary'} on:click={loginPressed} disabled={isLastFail}
		>{isLastFail ? 'Falsche Login-Daten' : 'Anmelden'}</Button
	>
</div>
