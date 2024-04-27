<script lang="ts">
	import { ArrowBigLeft, ArrowBigRight } from "lucide-svelte";

    export let counter:number;
</script>

<div class="flex flex-row gap-2 justify-center max-w-lg">
    <button class="btn btn-primary btn-lg w-full" on:click={() => counter -= 1}>
        <ArrowBigLeft/>
        Zurück
    </button>
    
    <button class="btn btn-primary btn-lg w-full" on:click={() => counter += 1}>
        Weiter
        <ArrowBigRight/>
    </button>
</div>