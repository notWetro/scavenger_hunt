<script lang="ts">
	import { Alert, Button } from 'flowbite-svelte';
	import { Html5Qrcode } from 'html5-qrcode';
	import { Camera, CameraOff } from 'lucide-svelte';
	import { createEventDispatcher } from 'svelte';

	export let data: string = '';

	$: {
		console.log(data);
	}

	let html5Qrcode: Html5Qrcode | null = null;
	let scannerActive = false;
	let searchFailed = false;
	let scanFailed = false;
	let cameraMissing = false;

	const dispatch = createEventDispatcher();

	// /**
	//  * @brief Calculates the size of a dynamic QR box based on the viewfinder's dimensions.
	//  *
	//  * This function determines the width and height of a QR box that maintains a specific
	//  * aspect ratio within the constraints of the viewfinder's dimensions. It ensures that
	//  * the QR box's size does not fall below a minimum size.
	//  *
	//  * @param viewfinderWidth The width of the viewfinder in pixels.
	//  * @param viewfinderHeight The height of the viewfinder in pixels.
	//  * @returns An object containing the calculated width and height of the QR box.
	//  */
	// function getDynamicQrBoxSize(viewfinderWidth: number, viewfinderHeight: number) {
	// 	const targetAspectRatio = 8 / 3; // Target aspect ratio for the QR box.
	// 	const minSize = 50; // Minimum size for the width and height of the QR box.
	// 	let width: number, height: number;

	// 	// Calculate the QR box size based on the viewfinder's aspect ratio.
	// 	if (viewfinderWidth / viewfinderHeight > targetAspectRatio) {
	// 		height = Math.floor(viewfinderHeight * 0.7); // 70% of viewfinder height.
	// 		width = Math.floor(height * targetAspectRatio); // Width based on target aspect ratio.
	// 	} else {
	// 		width = Math.floor(viewfinderWidth * 0.7); // 70% of viewfinder width.
	// 		height = Math.floor(width / targetAspectRatio); // Height based on target aspect ratio.
	// 	}

	// 	// Ensure the QR box size does not fall below the minimum size.
	// 	width = Math.max(width, minSize);
	// 	height = Math.max(height, minSize);
	// 	return { width, height };
	// }

	/**
	 * @brief Handles successful scan events.
	 *
	 * This function is called when a scan successfully decodes a barcode. It updates the state
	 * based on the decoded text, validates the EAN, and deactivates the scanner.
	 *
	 * @param decodedText The text decoded from the barcode scan.
	 */
	function onScanSuccess(decodedText: string) {
		data = decodedText;
		stopScanner();
		scanFailed = false;
		cameraMissing = false;
		scannerActive = false;
		dispatch('finished');
	}

	function onScanFailure(error: string) {
		scanFailed = true;
		console.warn(`Code scan error = ${error}`);
	}

	/**
	 * @brief Initializes and starts the barcode scanner.
	 *
	 * This function activates the scanner, waits for a short delay, and then attempts to
	 * initialize the Html5Qrcode scanner on a specific page element. It configures the scanner
	 * to use the environment-facing camera (back), sets the frame rate and QR box size, and defines
	 * callback functions for scan success and failure. If the scanner cannot be started or if
	 * the target element is missing, it logs an error and updates the state to reflect that the
	 * camera is missing or inactive.
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
	 * @brief Stops the barcode scanner.
	 *
	 * This function attempts to stop the barcode scanner if it is currently active. It sets the
	 * scanner's active state to false upon successful termination. If the scanner cannot be
	 * stopped, it logs the error to the console.
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

	/**
	 * @brief Toggles the barcode scanner's active state when the Camera button is clicked.
	 *
	 * This function is responsible for toggling the barcode scanner's active state. If the scanner
	 * is currently active, it will be stopped. Otherwise, the scanner will be started. It also resets
	 * the `scanFailed` and `cameraMissing` flags to false regardless of the scanner's new state.
	 */
	function toggleScanner() {
		scanFailed = false;
		cameraMissing = false;
		if (scannerActive) {
			stopScanner();
		} else {
			startScanner();
		}
	}
</script>

{#if searchFailed}
	<Alert color="yellow">Couldn't find product!</Alert>
{/if}

{#if scanFailed}
	<Alert color="yellow">Couldn't detect any sq code!</Alert>
{/if}

{#if cameraMissing}
	<Alert color="yellow">Couldn't open the camera.</Alert>
{/if}

<div>
	<div class="flex items-center w-full gap-2">
		<Button color="alternative" on:click={toggleScanner}>
			{#if scannerActive}
				<CameraOff />
			{:else}
				<Camera />
			{/if}
		</Button>
	</div>
	{#if scannerActive}
		<div id="reader" class="w-96 h-96 bg-black mx-auto my-4 sm:my-6" />
	{/if}
</div>
