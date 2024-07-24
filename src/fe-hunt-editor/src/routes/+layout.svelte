<script>
	import '../app.css';
	import Navbar from '$lib/components/Navbar.svelte';
	import Footer from '$lib/components/Footer.svelte';
</script>

<div class="flex flex-col min-h-screen">
	<header>
		<Navbar />
	</header>

	<main
		class="flex flex-1 flex-col justify-center content-center bg-primary-50 w-full px-5 md:px-6 lg:px-40 mx-auto"
	>
		<slot />
	</main>

	<footer>
		<Footer />
	</footer>
</div>
