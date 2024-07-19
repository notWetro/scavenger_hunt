<script>
	import Header from './Header.svelte';
	import './styles.css';
	import '../app.css';
</script>

<div class="flex flex-col min-h-screen">
	<Header />

	<main class="flex flex-col flex-grow p-4 w-full max-w-full mx-auto box-border">
		<slot />
	</main>

	<div class="divider mx-auto my-2 w-7/12" />

	<footer class="flex flex-col items-center justify-center pb-2 text-base-content">
		<p>Scavenger Hunt Editor for AR App @HS-AA</p>
	</footer>
</div>

<style>
</style>
