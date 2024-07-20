<script lang="ts">
	import { Card } from 'flowbite-svelte';
	import QrCodeSelector from './QrCodeScanner.svelte';
	import { ScanQrCodeIcon } from 'lucide-svelte';
	import { PUBLIC_API_URL } from '$env/static/public';
	import { playingHunt } from '$lib/stores/playingHunt';
	import { token } from '$lib/stores/tokenStore';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let data: string;

	async function submitSolution() {
		if (!data) throw Error('data is not defined!');

		const huntId = $playingHunt.id;
		const token = $token;
		console.log('huntId = ', huntId);
		console.log('token = ', token);
		console.log('data = ', data);

		const response = await fetch(
			`${PUBLIC_API_URL}/Participants/SubmitSolution/${$playingHunt.id}`,
			{
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: $token
				},
				body: JSON.stringify({ data })
			}
		);

		if (response.ok) {
			dispatch('ValidSolution');
		} else {
			dispatch('InvalidSolution');
		}
	}
</script>

<Card class="p-4">
	<div class="flex flex-row items-center text-center">
		<ScanQrCodeIcon class="w-8 h-8 mb-2 mr-2 text-primary-800" />
		<h5
			class="mb-2 text-xl font-semibold underline underline-offset-4 tracking-tight text-primary-800 dark:text-white"
		>
			Submit Solution
		</h5>
	</div>

	<QrCodeSelector bind:data on:Finished={submitSolution} />
	<p class="p-4">Once the QR-Code has been scanned it will be submitted.</p>
	<p class="p-4">{data}</p>
</Card>
