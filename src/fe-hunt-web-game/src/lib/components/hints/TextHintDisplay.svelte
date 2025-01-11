<script lang="ts">
	import { Card } from 'flowbite-svelte';
	import { FileType2Icon } from 'lucide-svelte';
	export let data: string = '';
</script>

<Card class="p-4 mb-4 w-full max-w-none mx-auto">
	<div class="flex flex-row items-center text-center">
		<FileType2Icon class="w-8 h-8 mb-2 mr-2 text-primary-800" />
		<h5
			class="mb-2 text-xl font-semibold underline underline-offset-4 tracking-tight text-primary-800"
		>
			Current Hint
		</h5>
	</div>
	<p class="p-4">{data}</p>
</Card>
