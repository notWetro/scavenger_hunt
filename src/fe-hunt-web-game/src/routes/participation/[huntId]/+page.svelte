<script lang="ts">
	import { PUBLIC_PARTICIPANT_API_URL } from '$env/static/public';
	import type { Hunt } from '$lib/models/hunt';
	import { Button, Card, Helper, Input, Label, Modal } from 'flowbite-svelte';
	import type { PageData } from './$types';
	import { Goal, Check } from 'lucide-svelte';
	import { goto } from '$app/navigation';

	let success: boolean | null = null;
	let attemptMade: boolean = false;
	let showModal: boolean = false;
	let validPassword: boolean = false;
	let showSuccessModal: boolean = false;

	let username: string;
	let password: string;

	export let data: PageData;
	$: hunt = data.hunt as Hunt;

	// Reactive statement to clear username and password when modal is closed
	$: if (!showModal) {
		username = '';
		password = '';
		success = null;
		attemptMade = false;
	}

	// New: Function to check if Password contains 8 chars, a upper case
	function isPasswordValid(password: string): boolean {
  		const passwordRegex = /^(?=.*[A-Z])(?=.*\d).{8,}$/;
  		return passwordRegex.test(password);
	}

	async function submit(username: string, password: string, huntId: number) {
		
		attemptMade = true;
		validPassword = !isPasswordValid(password);

		if (!isPasswordValid(password)) {
    		success = false;
    		console.error('Password must be at least 8 characters long, containing at least one number and one uppercase letter.');
    		return;
  		}


		try {
			const res = await fetch(`${PUBLIC_PARTICIPANT_API_URL}/Participations/Hunt/${huntId}`, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json'
				},
				body: JSON.stringify({
					username,
					password
				})
			});

			if (!res.ok) {
				success = false;
				throw new Error(await res.text());
			}

			showSuccessModal = true;
			success = true;
			showModal = false; 
			return await res.json();
		} catch (error) {
			console.error(error);
			success = false;
		}
	}

	function navigateToLogin() {
    	goto('/login');
  	}
</script>



<main>
	<div class="flex gap-2 flex-col mb-4 items-center">
		<h1 class="text-2xl font-bold underline">Register for Scavenger Hunt:</h1>
	</div>

	<!-- New: Home Button -->
	<Button 
    on:click={() => goto('/')}
    class="absolute top-8 right-8"
  	>
    Home 🏠
  	</Button>
	
	<div class="flex flex-col items-center">
		<Card>
		<h2 class="mb-4 text-xl font-bold tracking-tight text-gray-800">{hunt.title}</h2>
		<p class="text-gray-600 leading-tight font-normal mb-6">{hunt.description}</p>

		<Button
			class="mb-6"
			on:click={() => (showModal = true)}
			disabled={success === true}
		>
			{#if success}
			<Check />
			{:else}
			Participate!
			{/if}
		</Button>

		<div class="flex gap-4 flex-col mb-2 items-center">
		<button
			class="text font-bold underline text-blue-600 hover:text-blue-800 focus:outline-none"
			on:click={navigateToLogin}
		>
			Or you are already registered for this Hunt?
		</button>
		</div>

		</Card>
	</div>

	
	
	<!-- Participan Modal -->
	<Modal
		bind:open={showModal}
		title="Participate in Scavenger Hunt!"
		size="xs"
		autoclose={false}
		class="w-full"
	>
		<div class="flex flex-col space-y-6">
			<Label class="space-y-2">
				<span>Username</span>
				<Input required type="text" name="username" bind:value={username} placeholder="Username" />
			</Label>
			<Label class="space-y-2">
				<span>Your password</span>
				<Input
					type="password"
					name="password"
					color={success === false ? 'red' : 'base'}
					bind:value={password}
					placeholder="•••••"
					required
				/>
			</Label>
			<!-- New: check if Password is Valid -->
			{#if success === false && attemptMade && !validPassword}
  				<Helper class="text-red-900 text-sm">
    				Submission failed! Please try again.
  				</Helper>
			{:else if validPassword && attemptMade}
  				<Helper class="text-red-900 text-sm">
    				Password must be at least 8 characters long, contain at least one uppercase letter, and one number.
  				</Helper>
			{/if}
			<Button
				type="submit"
				on:click={async () => submit(username, password, hunt.id)}
				class="w-full1"
				disabled={!username || !password}
			>
				Submit participation credentials
				<Goal class="ml-2" />
			</Button>
		</div>
	</Modal>
	<!-- New: Feedback Success Modal if Participation was successful -->
	<Modal
		bind:open={showSuccessModal}
		title="Registration Successful!"
		size="xs"
		autoclose={true}
		class="w-full"
	>
		<p class="text-center text-green-600">Your registration was successful! 🎉</p>
		<div class="flex justify-center gap-4">
    		<Button on:click={() => goto('/')}>Home 🏠</Button>
    		<Button on:click={navigateToLogin}>Play</Button>
  		</div>
	</Modal>
</main>