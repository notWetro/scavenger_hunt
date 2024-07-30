<script lang="ts">
	import type { Stats } from '$lib/models/stats';
	import type { PageData } from './$types';
	import StatsDisplay from '$lib/components/StatsDisplay.svelte';
	import type { Organisator } from '$lib/models/organisator';
	import OrganisatorCard from '$lib/components/OrganisatorCard.svelte';

	export let data: PageData;
	$: stats = data.stats as Stats;

	const organisators: Organisator[] = [
		{
			name: 'Marc H.',
			email: 'marc.hermann@hs-aalen.de'
		},
		{
			name: 'Lukas S.',
			email: 'lukas.steckbauer@studmail.htw-aalen.de'
		},
		{
			name: 'Rosario A.',
			email: 'rosario.aranzulla@studmail.htw-aalen.de'
		}
	];
</script>

<main class="p-4 h-screen">
	<div class="flex flex-col justify-center p-2 items-center">
		<h1 class="text-xl font-bold tracking-tight mb-2">Welcome!</h1>

		<p class="mb-4">Join fun and exciting scavenger hunts around your city!</p>

		<StatsDisplay {stats} />

		<h1 class="text-xl font-bold text-center tracking-tight mb-2">How to enter?</h1>

		<p class="mb-2">
			In order to join a Scavenger Hunt you'll need to ask for a participation invite link or
			QR-Code.
		</p>
		<p class="mb-2">Ask an organisator below:</p>
	</div>
	<div class="snap-x overflow-x-auto flex flex-row gap-2 xl:justify-center">
		{#each organisators as organisator}
			<div class="snap-start">
				<OrganisatorCard {organisator} />
			</div>
		{/each}
	</div>
</main>
