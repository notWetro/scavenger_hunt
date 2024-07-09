<script lang="ts">
	import { createEventDispatcher, onMount } from 'svelte';
	import { v4 as uuidv4 } from 'uuid';
	import QRCode from 'qrcode';
	import { huntStore } from '$lib/stores/huntStore';
	import { Button } from 'flowbite-svelte';
	import { Download, Goal } from 'lucide-svelte';
	import {
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';

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
				if (assignment.solution.solutionType === 'QRCode') {
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

	async function downloadQRCode(id: number, qrUrl) {
		try {
			const response = await fetch(qrUrl);
			const blob = await response.blob();
			const downloadUrl = window.URL.createObjectURL(blob);
			const a = document.createElement('a');
			a.style.display = 'none';
			a.href = downloadUrl;
			// The filename you want to save the file as
			a.download = `qr-code-${id}.png`;
			document.body.appendChild(a);
			a.click();
			window.URL.revokeObjectURL(downloadUrl);
		} catch (error) {
			console.error('Error downloading QR code:', error);
		}
	}

	function submitHunt() {
		console.log('Hunt:', hunt);
		// TODO: Implement API call to submit the hunt
		dispatch('Finished');
	}
</script>

<div class="flex flex-col">
	<h1 class="font-semibold text-2xl">Titel: {hunt.title}</h1>
	<p>Beschreibung: {hunt.description}</p>
</div>

<Table>
	<TableHead>
		<TableHeadCell>Id</TableHeadCell>
		<TableHeadCell>Hinweistyp</TableHeadCell>
		<TableHeadCell>Hinweis</TableHeadCell>
		<TableHeadCell>Lösungstyp</TableHeadCell>
		<TableHeadCell>Lösung</TableHeadCell>
		<TableHeadCell>Aktion</TableHeadCell>
	</TableHead>
	<TableBody tableBodyClass="divide-y">
		{#each hunt.assignments as assignment}
			<TableBodyRow>
				<TableBodyCell>{assignment.id}</TableBodyCell>
				<TableBodyCell>{assignment.hint.hintType}</TableBodyCell>
				<TableBodyCell>{assignment.hint.data}</TableBodyCell>
				<TableBodyCell>{assignment.solution.solutionType}</TableBodyCell>
				<TableBodyCell>
					{#if assignment.solution.solutionType === 'Text'}
						{assignment.solution.data}
					{:else if assignment.solution.solutionType === 'Location'}
						Latitude: {assignment.solution.data.split(';')[0]}, Longitude: {assignment.solution.data.split(
							';'
						)[1]}
					{:else if assignment.solution.solutionType === 'QRCode'}
						<img src={qrCodes.find((qr) => qr.id === assignment.id)?.qrUrl || ''} alt="QR Code" />
					{/if}
				</TableBodyCell>
				<TableBodyCell>
					{#if assignment.solution.solutionType === 'QRCode'}
						<Button
							on:click={() =>
								downloadQRCode(
									assignment.id,
									qrCodes.find((qr) => qr.id === assignment.id)?.qrUrl || ''
								)}
						>
							<Download />
						</Button>
					{/if}
				</TableBodyCell>
			</TableBodyRow>
		{/each}
	</TableBody>
</Table>

<Button class="mt-5" on:click={submitHunt}>
	<Goal class="mr-2" />
	Schnitzeljagd erstellen
</Button>
