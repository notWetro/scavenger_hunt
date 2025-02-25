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
	import { Download, XCircle } from 'lucide-svelte';
	import { huntStore } from '$lib/stores/huntStore';
	import { HintType } from '$lib/models/Hint';
	import { SolutionType } from '$lib/models/Solution';
	import { mapHintTypeToText, mapSolutionTypeToText } from '$lib/utils/typeMappingUtil';

	/** The current hunt data from the store. */
	$: hunt = $huntStore;

	/** List of QR codes associated with the assignments. */
	export let qrCodes: any[];

	/**
	 * Downloads the QR code image for a given assignment.
	 * @param id - The ID of the assignment.
	 * @param qrUrl - The URL of the QR code image.
	 */
	async function downloadQRCode(id: number, qrUrl: any) {
		try {
			const response = await fetch(qrUrl);
			const blob = await response.blob();
			const downloadUrl = window.URL.createObjectURL(blob);
			const a = document.createElement('a');
			a.style.display = 'none';
			a.href = downloadUrl;
			a.download = `qr-code-${id}.png`;
			document.body.appendChild(a);
			a.click();
			window.URL.revokeObjectURL(downloadUrl);
		} catch (error) {
			console.error('Error downloading QR code:', error);
		}
	}
</script>

<Table >
	<TableHead>
		<TableHeadCell class="text-center align-middle">Id</TableHeadCell>
    	<TableHeadCell class="text-center align-middle">Hint type</TableHeadCell>
    	<TableHeadCell class="text-center align-middle">Hint</TableHeadCell>
    	<TableHeadCell class="text-center align-middle">Additional Hint Data</TableHeadCell>
    	<TableHeadCell class="text-center align-middle">Solution type</TableHeadCell>
    	<TableHeadCell class="text-center align-middle">Solution</TableHeadCell>
		</TableHead>
	<TableBody>
		{#each hunt.assignments as assignment}
			<TableBodyRow>
				<TableBodyCell class="text-center align-middle">{assignment.id}</TableBodyCell>
				<TableBodyCell class="text-center align-middle">{mapHintTypeToText(assignment.hint.hintType)}</TableBodyCell>
				<TableBodyCell class="text-center align-middle">
					{#if assignment.hint.hintType === HintType.Text}
						{assignment.hint.data}
					{:else if assignment.hint.hintType === HintType.Image}
						<div class="flex flex-col items-center"> 
							<img src={`${assignment.hint.data}`} alt="Hint as Img" class="object-scale-down w-28" />
						</div>
					<!-- New: Add correctly scaled Video as Hint Type -->
					{:else if assignment.hint.hintType === HintType.Video}
						<div class="flex flex-col items-center"> 
							<!-- svelte-ignore a11y-media-has-caption -->
							<video controls class="object-scale-down w-28">
								<source src={`${assignment.hint.data}`} type="video/mp4" />
								Your browser does not support the video tag.
							</video>
						</div>
					<!-- New: Add Audio as Hint Type -->
					{:else if assignment.hint.hintType === HintType.Audio}
						<div class="flex flex-col items-center"> 
							<!-- svelte-ignore a11y-media-has-caption -->
							<audio controls class="object-scale-down w-28">
								<source src={`${assignment.hint.data}`} type="audio/mp3" />
								Your browser does not support the audio element.
							</audio>
						</div>
					{/if}
				</TableBodyCell>
				<TableBodyCell class="text-center align-middle"> 
					{#if assignment.hint.hintType === HintType.Image || assignment.hint.hintType == HintType.Video}
						{assignment.hint.additionalData}
					{:else}
						<div class="flex items-center justify-center">
    						<XCircle class="w-5 h-5 mr-2" />
    						No additional data available
  						</div>
					{/if} 
				</TableBodyCell>
				<TableBodyCell class="text-center align-middle">{mapSolutionTypeToText(assignment.solution.solutionType)}</TableBodyCell>
				<TableBodyCell class="text-center align-middle">
					{#if assignment.solution.solutionType === SolutionType.Text}
						{assignment.solution.data}
					{:else if assignment.solution.solutionType === SolutionType.Location}
						Latitude: {assignment.solution.data.split(';')[0]}, Longitude: {assignment.solution.data.split(
							';'
						)[1]}
					{:else if assignment.solution.solutionType === SolutionType.QRCode}
						<div class="flex flex-col items-center"> 
						<img src={qrCodes.find((qr) => qr.id === assignment.id)?.qrUrl || ''} alt="QR Code" />
						<Button
							on:click={() =>
								downloadQRCode(
									assignment.id,
									qrCodes.find((qr) => qr.id === assignment.id)?.qrUrl || ''
								)}
						>
							<Download />
						</Button>
						</div>
					{/if}
				</TableBodyCell>
			</TableBodyRow>
		{/each}
	</TableBody>
</Table>
