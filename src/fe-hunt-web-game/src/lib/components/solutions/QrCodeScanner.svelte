<script lang="ts">
	import { Alert } from 'flowbite-svelte';
	import { Html5Qrcode } from 'html5-qrcode';
	import { createEventDispatcher, onDestroy, onMount } from 'svelte';

	/**
	 * Export a prop 'data' to hold the scanned QR code data.
	 */
	export let data: string = '';

	/**
	 * Initialize the QR code scanner on component mount.
	 */
	onMount(() => {
		startScanner();
	});

	/**
	 * Stop the QR code scanner on component destroy.
	 */
	onDestroy(() => {
		try {
			stopScanner();
		} catch (error) {
			/* Ignore, doesn't matter */
		}
	});

	/**
	 * Log the scanned data whenever it changes.
	 */
	$: {
		console.log(data);
	}

	let html5Qrcode: Html5Qrcode | null = null;
	let scannerActive = false;
	let searchFailed = false;
	let scanFailed = false;
	let cameraMissing = false;

	const dispatch = createEventDispatcher();

	/**
	 * Handles successful scan events.
	 * @param decodedText The text decoded from the scan.
	 */
	function onScanSuccess(decodedText: string) {
		data = decodedText;
		stopScanner();
		scanFailed = false;
		cameraMissing = false;
		scannerActive = false;
		dispatch('Finished');
	}

	/**
	 * Handles scan failure events.
	 * @param error The error message from the scan failure.
	 */
	function onScanFailure(error: string) {
		// Only log or handle errors if the scanner is actively scanning
		if (scannerActive) {
			console.error(`Code scan error = ${error}`);
			scanFailed = true;
		}
	}

	/**
	 * Initializes and starts the scanner.
	 */
	function startScanner() {
		scannerActive = true;
		setTimeout(() => {
			const readerElement = document.getElementById('reader');
			if (readerElement) {
				html5Qrcode = new Html5Qrcode('reader');
				html5Qrcode
					.start(
						{ facingMode: 'environment' },
						{
							fps: 10
						},
						onScanSuccess,
						onScanFailure
					)
					.catch((err) => {
						console.error(`Unable to start the scanner: ${err}`);
						cameraMissing = true;
						scannerActive = false;
					});
			} else {
				console.error('Element with id="reader" not found');
				cameraMissing = true;
				scannerActive = false;
			}
		}, 100);
	}

	/**
	 * Stops the scanner.
	 */
	function stopScanner() {
		if (html5Qrcode) {
			html5Qrcode
				.stop()
				.then(() => {
					scannerActive = false;
				})
				.catch((err) => {
					console.error(`Unable to stop the scanner: ${err}`);
				});
		}
	}
</script>

<!-- Display alerts for different error states and the QR code scanner -->
{#if searchFailed}
	<Alert color="yellow">Couldn't find QR-Code data!</Alert>
{/if}

{#if scanFailed}
	<Alert color="yellow">Couldn't detect any QR-Code!</Alert>
{/if}

{#if cameraMissing}
	<Alert color="red">Couldn't open the camera.</Alert>
{/if}

<div>
	{#if scannerActive}
		<div id="reader" class="bg-black mx-auto my-4 sm:my-6" />
	{/if}
</div>
