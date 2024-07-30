<script lang="ts">
	import { PUBLIC_PARTICIPANT_API_URL } from '$env/static/public';
	import type { Hunt } from '$lib/models/hunt';
	import { Button, Card, Helper, Input, Label, Modal } from 'flowbite-svelte';
	import type { PageData } from './$types';
	import { Goal, Check } from 'lucide-svelte';

	let success: boolean | null = null;
	let attemptMade: boolean = false;
	let showModal: boolean = false;

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

	async function submit(username: string, password: string, huntId: number) {
		attemptMade = true;
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

			success = true;
			return await res.json();
		} catch (error) {
			console.error(error);
			success = false;
		}
	}
</script>

<main>
	<div class="flex gap-2 flex-col mb-4 items-center">
		<h1 class="text-2xl font-bold underline">Register for Scavenger Hunt:</h1>
		<span>{hunt.title}</span>
	</div>
	<div class="flex flex-col items-center">
		<Card>
			<h2 class="mb-2 text-xl font-bold tracking-tight text-gray-800">{hunt.title}</h2>
			<p class="text-gray-600 leading-tight font-normal mb-3">{hunt.description}</p>
			<Button on:click={() => (showModal = true)} disabled={success === true}>
				{#if success}
					<Check />
				{:else}
					Participate!
				{/if}
			</Button>
		</Card>
	</div>
	

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
			<Helper class={`text-red-900 text-sm ${success === false && attemptMade ? '' : 'hidden'}`}
				>Submission failed! Please try again.</Helper
			>
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
</main>
