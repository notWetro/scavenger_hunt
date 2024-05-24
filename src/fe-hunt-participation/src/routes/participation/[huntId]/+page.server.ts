import type { Hunt } from '$lib/models/hunt';
import { error } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import { PUBLIC_HUNT_API_ADRESS, PUBLIC_DEMO_MODE } from '$env/static/public';

export const load: PageServerLoad = async ({ params, fetch }) => {
	const { huntId } = params;

	console.log(`${PUBLIC_HUNT_API_ADRESS}/Hunt/${huntId}`)

	if (PUBLIC_DEMO_MODE === "True") {
		return {
			hunt: {
				id: huntId,
				title: 'Hs-Aalen Ersties Tour',
				description: 'Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies!'
			}
		};
	}

	const res = await fetch(`${PUBLIC_HUNT_API_ADRESS}/Hunt/${huntId}`)

	if (!res.ok) {
		throw error(res.status, await res.text());
	}

	return {
		hunt: await res.json() as Hunt
	};
};
