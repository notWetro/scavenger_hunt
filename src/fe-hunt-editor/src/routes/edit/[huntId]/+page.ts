import type { PageLoad } from './$types';
import { PUBLIC_API_URL } from '$env/static/public';

export const load: PageLoad = async ({ params }) => {
	const { huntId } = params;
	const hunt = await fetch(`${PUBLIC_API_URL}/hunts/${huntId}`).then((res) => res.json());
	console.log(hunt);
	return {
		hunt: hunt
	};
};
