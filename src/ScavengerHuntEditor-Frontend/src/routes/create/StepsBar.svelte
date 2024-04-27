<script lang="ts">
    export let counter:number;
</script>

<ul class="steps steps-horizontal">
	<li class={`step ${counter >= 1 ? "step-primary" : ""}`}>Basisdaten</li>
	<li class={`step ${counter >= 2 ? "step-primary" : ""}`}>Aufgaben & Lösungen</li>
	<li class={`step ${counter >= 3 ? "step-primary" : ""}`}>Erstellen</li>
</ul>