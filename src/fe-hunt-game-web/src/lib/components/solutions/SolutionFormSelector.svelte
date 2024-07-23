<script lang="ts">
	import { Alert } from 'flowbite-svelte';
	import QrCodeSolutionForm from './QrCodeSolutionForm.svelte';
	import TextSolutionForm from './TextSolutionForm.svelte';
	import LocationSolutionForm from './LocationSolutionForm.svelte';
	import { playingHunt } from '$lib/stores/playingHunt';
	import { PUBLIC_API_URL } from '$env/static/public';
	import { token } from '$lib/stores/tokenStore';
	import { createEventDispatcher } from 'svelte';
	import SubmissionHintDisplay from './SubmissionHintDisplay.svelte';

	const dispatch = createEventDispatcher();

	enum SubmissionStatus {
		NotSubmitted,
		InvalidSubmission,
		ValidSubmission
	}

	export let type: number;
	export let data: string;
	let hintData: string = '';

	let submissionStatus: SubmissionStatus = SubmissionStatus.NotSubmitted;

	async function submitSolution() {
		if (!data) throw Error('data is not defined!');

		const huntId = $playingHunt.id;
		const token = $token;
		console.log('huntId = ', huntId);
		console.log('token = ', token);
		console.log('data = ', data);

		const response = await fetch(
			`${PUBLIC_API_URL}/Participants/SubmitSolution/${$playingHunt.id}`,
			{
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: $token
				},
				body: JSON.stringify({ data })
			}
		);

		if (response.ok) {
			let responseStatus = await response.json();

			if (responseStatus.success === true) {
				submissionStatus = SubmissionStatus.ValidSubmission;
				dispatch('OnNext');
				return;
			} else {
				submissionStatus = SubmissionStatus.InvalidSubmission;
				hintData = responseStatus.hintData;
				console.log(hintData);
			}
		}
	}
</script>

<h3 class="text-lg font-semibold mb-4 text-center">Submit a solution</h3>

{#if type === 0}
	<!-- Type 0 means Qr-Code -->
	<QrCodeSolutionForm bind:data on:SubmitData={submitSolution} />
{:else if type === 1}
	<!-- Type 1 means Text -->
	<TextSolutionForm bind:data on:SubmitData={submitSolution} />
{:else if type === 2}
	<!-- Type 2 means Location -->
	<LocationSolutionForm bind:data on:SubmitData={submitSolution} />
{:else}
	<!-- This Type shouldn't exist -->
	<span>Hm. Something ain't right and it's not your fault.</span>
{/if}

{#if submissionStatus === SubmissionStatus.InvalidSubmission}
	<SubmissionHintDisplay {hintData} {type} />
{:else if submissionStatus === SubmissionStatus.ValidSubmission}
	<Alert color="green">You are correct!</Alert>
{/if}
