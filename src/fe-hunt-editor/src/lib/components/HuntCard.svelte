<script lang="ts">
	import type { Hunt } from '$lib/models/Hunt';
	import { Edit, Trash, Share } from 'lucide-svelte';
	import { Button, Card } from 'flowbite-svelte';
	import { PUBLIC_API_URL } from '$env/static/public';
	import { createEventDispatcher } from 'svelte';
	import { onMount } from 'svelte';

	export let hunt: Hunt;
	const dispatch = createEventDispatcher();

	let showShareModal: boolean = false;
	let lastFocusedElement: HTMLElement | null = null; // Fokusmanagement

	// Funktion zum Löschen eines Hunts
	async function deleteHunt() {
		try {
			const response = await fetch(`${PUBLIC_API_URL}/hunts/${hunt.id}`, {
				method: 'DELETE',
			});
			if (!response.ok) throw new Error('Fehler beim Löschen');
			console.log('Hunt deleted successfully');
			dispatch('HuntDeleted');
		} catch (error) {
			console.error('Error deleting hunt:', error);
		}
	}

	// Modal öffnen und Fokus setzen
	function showShareWindow() {
		lastFocusedElement = document.activeElement as HTMLElement;
		showShareModal = true;
		onMount(() => {
			document.querySelector('.modal button')?.focus();
		});
	}

	// Modal schließen und Fokus zurücksetzen
	function closeShareWindow() {
		showShareModal = false;
		lastFocusedElement?.focus();
	}
</script>



<Card
	class="flex flex-col justify-between w-[22.5rem] bg-base-100 shadow-lg rounded-xl md:w-96"
	aria-label="Hunt Card"
>
	<div>
		<img
			src="/src/lib/images/scav-hunt-icon.jpg"
			alt="Scavenger Hunt Logo"
			class="object-fill"
		/>
		<div class="p-4">
			<h2 class="text-xl font-bold text-black">{hunt.title}</h2>
			<p>{hunt.description}</p>
		</div>
	</div>
	<div class="flex justify-end gap-2">
		<Button
			aria-label="Teilen"
			on:click={showShareWindow}
		>
			<Share class="mr-1" />
			Share
		</Button>
		<Button
			href={`/edit/${hunt.id}`}
			aria-label="Bearbeiten"
		>
			<Edit class="mr-1" />
			Edit
		</Button>
		<Button
			aria-label="Löschen"
			on:click={deleteHunt}
		>
			<Trash class="mr-1" />
			Delete
		</Button>
	</div>
</Card>

{#if showShareModal}
	<div
		class="backdrop"
		aria-hidden="true"
		on:click={closeShareWindow}
	></div>

	<div
		class="modal"
		role="dialog"
		aria-labelledby="modal-title"
		aria-describedby="modal-description"
	>
		<!-- Schließen-Button oben rechts -->
		<button 
			class="close-button" 
			aria-label="Fenster schließen" 
			on:click={closeShareWindow}
		>
			&times;
		</button>

		<!-- Die beiden Buttons in der Mitte -->
		<div class="button-container">
			<Button color="blue" class="long-button">Blue</Button>
			<Button color="blue" class="long-button">Blue</Button>
		</div>
	</div>
{/if}


<style>
	.modal {
		position: fixed;
		top: 50%;
		left: 50%;
		transform: translate(-50%, -50%);
		background: white;
		padding: 3rem;
		box-shadow: 0 8px 15px rgba(0, 0, 0, 0.2);
		z-index: 1000;
		display: flex;
		flex-direction: column;
		align-items: center;
		border-radius: 12px;
		width: 80%;
		max-width: 600px;
		outline: none;
	}

	.backdrop {
		position: fixed;
		top: 0;
		left: 0;
		width: 100%;
		height: 100%;
		background: rgba(0, 0, 0, 0.6);
		z-index: 999;
	}

	/* Schließen-Button oben rechts */
	.close-button {
		position: absolute;
		top: 10px;  /* Etwas mehr Abstand nach unten */
		right: 10px; /* Etwas mehr Abstand zum rechten Rand */
		width: 30px;  /* Breite des Buttons */
		height: 30px; /* Höhe des Buttons, gleich der Breite */
		background: red;
		border: black;
		font-size: 1.5rem;  
		color: #fff;
		cursor: pointer;
		display: flex;
		justify-content: center; /* Kreuz horizontal zentrieren */
		align-items: center; /* Kreuz vertikal zentrieren */
	}

	/* Container für die Buttons */
	.button-container {
		display: flex;
		flex-direction: column; /* Buttons untereinander anordnen */
		gap: 1rem; /* Abstand zwischen den Buttons */
		width: 100%; /* Volle Breite des Containers */
	}

	/* Anpassung für längere Buttons */
	.long-button {
		width: 100%; /* Breite auf 100% setzen, um die Buttons länger zu machen */
	}

	/* Optional: Anpassung der Buttonfarbe */
	Button {
		padding: 1rem;
		border-radius: 8px;
	}
</style>
