<script>
	import '../app.css';
	import Navbar from '$lib/components/Navbar.svelte';
</script>

<div class="flex flex-col min-h-screen">
	<header>
		<Navbar />
	</header>

	<main
		class="flex-1 flex flex-col justify-center content-center bg-primary-50 w-full lg:pl-40 lg:pr-40 ml-auto mr-auto mt-0 mb-0 dark:bg-gray-800"
	>
		<slot />
	</main>

	<footer>
		<div class="w-full sm:mx-auto p-4 bg-primary-50">
			<hr class="my-6 border-gray-300 sm:mx-auto lg:mx-40 dark:border-gray-700" />
			<span class="block text-sm text-center">by Lukas & Rosario. @HS-AA</span>
			<span class="block text-sm text-center"
				>© 2024 <a class="hover:underline" href="/">Schnitzeljagd Editor</a></span
			>
		</div>
	</footer>
</div>
