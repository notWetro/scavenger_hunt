<script lang="ts">
	import type { Hunt } from '$lib/models/Hunt';

	export let hunt: Hunt;
	import { Edit, Trash } from 'lucide-svelte';
	import { Button, Card } from 'flowbite-svelte';
	import { PUBLIC_API_URL } from '$env/static/public';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	async function deleteHunt() {
		const response = await fetch(`${PUBLIC_API_URL}/hunts/${hunt.id}`, {
			method: 'DELETE'
		});
		if (response.ok) {
			console.log('Hunt deleted successfully');
			dispatch('HuntDeleted');
		} else {
			console.error('Error deleting hunt:', response.statusText);
		}
	}
</script>

<Card class="flex flex-col justify-between w-[22.5rem] bg-base-100 shadow-lg rounded-xl md:w-96">
	<div>
		<img src="/src/lib/images/scav-hunt-icon.jpg" alt="scav-hunt-logo" class="object-fill" />
		<div class="p-4">
			<h2 class="text-xl font-bold text-black">{hunt.title}</h2>
			<p>{hunt.description}</p>
		</div>
	</div>
	<div class="flex justify-end gap-2">
		<Button href="/edit/{hunt.id}">
			<Edit class="mr-1" />
			Edit
		</Button>
		<Button on:click={deleteHunt}>
			<Trash class="mr-1" />
			Delete
		</Button>
	</div>
</Card>
