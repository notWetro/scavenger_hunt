<script lang="ts">
  import { PUBLIC_API_URL } from '$env/static/public';
  import CheerDisplay from '$lib/components/CheerDisplay.svelte';
  import HintDisplay from '$lib/components/hints/HintDisplay.svelte';
  import SolutionFormSelector from '$lib/components/solutions/SolutionFormSelector.svelte';
  import SolutionTypeHintDisplay from '$lib/components/solutions/SolutionTypeHintDisplay.svelte';
  import type { HuntAssignmentResponse } from '$lib/dtos/assignment/HuntAssignmentResponse';
  import type { HuntLoginResponse } from '$lib/dtos/login/huntLoginResponse';
  import { completeHunts } from '$lib/stores/completeHunts';
  import { playingHunt } from '$lib/stores/playingHunt';
  import { ongoingHunts } from '$lib/stores/ongoingHunts';
  import { token } from '$lib/stores/tokenStore';
  import { Button } from 'flowbite-svelte';
  import { onMount } from 'svelte';
  import { fade } from 'svelte/transition';
  import { tick } from 'svelte';

  let currentHunt: HuntLoginResponse;
  let currentAssignment: HuntAssignmentResponse;  
  let solutionData: string = '';

  let isSolutionShown: boolean = false;
  let isNextButtonShown: boolean = false;
  let isFinished: boolean = false;
  let solutionCardRef: HTMLDivElement;

  onMount(() => {
    currentHunt = $playingHunt;
    fetchCurrentAssignment();
  });

  // Toggles the solution card and autoscrolles to the card.
  async function toggleSolutionInput() {
  	isSolutionShown = !isSolutionShown;
	if (isSolutionShown) {
		await tick();
		if (solutionCardRef) {
			solutionCardRef.scrollIntoView({ behavior: 'smooth', block: 'center' });
		}
	}
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
      completeHunts.update((hunts) => {
            if (!hunts.some(hunt => hunt.id === currentHunt.id)) {
                hunts.push(currentHunt);
            }
            return hunts;
            playingHunt.set(null);
        });
        ongoingHunts.update((hunts) => {
          return hunts.filter(hunt => hunt.id !== currentHunt.id);
        });
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
    <h1 class="text-3xl font-bold mb-4 text-center">
      <a href="/home">{currentHunt.title}</a>
    </h1>
    <hr class="border-t-2 border-gray-300 my-4" />
  {/if}

  {#if isFinished}
    <CheerDisplay />
  {:else if currentAssignment}
    <div class="flex flex-col gap-2" transition:fade={{ delay: 0, duration: 250 }}>
      {#if currentAssignment.hintType === 1 || currentAssignment.hintType === 2 && currentAssignment.additionalData !== null}
        <h1 class="text-center text-lg font-semibold">{currentAssignment.additionalData}</h1>
      {/if}

      <HintDisplay bind:type={currentAssignment.hintType} bind:data={currentAssignment.hintData} />

      <SolutionTypeHintDisplay bind:type={currentAssignment.solutionType} />
      <div class="flex flex-col" transition:fade={{ delay: 2000, duration: 250 }}>
        <Button on:click={toggleSolutionInput}>Answer found!</Button>
      </div>

      {#if isSolutionShown}
        <div transition:fade={{ delay: 0, duration: 300 }}>
          <div bind:this={solutionCardRef}>
            <SolutionFormSelector
              bind:type={currentAssignment.solutionType}
              bind:data={solutionData}
              on:OnNext={showNextButton}
            />
          </div>
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
