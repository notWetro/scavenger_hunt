<script lang="ts">
	import { Alert } from 'flowbite-svelte';
	import QrCodeSolutionForm from './QrCodeSolutionForm.svelte';

	enum SubmissionStatus {
		NotSubmitted,
		InvalidSubmission,
		ValidSubmission
	}

	export let type: number;
	export let data: string;

	let submissionStatus: SubmissionStatus = SubmissionStatus.NotSubmitted;
</script>

<h3 class="text-lg font-semibold mb-4 text-center">Submit a solution</h3>

{#if type === 0}
	<!-- Type 0 means Qr-Code -->
	<QrCodeSolutionForm
		bind:data
		on:InvalidSolution={() => {
			submissionStatus = SubmissionStatus.InvalidSubmission;
		}}
		on:ValidSolution={() => {
			submissionStatus = SubmissionStatus.ValidSubmission;
		}}
	/>
{:else if type === 1}
	<!-- Type 1 means Text -->
	<h1>There should be an object here but it's not implemented.</h1>
{:else if type === 2}
	<!-- Type 2 means Location -->
	<h1>There should be an object here but it's not implemented.</h1>
{:else}
	<!-- This Type shouldn't exist -->
	<span>Hm. Something ain't right and it's not your fault.</span>
{/if}

{#if submissionStatus === SubmissionStatus.InvalidSubmission}
	<Alert color="orange">Unfortunately the submission isn't correct! Try again!</Alert>
{:else if submissionStatus === SubmissionStatus.ValidSubmission}
	<Alert color="green">You are correct!</Alert>
{/if}
