<script lang="ts">
	import {
		Button,
		Table,
		TableBody,
		TableBodyCell,
		TableBodyRow,
		TableHead,
		TableHeadCell
	} from 'flowbite-svelte';
	import { Download } from 'lucide-svelte';
	import { huntStore } from '$lib/stores/huntStore';
	$: hunt = $huntStore;

	export let qrCodes: [];

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
</script>

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
						<!--						TODO: check if this error is important-->
						<img src={qrCodes.find((qr) => qr.id === assignment.id)?.qrUrl || ''} alt="QR Code" />
					{/if}
				</TableBodyCell>
				<TableBodyCell>
					{#if assignment.solution.solutionType === 'QRCode'}
						<!--						TODO: check if this error is important-->
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
