import type { Hunt } from '$lib/models/hunt';
import { error } from '@sveltejs/kit';

export async function load({ params, fetch }) {
	const { huntId } = params;
	const isDebug = true;

	let huntData: Hunt;

	if (isDebug) {
		huntData = {
			id: Number(huntId),
			title: 'Hs-Aalen Ersties Tour',
			description: 'Nur für newbies!'
		};
	} else {
		const res = await fetch(`http://localhost:4000/api/Hunt/${huntId}`);

		if (!res.ok) {
			throw error(res.status, await res.text());
		}

		huntData = (await res.json()) as Hunt;
	}

	return {
		huntData
	};
}
