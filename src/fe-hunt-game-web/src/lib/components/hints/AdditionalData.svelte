<script lang="ts">
  export let type: number;
  export let data: string;
</script>

{#if type === 0}
  <p>{data}</p> <!-- Text-Data -->
{:else if type === 1}
  <img src={data} alt="Additional Image" />
{:else if type === 2}
  <video src={data} controls muted>
    Your browser does not support the video tag.
  </video>
{:else if type === 3}
  <audio src={data} controls muted>
    Your browser does not support the audio tag.
  </audio>
{:else}
  <span>Invalid type</span>
{/if}
