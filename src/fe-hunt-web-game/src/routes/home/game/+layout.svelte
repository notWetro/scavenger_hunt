<script lang="ts">
  import { goto } from '$app/navigation';
  import { Button} from 'flowbite-svelte';
</script>

<div class="relative  min-h-screen bg-gray-50 w-4/5 mx-auto">
  <div class="relative top-2 right-2">
	<!-- New: Home Button -->
	<Button 
		on:click={() => goto('/home')}
		class="absolute top-0 right-0"
	>
  		Home 🏠
	</Button>
  </div>

  <main class="p-4">
    <slot />
  </main>
</div>
