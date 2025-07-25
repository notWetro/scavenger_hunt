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

	/** List of QR codes associated with the assignments. */
	let qrCodes: any[] = [];

	/** The current hunt data from the store. */
	let hunt = $huntStore;

	const dispatch = createEventDispatcher();

	onMount(async () => {
		await generateQRCodes();
	});

	/**
	 * Generates QR codes for assignments with QR code solutions.
	 */
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

	/**
	 * Submits the hunt data to the server.
	 */
	async function submitHunt() {
		try {
			console.log('Hunt:', hunt);

			// Sets the additionalData to null if the field is empty
			hunt.assignments.forEach((assignment) => {
				if (!assignment.hint.additionalData) {
					assignment.hint.additionalData = null;
				}
			});

			const huntData = { ...hunt };

			const timeout = (ms) => new Promise((_, reject) => setTimeout(() => reject(new Error('Request timed out')), ms));
			const fetchPromise = fetch(`${PUBLIC_API_URL}/hunts`, {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify(huntData)
			});
			const response = await Promise.race([fetchPromise, timeout(5000)]);

			if (!response.ok) {
				const errorData = await response.json();
				console.error("Backend error:", errorData);
				throw new Error(`Failed to create Hunt: ${response.status} - ${errorData.message || 'Unknown error'}`);
			}

			console.log("Hunt created successfully");
			dispatch('Finished');
		} catch (error) {
			console.error("Error creating hunt:", error);
		}
	}

	/**
	 * Navigates back to the previous step.
	 */
	function goBackToPreviousStep() {
		dispatch('goBack');
	}
</script>

<div class="flex flex-col">
	<h1 class="font-semibold text-2xl">Title: {hunt.title}</h1>
	<p>Description: {hunt.description}</p>
</div>

<OverviewTable {qrCodes} />

<!-- New: Add the Button to go back from the last hunt creation screen -->
<div style="display: flex; gap: 20px; justify-content: flex-end; align-items: right; width: 100%;">
<Button class="mt-5" on:click={goBackToPreviousStep} style="flex: 1;">
	<ArrowLeft class="ml-2" />
	Previous
</Button>
<Button class="mt-5" on:click={submitHunt} style="flex: 1;">
	<Goal class="mr-2" />
	Create scavenger hunt!
</Button>
</div>