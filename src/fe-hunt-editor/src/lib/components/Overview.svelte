<script lang="ts">
	import { createEventDispatcher, onMount } from 'svelte';
	import { v4 as uuidv4 } from 'uuid';
	import QRCode from 'qrcode';
	import { huntStore } from '$lib/stores/huntStore';
	import { Button } from 'flowbite-svelte';
	import { Goal } from 'lucide-svelte';
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

	async function submitHunt() {
    try {
        console.log('Hunt:', hunt);
        const huntData = { ...hunt };

        // Optional: Validate huntData here

        const timeout = (ms) => new Promise((_, reject) => setTimeout(() => reject(new Error('Request timed out')), ms));
        const fetchPromise = fetch(`${PUBLIC_API_URL}/hunts`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(huntData)
        });
        const response = await Promise.race([fetchPromise, timeout(5000)]);

        if (!response.ok) {
            const errorData = await response.json();
            throw new Error(`Failed to create Hunt: ${response.status} - ${errorData.message || 'Unknown error'}`);
        }

        console.log("Hunt created successfully");
        dispatch('Finished');
    } catch (error) {
        console.error("Error creating hunt:", error);
        // Optional: Dispatch an error action or show a user notification
    }
}


</script>

<div class="flex flex-col">
	<h1 class="font-semibold text-2xl">Title: {hunt.title}</h1>
	<p>Description: {hunt.description}</p>
</div>

<OverviewTable {qrCodes} />

<Button class="mt-5" on:click={submitHunt}>
	<Goal class="mr-2" />
	Create scavenger hunt!
</Button>
