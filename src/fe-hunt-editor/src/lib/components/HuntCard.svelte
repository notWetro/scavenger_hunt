<script lang="ts">
	import type { Hunt } from '$lib/models/Hunt';
	import { Edit, Trash, Share, Copy, Download } from 'lucide-svelte';
	import { Button, Card, Modal } from 'flowbite-svelte';
	import { PUBLIC_API_URL } from '$env/static/public';
	import { createEventDispatcher } from 'svelte';
	import { onMount } from 'svelte';
	import { goto } from '$app/navigation';
	import QRCode from 'qrcode';
	import scavHuntIcon from '$lib/images/scavHuntIcon.jpg';

	export let hunt: Hunt;
	const dispatch = createEventDispatcher();

	let showShareModal: boolean = false;

	const baseUrl = window.location.origin;
	
	const parts = baseUrl.split(":");
	let port = parts[2];

	if (port && port.startsWith("5")) {
		port = "4" + port.slice(1);
	}

	let link: string = `http://${parts[1]}:${port}:/participation/${hunt.id}`;

	/**
	 * Fetches the public IP address from the backend and updates the link.
	 */
	async function getPublicIP() {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/hunts/server-ip`);
			const data = await response.json();
			console.log('Ip Adresse:', data.ip);
			link = `http://${data.ip}:${port}/participation/${hunt.id}`;
		} catch (error) {
			console.log('Error fetching the IP Adresse:', error);
		}
	}

	getPublicIP();

	/**
	 * Deletes the hunt from the backend and dispatches the HuntDeleted event.
	 */
	async function deleteHunt() {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/hunts/${hunt.id}`, {
				method: 'DELETE',
			});
			if (!response.ok) throw new Error('Error deleting the hunt');
				console.log('Hunt deleted successfully');
				dispatch('HuntDeleted');
		} catch (error) {
			console.error('Error deleting hunt:', error);
		}
	}

	/**
	 * Downloads the QR code for the hunt as an image file.
	 * @param id - The ID of the hunt.
	 * @param qrUrl - The URL to be encoded in the QR code.
	 */
	async function downloadQRCode(id: number, qrUrl: string) {
		try {
			const qrCodeDataUrl = await QRCode.toDataURL(qrUrl, {
				width: 600
			});
			const response = await fetch(qrCodeDataUrl);
			const blob = await response.blob();
			const downloadUrl = window.URL.createObjectURL(blob);
			const a = document.createElement('a');
			a.style.display = 'none';
			a.href = downloadUrl;
			a.download = `hunt-${hunt.title}-qr-code.png`;
			document.body.appendChild(a);
			a.click();
			window.URL.revokeObjectURL(downloadUrl);
		} catch (error) {
			console.error('Error downloading QR code:', error);
		}
	}

	/**
	 * Copies the provided URL to the clipboard.
	 * @param url - The URL to be copied.
	 */
	async function copyURLToClipboard(url: string) {
		try {
			await navigator.clipboard.writeText(url);
			alert('URL wurde in die Zwischenablage kopiert!');
		} catch (error) {
			console.error('Error Copying the URL', error);
		}
	}
</script>



<Card
	class="flex flex-col justify-between w-[22.5rem] bg-base-100 shadow-lg rounded-xl md:w-96"
	aria-label="Hunt Card"
>
	<div>
		<img
			src={scavHuntIcon}
			alt="Scavenger Hunt Logo"
			class="object-fill"
		/>
		<div class="p-4">
			<h2 class="text-xl font-bold text-black">{hunt.title}</h2>
			<p>{hunt.description}</p>
		</div>
	</div>
	<div class="flex justify-end gap-2">
		<Button on:click={() => (showShareModal = true)}>
			<Share class="mr-1" />
			Share
		</Button>
		<Button on:click={() => goto(`/edit/${hunt.id}`)}>
			<Edit class="mr-1" />
			Edit
		</Button>
		<Button on:click={deleteHunt}>
			<Trash class="mr-1" />
			Delete
		</Button>
	</div>
</Card>

<Modal title="Share the Hunt {hunt.title}" bind:open={showShareModal} size="xs" autoclose outsideclose>
  <div class="flex flex-col gap-2">
    	<h3 class="mb-5 text-lg font-normal text-gray-500 dark:text-gray-400">How do you want to share the hunt?</h3>
		<Button on:click= {() => downloadQRCode(hunt.id, link)} color="blue" class="me-2"> <Download class="mr-1" /> Download the QR Code </Button>
    	<Button on:click={() => copyURLToClipboard(link)} color="blue" class="me-2"> <Copy class="mr-1" /> Copy the link to play</Button>
	</div>
</Modal>