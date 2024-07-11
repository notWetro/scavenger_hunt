<script lang="ts">
	import type { Hunt } from '$lib/models/Hunt';

	export let hunt: Hunt;
	import { Edit } from 'lucide-svelte';
	import { Button, Card } from 'flowbite-svelte';
</script>

<Card class="w-96 bg-base-100 shadow-xl rounded-xl">
	<img src="/src/lib/images/scav-hunt-icon.jpg" alt="scav-hunt-logo" class="object-fill" />
	<div class="">
		<h2 class="text-xl font-bold text-black">{hunt.title}</h2>
		<p>{hunt.description}</p>
		<div class="flex justify-end">
			<Button href="/edit/{hunt.id}">
				<Edit />
				Edit
			</Button>
		</div>
	</div>
</Card>
