<script lang="ts">
	import TextHintDisplay from './TextHintDisplay.svelte';

	export let type: number;
	export let data: string;
</script>

<!-- New: add Video and Audio as supportet Hints and displays them -->
{#if type === 0}
	<!-- Type 0 means Text -->
	<TextHintDisplay {data} />
{:else if type === 1}
	<!-- Type 1 means Image -->
	<img src={data} alt="Hm. Something ain't right and it's not your fault." />
{:else if type === 2}
	<!-- Type 2 means Video -->
	<video src={data} controls muted>
		Your browser does not support the video tag.
	</video>
{:else if type === 3}
	<!-- Type 3 means Audio -->
	<audio src={data} controls muted>
		Your browser does not support the video tag.
	</audio>
{:else if type === 4}
	<!-- Type 3 means Object -->
	<h1>There should be an object here but it's not implemented.</h1>
{:else}
	<!-- This Type shouldn't exist -->
	<span>Hm. Something ain't right and it's not your fault.</span>
{/if}
