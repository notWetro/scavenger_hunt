<script lang="ts">
	import { PUBLIC_API_URL } from '$env/static/public';
	import CheerDisplay from '$lib/components/CheerDisplay.svelte';
	import HintDisplay from '$lib/components/hints/HintDisplay.svelte';
	import SolutionFormSelector from '$lib/components/solutions/SolutionFormSelector.svelte';
	import SolutionTypeHintDisplay from '$lib/components/solutions/SolutionTypeHintDisplay.svelte';
	import type { HuntAssignmentResponse } from '$lib/dtos/assignment/HuntAssignmentResponse';
	import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
	import { playingHunt } from '$lib/stores/playingHunt';
	import { token } from '$lib/stores/tokenStore';
	import { Button } from 'flowbite-svelte';
	import { onMount } from 'svelte';
	import { fade } from 'svelte/transition';

	let currentHunt: HuntLoginResponse;
	let currentAssignment: HuntAssignmentResponse;
	let solutionData: string = '';
	$: {
		console.log(solutionData);
	}
	let isSolutionShown: boolean = false;
	let isNextButtonShown: boolean = false;
	let isFinished: boolean = false;

	onMount(() => {
		currentHunt = $playingHunt;
		fetchCurrentAssignment();
	});

	function toggleSolutionInput() {
		isSolutionShown = !isSolutionShown;
	}

	function showNextButton() {
		isNextButtonShown = true;
	}

	async function fetchCurrentAssignment() {
		if (!currentHunt) throw Error('currentHunt is not defined!');

		const response = await fetch(
			`${PUBLIC_API_URL}/Participants/CurrentAssignment/${currentHunt.id}`,
			{
				method: 'GET',
				headers: {
					'Content-Type': 'application/json',
					Authorization: $token
				}
			}
		);

		if (!response.ok) {
			throw Error('An error occured!');
		}

		if (response.status === 269) {
			isFinished = true;
		} else {
			currentAssignment = (await response.json()) as HuntAssignmentResponse;
		}
	}

	function resetState() {
		isSolutionShown = false;
		isNextButtonShown = false;
		solutionData = '';
	}

	async function fetchNextAssignment() {
		resetState();
		await fetchCurrentAssignment();
	}
</script>

<div class="h-screen p-4">
	{#if currentHunt}
		<!-- <Button class="absolute top-4 right-4 bg-none" href="/home">
			<XIcon class="w-6 h-6" />
		</Button> -->
		<h1 class="text-xl font-semibold mb-4 text-center">
			<a href="/home">{currentHunt.title}</a>
		</h1>
	{/if}

	{#if isFinished}
		<CheerDisplay />
	{:else if currentAssignment}
		<div class="flex flex-col gap-2" transition:fade={{ delay: 0, duration: 250 }}>
			<HintDisplay bind:type={currentAssignment.hintType} bind:data={currentAssignment.hintData} />

			<SolutionTypeHintDisplay bind:type={currentAssignment.solutionType} />
			<div class="flex flex-col" transition:fade={{ delay: 2000, duration: 250 }}>
				<Button on:click={toggleSolutionInput}>Answer found!</Button>
			</div>

			{#if isSolutionShown}
				<div transition:fade={{ delay: 0, duration: 300 }}>
					<SolutionFormSelector
						bind:type={currentAssignment.solutionType}
						bind:data={solutionData}
						on:OnNext={showNextButton}
					/>
				</div>
			{/if}

			{#if isNextButtonShown}
				<div class="flex flex-col" transition:fade={{ delay: 0, duration: 250 }}>
					<Button on:click={fetchNextAssignment}>Next hint!</Button>
				</div>
			{/if}
		</div>
	{/if}
</div>
