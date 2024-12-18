<script lang="ts">
	import { Button, Card } from 'flowbite-svelte';
	import { MapPinIcon } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	const dispatch = createEventDispatcher();

	export let data: string;

	async function submitSolution() {
		try {
			data = await getCurrentLocation();
			dispatch('SubmitData');
		} catch (error) {
			console.error(error);
			dispatch('InvalidSolution');
		}
	}

	async function getCurrentLocation(): Promise<string> {
		return new Promise((resolve, reject) => {
			navigator.geolocation.getCurrentPosition(
				(position) => {
					const { latitude, longitude } = position.coords;
					resolve(`${latitude};${longitude}`);
				},
				(error) => {
					reject(`Unable to retrieve your location: ${error.message}`);
				}
			);
		});
	}
</script>

<Card class="p-4 mb-4 w-full max-w-none mx-auto">
	<div class="flex flex-row items-center text-center">
		<MapPinIcon class="w-8 h-8 mb-2 mr-2 text-primary-800" />
		<h5
			class="mb-2 text-xl font-semibold underline underline-offset-4 tracking-tight text-primary-800 dark:text-white"
		>
			Submit Solution
		</h5>
	</div>

	<p class="p-4">Submit your location to know if you are near.</p>
	<Button on:click={submitSolution}>Submit</Button>
</Card>
