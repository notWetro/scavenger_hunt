<script lang="ts">
	import type { Hunt } from '$lib/models/Hunt';

	export let hunt: Hunt;
	import { Eye, Edit } from 'lucide-svelte';
</script>

<div class="card w-96 bg-base-100 shadow-xl rounded-xl">
	<img
		src="/src/lib/images/scav-hunt-icon.jpg"
		alt="scav-hunt-logo"
		class="object-fill h-48 rounded-t-xl"
	/>
	<div class="card-body">
		<h2 class="card-title">{hunt.title}</h2>
		<p>{hunt.description}</p>
		<div class="card-actions justify-end">
			<button class="btn btn-primary">
				<Edit />
				Edit
			</button>
			<button class="btn btn-neutral">
				<Eye />
				View
			</button>
		</div>
	</div>
</div>
