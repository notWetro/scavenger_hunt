<script lang="ts">
	import type { Organisator } from '$lib/models/organisator';

	import { Button, Card } from 'flowbite-svelte';
	import { CircleUserIcon, MailIcon } from 'lucide-svelte';

	export let organisator: Organisator;

	const subject = 'Scavenger Hunt Access';
</script>

<Card class="w-40">
	<div class="flex flex-col items-center gap-2">
		<p class="font-semibold underline">{organisator.name}</p>
		<CircleUserIcon class="size-16" strokeWidth="1" />
		<Button color="alternative">
			<a href={`mailto:${organisator.email}?subject=${subject}`}>
				<MailIcon class="text-primary-800" />
			</a>
		</Button>
	</div>
</Card>
