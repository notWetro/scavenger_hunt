<script lang="ts">
	import MessageCircleDashed from 'lucide-svelte/icons/message-circle-dashed';

	function showNotification() {
		window.alert('Hello there!');
	}
</script>

<main class="p-4">
	<div class="flex flex-col justify-center">
		<h1 class="text-2xl font-bold text-center">Hello, world!</h1>
		<button class="btn btn-primary" on:click={showNotification}>
			<MessageCircleDashed />
			Press me
		</button>
	</div>
</main>
