<script lang="ts">
	import { images } from '$lib/images/images';
	import { Carousel, Timeline, TimelineItem } from 'flowbite-svelte';
	import { Button } from 'flowbite-svelte';
	import { ArrowRightIcon } from 'lucide-svelte';
</script>

<div>
	<Carousel {images} duration={3000} let:Indicators>
		<Indicators />
	</Carousel>
</div>

<div class="m-4 text-end">
	<h1 class="text-xl font-bold mb-8">The Schnitzeljagd Game</h1>
	<p class="font-semibold mb-2">Discover Aalen University in a fun way!</p>
	<p class="mb-4">
		Log in here and experience our interactive scavenger hunt - an exciting way to explore the
		campus and make new friends.
	</p>
	<Button href="/login">
		Log in now!
		<ArrowRightIcon class="ml-4" />
	</Button>
</div>

<div class="w-11/12 m-4">
	<h1 class="text-xl font-bold">Project Roadmap</h1>
	<Timeline>
		<TimelineItem title="Project Start" date="February 2024">
			<p class="mb-4 text-base font-normal text-gray-500 dark:text-gray-400">
				First ideas, wireframing and workflows.
			</p>
		</TimelineItem>
		<TimelineItem title="Architecting" date="March 2024">
			<p class="text-base font-normal text-gray-500 dark:text-gray-400">
				Architectural design, technological exploration and first insight.
			</p>
		</TimelineItem>
		<TimelineItem title="Implementation" date="June 2024">
			<p class="text-base font-normal text-gray-500 dark:text-gray-400">First implementation.</p>
		</TimelineItem>
		<TimelineItem title="Submission" date="August 2024">
			<p class="text-base font-normal text-gray-500 dark:text-gray-400">Project submission.</p>
		</TimelineItem>
	</Timeline>
</div>

<style>
	.button-container {
		text-align: center;
	}
</style>
