<script lang="ts">
	import jsQR from 'jsqr';
	import { createEventDispatcher, onMount } from 'svelte';
	import type { QRCodePosition } from './QRCodePosition';

	let dispatch = createEventDispatcher();

	export let data: string = '';
	export let displayText: string = 'Success!';

	let isDataVisible: boolean = false;

	let canvas: HTMLCanvasElement;
	let video: HTMLVideoElement;
	let qrCodePosition: QRCodePosition = {
		topLeft: { x: 0, y: 0 },
		topRight: { x: 0, y: 0 },
		bottomRight: { x: 0, y: 0 },
		bottomLeft: { x: 0, y: 0 }
	};

	let overlayStyle = '';

	let clientWidth: number;
	let clientHeight: number;

	$: {
		console.log(clientHeight);
		console.log(clientWidth);
	}

	onMount(() => {
		video = document.createElement('video');

		navigator.mediaDevices.getUserMedia({ video: { facingMode: 'environment' } }).then((stream) => {
			video.srcObject = stream;
			video.setAttribute('playsinline', 'true');
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
					updateOverlayStyle();
					data = code.data;
					isDataVisible = true;
					dispatch('Finished');
				} else {
					isDataVisible = false;
				}
			}
		}
		requestAnimationFrame(tick);
	}

	function updateOverlayStyle() {
		const { topLeft, topRight, bottomRight, bottomLeft } = qrCodePosition;
		const width = Math.sqrt((topRight.x - topLeft.x) ** 2 + (topRight.y - topLeft.y) ** 2);
		const height = Math.sqrt((bottomLeft.x - topLeft.x) ** 2 + (bottomLeft.y - topLeft.y) ** 2);

		overlayStyle = `
			transform: translate(${topLeft.x}px, ${topLeft.y}px);
			width: ${width}px;
			height: ${height}px;
		`;
	}
</script>

<div class="container grid">
	<canvas class="grid-area-custom" bind:this={canvas} />
	{#if isDataVisible}
		<div
			class="grid-area-custom border-2 border-solid border-primary-600 bg-white bg-opacity-85 text-black text-center"
			style={overlayStyle}
		>
			{displayText}
		</div>
	{/if}
</div>
