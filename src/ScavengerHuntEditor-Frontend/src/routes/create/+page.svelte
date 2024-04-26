<script lang="ts">
	import { ArrowBigRight } from 'lucide-svelte';
	let counter: number = 1;
</script>

<ul class="steps steps-horizontal">
	<li class={`step ${counter >= 1 ? "step-primary" : ""}`}>Basisdaten</li>
	<li class={`step ${counter >= 2 ? "step-primary" : ""}`}>Aufgaben & Lösungen</li>
	<li class={`step ${counter >= 3 ? "step-primary" : ""}`}>Erstellen</li>
</ul>

<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch w-full">

	{#if counter === 1}
	<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch w-full">
		<input type="text" placeholder="Titel der Schnitzeljagd" class="input input-bordered w-full max-w-lg input-lg" />
		<textarea placeholder="Beschreibung der Schnitzeljagd" class="textarea textarea-bordered textarea-lg w-full max-w-lg" ></textarea>
	</div>
	{/if}

	{#if counter === 2}

		<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch max-w-lg">
			<div class="grid gap-10 grid-cols-3">
				<div class="col-start-1 row-start-1 row-span-2 bg-amber-300 rounded-b">
					<ul>
						<li>Aufgabe 1</li>
						<li>Aufgabe 2</li>
						<li>Aufgabe 3</li>
					</ul>
				</div>
				<div class="col-start-2 col-span-2 row-start-1 bg-amber-300 rounded-b mt-3 gap-4">
					<label for="select">Hinweis</label>
					<select id="select" class="select select-bordered select-lg w-full max-w-xs">
						<option disabled selected>Hinweis-Typ</option>
						<option>Text</option>
						<option>Bild</option>
						<option>Objekt</option>
					</select>
					<textarea placeholder="Hinweis hier eingeben" class="textarea textarea-bordered textarea-lg w-full max-w-lg" ></textarea>
				</div>
				<div class="col-start-2 col-span-2 row-start-2 bg-amber-300 rounded-b">
						<label>Lösung</label>

						<select class="select select-bordered select-lg w-full max-w-xs">
						<option disabled selected>Lösungs-Typ</option>
						<option>Standort</option>
						<option>Textantwort</option>
						<option>QR-Code</option>
					</select>
					<textarea placeholder="Gewünschte Lösung hier eingeben" class="textarea textarea-bordered textarea-lg w-full max-w-lg" ></textarea>
				</div>
			</div>
		</div>

	{/if}

	<button class="btn btn-primary btn-block max-w-lg" on:click={() => counter += 1}>
		Weiter
		<ArrowBigRight/>
	</button>
</div>

<!--{#if counter === 2}

	<aside>

	</aside>
	<main>

	</main>


	<div class="mt-3 flex flex-col gap-4 items-center justify-items-stretch w-full">
		<div class="col-span-1 bg-gray-200">A</div>
		<div class="col-span-2 bg-gray-300">
			<div class="grid grid-rows-2 gap-4">
				<div class="row-span-1 bg-gray-400">B</div>
				<div class="row-span-1 bg-gray-500">C</div>
			</div>
		</div>
	</div>

{/if}-->