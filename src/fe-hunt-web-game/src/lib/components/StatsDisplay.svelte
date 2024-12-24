<script lang="ts">
	import type { Stats } from '$lib/models/stats';
	import { Card } from 'flowbite-svelte';
	import { LandPlotIcon, UsersIcon } from 'lucide-svelte';

	export let stats: Stats;
</script>

<Card class="mb-6">
	<h3 class="text-center mb-4 tracking-tight">We've got...</h3>

	<div class="grid grid-cols-2 divide-x-2 place-content-center gap-2">
		<div class="grid grid-rows-2 items-center justify-center text-center">
			<UsersIcon class="m-1 size-8" />
			<p class="m-1 text-2xl font-extrabold text-primary-700">{stats.participantCount}</p>
			<p class="font-semibold col-span-2 tracking-tight">
				Participant{stats.participantCount > 1 ? 's' : ''}
			</p>
		</div>

		<div class="grid grid-rows-2 items-center justify-center text-center">
			<LandPlotIcon class="m-1 size-8" />
			<p class="m-1 text-2xl font-extrabold text-primary-700">{stats.participationCount}</p>
			<p class="font-semibold col-span-2 tracking-tight">
				Participation{stats.participationCount > 1 ? 's' : ''}
			</p>
		</div>
	</div>
</Card>
