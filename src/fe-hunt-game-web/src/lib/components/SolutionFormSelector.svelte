<script lang="ts">
	import QrCodeSelector from './QrCodeSelector.svelte';

	export let type: number;
	export let data: string;
</script>

{#if type === 0}
	<!-- Type 0 means Qr-Code -->
	<!-- <QrCodeSelector bind:data /> -->
{:else if type === 1}
	<!-- Type 1 means Text -->
{:else if type === 2}
	<!-- Type 2 means Location -->
	<h1>There should be an object here but it's not implemented.</h1>
{:else}
	<!-- This Type shouldn't exist -->
	<span>Hm. Something ain't right and it's not your fault.</span>
{/if}
