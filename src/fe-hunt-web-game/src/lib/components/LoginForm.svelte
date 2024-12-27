<script lang="ts">
	import { PUBLIC_API_URL } from '$env/static/public';
	import type { LoginResponse } from '$lib/dtos/login/loginResponse';
	import { completeHunts } from '$lib/stores/completeHunts';
	import { expiredHunts } from '$lib/stores/expiredHunts';
	import { ongoingHunts } from '$lib/stores/ongoingHunts';
	import { token } from '$lib/stores/tokenStore';
	import { wait } from '$lib/utils/wait';
	import { Button, Input } from 'flowbite-svelte';
	import { UserPenIcon, KeySquareIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';
	import { goto } from '$app/navigation';

	let dispatch = createEventDispatcher();
	let username: string = '';
	let password: string = '';

	let isLastFail: boolean = false;
	/**
	 * Reactive statement that checks if the last login attempt failed.
	 * If it did, it waits for 2000 milliseconds (2 seconds) before resetting the `isLastFail` flag to false.
	 * This provides a delay before the user can attempt to log in again.
	 */
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

			const expiredHuntsResponse = responseData.hunts.filter(
				(hunt) =>
					hunt.participationStatus === 0 ||
					hunt.participationStatus === 1 ||
					hunt.participationStatus === 4
			);
			if (expiredHuntsResponse) expiredHunts.set(expiredHuntsResponse);

			const ongoingHuntsResponse = responseData.hunts.filter(
				(hunt) => hunt.participationStatus === 2
			);
			if (ongoingHuntsResponse) ongoingHunts.set(ongoingHuntsResponse);

			const completeHuntsResponse = responseData.hunts.filter(
				(hunt) => hunt.participationStatus === 3
			);
			if (completeHuntsResponse) completeHunts.set(completeHuntsResponse);

			dispatch('loginSuccess');
		} else {
			isLastFail = true;
			dispatch('loginFail');
		}
	}
</script>

<div class="flex flex-col gap-4">

	<Input placeholder="username" bind:value={username}>
		<UserPenIcon slot="left" />
	</Input>

	<Input type="password" placeholder="password" bind:value={password}>
		<KeySquareIcon slot="left" />
	</Input>

	<Button color={isLastFail ? 'red' : 'primary'} on:click={loginPressed} disabled={isLastFail}
		>{isLastFail ? 'Wrong Log in credentials!' : 'Log in'}</Button
	>
</div>
