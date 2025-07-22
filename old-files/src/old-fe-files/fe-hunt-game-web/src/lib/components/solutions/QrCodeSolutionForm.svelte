<script lang="ts">
	import { Card } from 'flowbite-svelte';
	import { ScanQrCodeIcon } from 'lucide-svelte';

	import { createEventDispatcher } from 'svelte';
	import QrCodeReader from '../qr-code-reader/QRCodeReader.svelte';

	const dispatch = createEventDispatcher();

	export let data: string;
	let displayText: string = 'FOUND YOU!';
	let isSend = false;

	async function submitSolution() {
		if (!isSend) {
			dispatch('SubmitData');
			isSend = true;
		}
	}
</script>

<Card class="p-4 mb-4 w-full max-w-none mx-auto">
	<div class="flex flex-row items-center text-center">
		<ScanQrCodeIcon class="w-8 h-8 mb-2 mr-2 text-primary-800" />
		<h5
			class="mb-2 text-xl font-semibold underline underline-offset-4 tracking-tight text-primary-800 dark:text-white"
		>
			Submit Solution
		</h5>
	</div>

	<div class="qr-code-container">
		<QrCodeReader bind:data bind:displayText on:Finished={submitSolution} />
	</div>

	<p class="p-4">Once the QR-Code has been scanned it will be submitted.</p>
</Card>

<style>
	.qr-code-container {
		display: flex;
		justify-content: center;
		align-items: center;
		overflow: hidden; /* Hide any overflow */
		position: relative; /* For positioning the QR code within the container */
		height: 200px; /* Set a fixed height or use relative units */
		width: 100%; /* Make the container take full width */
	}
</style>
