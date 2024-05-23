<script lang="ts">
	import type { Hunt } from '$lib/models/hunt';
	import { onMount } from 'svelte';

	let modal: HTMLDialogElement;
	let username: string;
	let password: string;
	// TODO: accessing this still gives undefined
	export let huntData: Hunt = {
		id: 67,
		title: 'Hs-Aalen Ersties Tour',
		description: 'Nur für newbies!'
	};

	$: console.log('huntdata', huntData);

	function showForm() {
		modal.showModal();
	}
	// TODO: figure add actual http request
	async function submit(username: string, password: string, huntId: number) {
		console.log(username, password, huntId);
		//const res = await fetch(`http://localhost:4000/api/Participation/Hunt/${huntId}`);
	}
</script>

<main class="p-4">
	<h1 class="text-2xl font-bold text-center">
		Anmeldung
		<span class="badge badge-lg">@{huntData.id}</span>
	</h1>

	<div class="card w-96 bg-primary text-primary-content">
		<div class="card-body">
			<h2 class="card-title">{huntData.title}</h2>
			<p>{huntData.description}</p>
			<div class="card-actions justify-end">
				<button class="btn" on:click={showForm}> Participate! </button>
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
					<!--					TODO: figure out why clear also closes dialog -->
					<button
						class="btn btn-neutral"
						on:click={() => {
							username = '';
							password = '';
						}}>Clear</button
					>
					<button
						class="btn btn-primary"
						on:click={async () => submit(username, password, huntData.id)}>Submit</button
					>
				</div>
			</form>
		</div>
	</dialog>
</main>
