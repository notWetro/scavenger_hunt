import type { Hunt } from '$lib/models/hunt';
import { error } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';

export const load: PageServerLoad = async ({ params, fetch }) => {
	const { huntId } = params;
	const debug = true;

	if (debug) {
		return {
			hunt: {
				id: huntId,
				title: 'Hs-Aalen Ersties Tour',
				description: 'Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies!'
			}
		};
	}

	const res = await fetch(`http://localhost:4000/api/Hunt/${huntId}`)

	if (!res.ok) {
		throw error(res.status, await res.text());
	}

	return {
		hunt: await res.json() as Hunt
	};
};
