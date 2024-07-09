<script lang="ts">
	import type { Hunt } from '$lib/models/Hunt';

	export let hunt: Hunt;
	import { Eye, Edit } from 'lucide-svelte';
	import { Button, Card } from 'flowbite-svelte';
</script>

<Card class="w-96 bg-base-100 shadow-xl rounded-xl">
	<img src="/src/lib/images/scav-hunt-icon.jpg" alt="scav-hunt-logo" class="object-fill" />
	<div class="card-body">
		<h2 class="text-xl font-bold text-black">{hunt.title}</h2>
		<p>{hunt.description}</p>
		<div class="card-actions justify-end">
			<Button>
				<Edit />
				Edit
			</Button>
			<Button>
				<Eye />
				View
			</Button>
		</div>
	</div>
</Card>
