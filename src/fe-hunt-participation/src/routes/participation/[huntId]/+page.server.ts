import type { PageServerLoad } from './$types';
import { getHunt } from '$lib/services/hunt-api';

export const load: PageServerLoad = async ({ params, fetch }) => {
	const { huntId } = params;
	const hunt = await getHunt(huntId)
	console.log(hunt);
	return {
		hunt: hunt
	};
};
