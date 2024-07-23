<script lang="ts">
	import { Alert } from 'flowbite-svelte';
	import { Html5Qrcode } from 'html5-qrcode';
	import { createEventDispatcher, onDestroy, onMount } from 'svelte';

	export let data: string = '';

	onMount(() => {
		console.log('SOOOSEE');
		startScanner();
	});

	onDestroy(() => {
		console.log('EESOOOS');
		try {
			stopScanner();
		} catch (error) {
			/* Ignore, doesn't matter */
		}
	});

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
	 * @brief Handles successful scan events.
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

	function onScanFailure(error: string) {
		// Only log or handle errors if the scanner is actively scanning
		if (scannerActive) {
			console.error(`Code scan error = ${error}`);
			scanFailed = true;
		}
	}

	/**
	 * @brief Initializes and starts the scanner.
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
							// qrbox: getDynamicQrBoxSize
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
	 * @brief Stops the scanner.
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
