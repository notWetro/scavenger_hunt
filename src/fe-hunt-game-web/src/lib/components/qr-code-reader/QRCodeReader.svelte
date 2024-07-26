<script lang="ts">
	import jsQR from 'jsqr';
	import { onMount } from 'svelte';
	import type { Point } from './Point';
	import type { QRCodePosition } from './QRCodePosition';

	// Define the Point type and QRCodePosition interface

	let canvas: HTMLCanvasElement;
	let video: HTMLVideoElement;
	let qrCodePosition: QRCodePosition = {
		topLeft: { x: 0, y: 0 },
		topRight: { x: 0, y: 0 },
		bottomRight: { x: 0, y: 0 },
		bottomLeft: { x: 0, y: 0 }
	};

	onMount(() => {
		video = document.createElement('video');

		navigator.mediaDevices.getUserMedia({ video: { facingMode: 'environment' } }).then((stream) => {
			video.srcObject = stream;
			video.setAttribute('playsinline', 'true'); // required to tell iOS safari we don't want fullscreen
			video.play();
			requestAnimationFrame(tick);
		});
	});

	function tick() {
		if (video.readyState === video.HAVE_ENOUGH_DATA) {
			const context = canvas.getContext('2d');
			if (context) {
				canvas.width = video.videoWidth;
				canvas.height = video.videoHeight;
				context.drawImage(video, 0, 0, canvas.width, canvas.height);
				const imageData = context.getImageData(0, 0, canvas.width, canvas.height);
				const code = jsQR(imageData.data, canvas.width, canvas.height, {
					inversionAttempts: 'dontInvert'
				});

				if (code) {
					qrCodePosition = {
						topLeft: code.location.topLeftCorner,
						topRight: code.location.topRightCorner,
						bottomRight: code.location.bottomRightCorner,
						bottomLeft: code.location.bottomLeftCorner
					};
					console.log(code.data);
				}
			}
		}
		requestAnimationFrame(tick);
	}
</script>

<div class="qr-container">
	<canvas bind:this={canvas}></canvas>
	<div
		class="qr-overlay"
		style="
        top: {qrCodePosition.topLeft.y}px;
        left: {qrCodePosition.topLeft.x}px;
        width: {qrCodePosition.topRight.x - qrCodePosition.topLeft.x}px;
        height: {qrCodePosition.bottomLeft.y - qrCodePosition.topLeft.y}px;
      "
	>
		Hello
	</div>
</div>

<style>
	.qr-overlay {
		position: absolute;
		border: 2px solid white;
		background-color: rgba(255, 255, 255, 0.8);
		color: black;
		padding: 10px;
		text-align: center;
	}
	.qr-container {
		position: relative;
	}
</style>
