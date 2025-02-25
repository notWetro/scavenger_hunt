import { PUBLIC_HUNT_API_URL } from '$env/static/public';
import type { PageServerLoad } from './$types';
import type { Hunt } from '$lib/models/hunt';
import { error } from '@sveltejs/kit';

export const load: PageServerLoad = async ({ params, fetch }) => {
	const { huntId } = params;
	const res = await fetch(`${PUBLIC_HUNT_API_URL}/Hunts/${huntId}`);

	if (!res.ok) {
		const errMsg =
			res.status === 404
				? 'There seems to be no hunt with this Id, please try a different one!'
				: await res.text();
		throw error(res.status, errMsg);
	}

	const hunt = (await res.json()) as Hunt;
	return {
		hunt: hunt
	};
};
