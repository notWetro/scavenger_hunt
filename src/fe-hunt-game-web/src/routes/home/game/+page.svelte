<script lang="ts">
	import { PUBLIC_API_URL } from '$env/static/public';
	import HintDisplay from '$lib/components/HintDisplay.svelte';
	import SolutionFormSelector from '$lib/components/SolutionFormSelector.svelte';
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

	onMount(() => {
		currentHunt = $playingHunt;
		fetchCurrentAssignment();
	});

	function showSolutionInput() {
		isSolutionShown = true;
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

		if (response.ok) {
			const responseData = (await response.json()) as HuntAssignmentResponse;
			currentAssignment = responseData;
		} else {
			throw Error('An error occured!');
		}
	}
</script>

{#if currentHunt}
	<h1>{currentHunt.title}</h1>
{/if}

{#if currentAssignment}
	{#if isSolutionShown}
		<div transition:fade={{ delay: 250, duration: 300 }}>
			<SolutionFormSelector bind:type={currentAssignment.solutionType} bind:data={solutionData} />
		</div>
	{:else}
		<div transition:fade={{ delay: 250, duration: 300 }}>
			<HintDisplay bind:type={currentAssignment.hintType} bind:data={currentAssignment.hintData} />

			<Button on:click={showSolutionInput}>Lösung gefunden!</Button>
		</div>
	{/if}
{/if}
