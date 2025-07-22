<script>
  	import '../../app.css';
	import { Button } from 'flowbite-svelte'; 
    import { goto } from '$app/navigation'; 

	/**
	 * Navigates the user to the home page.
	 */
</script>

<div
    class="relative flex flex-col justify-center items-center min-h-screen bg-gradient-to-tr from-violet-300 via-sky-300 to-violet-100"
>
    <Button
        on:click={() => goto('/')}
        class="absolute top-4 right-4"
    >
        Home 🏠
    </Button>

    <div class="container xl:m-4 xl:rounded-xl p-5 bg-white bg-opacity-80">
        <slot />
    </div>
</div>
