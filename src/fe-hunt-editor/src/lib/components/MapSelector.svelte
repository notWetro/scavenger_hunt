<script lang="ts">
	import { TrashIcon } from 'lucide-svelte';
	import {
		Control,
		ControlButton,
		ControlGroup,
		DefaultMarker,
		MapEvents,
		MapLibre
	} from 'svelte-maplibre';
	import { createEventDispatcher, onMount } from 'svelte';

	onMount(() => {
		if (textValue !== '') {
			lat = parseFloat(textValue.split(';')[0]);
			lng = parseFloat(textValue.split(';')[1]);
			marker = {
				lngLat: [lng, lat]
			};
		}
		console.log(textValue);
		console.log(lat);
		console.log(lng);
	});

	let marker: any | null;

	const dispatch = createEventDispatcher();

	export let textValue: string = '';

	export let lat: number | null = null;
	export let lng: number | null = null;
	$: {
		if (marker) {
			lat = marker.lngLat.lat;
			lng = marker.lngLat.lng;
			textValue = `${lat};${lng}`;
		}
	}

	export function addMarker(e: CustomEvent) {
		marker = { lngLat: e.detail.lngLat };
		dispatch('markerAdded');
	}

	function removeMarker() {
		marker = null;
		lat = null;
		lng = null;
	}
</script>

<MapLibre
	style="https://basemaps.cartocdn.com/gl/positron-gl-style/style.json"
	class="relative aspect-[9/16] max-h-[70vh] w-full sm:aspect-video sm:max-h-full"
	center={[10.07, 48.84]}
	standardControls
	zoom={15.5}
	pitch={30}
>
	<!-- MapEvents gives you access to map events even from other components inside the map,
  where you might not have access to the top-level `MapLibre` component. In this case
  it would also work to just use on:click on the MapLibre component itself. -->
	<MapEvents on:click={addMarker} />

	{#if marker}
		<DefaultMarker lngLat={marker.lngLat} />
	{/if}

	<Control class="flex flex-col gap-y-2">
		<ControlGroup>
			<ControlButton on:click={removeMarker}>
				<TrashIcon />
			</ControlButton>
		</ControlGroup>
	</Control>
</MapLibre>
