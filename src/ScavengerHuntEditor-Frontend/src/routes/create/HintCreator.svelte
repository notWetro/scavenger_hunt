<script lang="ts">
	enum HintType {
		Text = 'Text',
		Image = 'Bild'
	}

	let hintType: HintType;
</script>

<div
	class="overflow-x-auto scroll-m-0 card bg-base-200 justify-items-stretch h-full w-full gap-2 p-2 shadow-xl"
>
	<label class="hidden" for="select">Hinweis</label>

	<select bind:value={hintType} class="select select-bordered select-lg w-full" id="select">
		<option disabled selected>Hinweis-Typ</option>
		{#each Object.values(HintType) as type}
			<option>{type}</option>
		{/each}
	</select>

	{#if hintType === HintType.Image}
		<input type="file" class="file-input file-input-bordered file-input-primary w-full" />
	{/if}

	{#if hintType === HintType.Text}
		<textarea
			class="textarea textarea-bordered textarea-lg w-full"
			placeholder="Hinweis hier eingeben"
		></textarea>
	{/if}
</div>
