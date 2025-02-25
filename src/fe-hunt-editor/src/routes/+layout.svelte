<script>
	/**
	 * Import the global CSS file.
	 */
	import '../app.css';
	/**
	 * Import the Navbar component which is displayed at the top of the page.
	 */
	import Navbar from '$lib/components/Navbar.svelte';
	/**
	 * Import the Footer component which is displayed at the bottom of the page.
	 */
	import Footer from '$lib/components/Footer.svelte';
</script>

<div class="flex flex-col min-h-screen">
	<header>
		<!-- Render the Navbar component -->
		<Navbar />
	</header>

	<main
		class="flex flex-1 flex-col justify-center content-center bg-primary-50 w-full px-5 md:px-6 lg:px-40 mx-auto"
	>
		<!-- Slot for rendering the main content of the page -->
		<slot />
	</main>

	<footer>
		<!-- Render the Footer component -->
		<Footer />
	</footer>
</div>
