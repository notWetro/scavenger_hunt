<script>
	import { PlusIcon } from 'lucide-svelte';
</script>

<div
	class="overflow-x-auto scroll-m-0 card bg-base-100 justify-items-stretch h-full w-full shadow-xl"
>
	<table class="table">
		<!-- head -->
		<thead>
			<tr>
				<th>Nr</th>
				<th>Hinweis</th>
				<th>Lösung</th>
			</tr>
		</thead>
		<tbody>
			<tr class="hover">
				<th>1</th>
				<td>Text</td>
				<td>QR</td>
			</tr>
			<tr class="hover">
				<th>2</th>
				<td>Bild</td>
				<td>Standort</td>
			</tr>
			<tr class="hover">
				<th>3</th>
				<td>Objekt</td>
				<td>Text</td>
			</tr>
			<tr class="hover">
				<th>4</th>
				<td>Text</td>
				<td>QR</td>
			</tr>
			<tr class="hover">
				<th>5</th>
				<td>Bild</td>
				<td>Standort</td>
			</tr>
			<tr class="hover">
				<th>6</th>
				<td>Objekt</td>
				<td>Text</td>
			</tr>
			<tr class="hover">
				<th>7</th>
				<td>Text</td>
				<td>QR</td>
			</tr>
			<tr class="hover">
				<th>8</th>
				<td>Bild</td>
				<td>Standort</td>
			</tr>
			<tr class="hover">
				<th>9</th>
				<td>Objekt</td>
				<td>Text</td>
			</tr>
		</tbody>
	</table>

	<button class="btn btn-ghost">
		Neue Aufgabe
		<PlusIcon />
	</button>
</div>
