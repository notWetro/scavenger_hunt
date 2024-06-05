<script lang="ts">
	import type { Hunt } from '$lib/models/hunt';
	import { postParticipation } from '$lib/services/hunt-api';
	import { error } from '@sveltejs/kit';
	import type { PageData } from './$types';
	import Check from 'lucide-svelte/icons/check';

	let modal: HTMLDialogElement;
	let success: boolean = false;
	let username: string;
	let password: string;

	export let data: PageData;
	$: hunt = data.hunt as Hunt;

	function showForm() {
		modal.showModal();
	}

	async function submit(username: string, password: string, huntId: number) {
		await postParticipation(username, password, huntId)
			.then(() => (success = true))
			.catch(() => (success = false));
	}
</script>

<main>
	<h1 class="text-2xl font-bold text-center">
		Anmeldung
		<span class="badge badge-lg">@{hunt.id}</span>
	</h1>

	<div class="card w-96 bg-primary text-primary-content mt-5">
		<div class="card-body">
			<h2 class="card-title">{hunt.title}</h2>
			<p>{hunt.description}</p>
			<div class="card-actions justify-end">
				<button class="btn" on:click={showForm} disabled={success}>
					{#if success}
						<Check />
					{:else}
						Participate!
					{/if}
				</button>
			</div>
		</div>
	</div>

	<dialog class="modal" bind:this={modal}>
		<div class="modal-box">
			<h3 class="font-bold text-lg">Participate in Scavenger Hunt!</h3>
			<form method="dialog" class="modal-backdrop">
				<div class="form-control">
					<label for="username" class="label">
						<span class="label-text">Username</span>
					</label>
					<input
						bind:value={username}
						type="text"
						id="username"
						placeholder="Username"
						class="input input-bordered text-gray-300"
					/>
				</div>
				<div class="form-control">
					<label for="password" class="label">
						<span class="label-text">Password</span>
					</label>
					<input
						bind:value={password}
						type="password"
						id="password"
						placeholder="Password"
						class="input input-bordered text-gray-300"
					/>
				</div>
				<div class="flex flex-row justify-end mt-4 gap-5">
					<button
						class="btn btn-neutral"
						on:click={() => {
							username = '';
							password = '';
						}}>Cancel</button
					>
					<button
						class="btn btn-primary"
						on:click={async () => submit(username, password, hunt.id)}
					>
						Submit
					</button>
				</div>
			</form>
		</div>
	</dialog>
</main>
