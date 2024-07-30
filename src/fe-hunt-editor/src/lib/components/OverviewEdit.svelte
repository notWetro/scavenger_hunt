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

	let qrCodes = [];

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
		// Map solutionType and hintType strings to numbers
		const solutionTypeMapping = { QRCode: 0, Text: 1, Location: 2 };
		const hintTypeMapping = { Text: 0, Image: 1 };

		// Create a new huntData object with modified assignments
		const huntData = {
			...hunt,
			assignments: hunt.assignments.map((assignment) => ({
				...assignment,
				solution: {
					...assignment.solution,
					solutionType: solutionTypeMapping[assignment.solution.solutionType]
				},
				hint: {
					...assignment.hint,
					hintType: hintTypeMapping[assignment.hint.hintType]
				}
			}))
		};

		const response = await fetch(`${PUBLIC_API_URL}/hunts/${huntData.id}`, {
			method: 'PUT',
			headers: {
				'Content-Type': 'application/json'
			},
			body: JSON.stringify(huntData)
		});
		if (!response.ok) {
			throw new Error(`Failed to create Hunt: ${response.status}`);
		}
		dispatch('Finished');
	}
</script>

<div class="flex flex-col">
	<h1 class="font-semibold text-2xl">Titel: {hunt.title}</h1>
	<p>Beschreibung: {hunt.description}</p>
</div>

<OverviewTable {qrCodes} />

<Button class="mt-5" on:click={updateHunt}>
	<Goal class="mr-2" />
	Schnitzeljagd aktualisieren
</Button>
