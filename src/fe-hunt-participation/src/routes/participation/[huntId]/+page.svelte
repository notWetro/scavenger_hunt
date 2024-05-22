<script lang="ts">
	import type { Hunt } from '$lib/models/hunt';

	export let huntData: Hunt = {
		id: 69,
		title: 'Hs-Aalen Ersties Tour',
		description: 'Nur für newbies!'
	};

	let formVisible: boolean = false;

	function showForm() {
		formVisible = true;
	}

	async function submit(username: string, huntId: number) {
		const res = await fetch(`http://localhost:4000/api/Participation/Hunt/${huntId}`);
		throw Error('This function is not implemented.');
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

	{#if formVisible}
		<button class="btn btn-neutral"> Clear </button>
		<button class="btn btn-primary" on:click={async () => submit('username', huntData.id)}>
			Submit
		</button>
	{/if}
</main>
