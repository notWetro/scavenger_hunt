<script lang="ts">
	import { createEventDispatcher, onMount } from 'svelte';
	import { v4 as uuidv4 } from 'uuid';
	import QRCode from 'qrcode';
	import { huntStore } from '$lib/stores/huntStore';
	import { Button } from 'flowbite-svelte';
	import { Goal, ArrowLeft } from 'lucide-svelte';
	import { PUBLIC_API_URL } from '$env/static/public';
	import OverviewTable from '$lib/components/OverviewTable.svelte';
	import { SolutionType } from '$lib/models/Solution';

	let qrCodes: any[] = [];
	
	// Subscribe to huntStore to access its current state
	$: hunt = $huntStore;

	const dispatch = createEventDispatcher();

	onMount(async () => {
		await generateQRCodes();
	});

	async function generateQRCodes() {
		qrCodes = await Promise.all(
			hunt.assignments.map(async (assignment) => {
				if (assignment.solution.solutionType === SolutionType.QRCode) {
					const qrText = `scavhunt-${uuidv4()}`;
					try {
						const qrUrl = await QRCode.toDataURL(qrText);
						assignment.solution.data = qrText;
						console.log(qrText);
						return { id: assignment.id, qrUrl };
					} catch (error) {
						console.error('Error generating QR code:', error);
						return { id: assignment.id, qrUrl: '' };
					}
				} else {
					return { id: assignment.id, qrUrl: '' };
				}
			})
		);
	}

	async function updateHunt() {
		console.log('Hunt:', hunt);
		const response = await fetch(`${PUBLIC_API_URL}/hunts/${hunt.id}`, {
			method: 'PUT',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(hunt)
		});
		if (!response.ok) {
			throw new Error(`Failed to create Hunt: ${response.status}`);
		}
		dispatch('Finished');
		
	}

// New: Calls the on:goBack={decreaseStep} in \src\routes\edit\[huntId]\+page.svelte
function goBackToPreviousStep() {
  		dispatch('goBack');
	}

</script>

<div class="flex flex-col">
	<h1 class="font-semibold text-2xl">Title: {hunt.title}</h1>
	<p>Description: {hunt.description}</p>
</div>

<OverviewTable {qrCodes} />


<div style="display: flex; gap: 20px; justify-content: flex-end; align-items: right; width: 100%;">
	<Button class="mt-5" on:click={goBackToPreviousStep} style="flex: 1;">
		<ArrowLeft class="ml-2" />
		Previous
	</Button>
	<Button class="mt-5" on:click={updateHunt} style="flex: 1;">
		<Goal class="mr-2" />
		Update scavenger hunt
	</Button>
</div>