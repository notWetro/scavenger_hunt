import { PUBLIC_PARTICIPANT_API_URL } from '$env/static/public';
import { error } from '@sveltejs/kit';
import type { PageServerLoad } from './$types';
import type { Stats } from '$lib/models/stats';

export const load: PageServerLoad = async ({ fetch }) => {
	const res = await fetch(`${PUBLIC_PARTICIPANT_API_URL}/Participations/Stats`);

	if (!res.ok) {
		// Instead of throwing a generic error, throw a custom error message
		// when the hunt is not found.
		const errMsg = res.status === 404 ? 'No stats could be read!' : await res.text();
		throw error(res.status, errMsg);
	}

	const stats = (await res.json()) as Stats;
	return { stats };
};