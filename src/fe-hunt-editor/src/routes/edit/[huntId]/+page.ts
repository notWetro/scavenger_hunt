import type { PageLoad } from './$types';
import { PUBLIC_API_URL } from '$env/static/public';

export const load: PageLoad = async ({ params }) => {
	const { huntId } = params;
	const hunt = await fetch(`${PUBLIC_API_URL}/hunts/${huntId}`).then((res) => res.json());
	console.log('Editing hunt:', hunt);

	let count = 1;
	for(var assignment of hunt.assignments){
		console.log('Assignments', assignment);
		assignment.id = count++;  
	}
	
	return {
		hunt: hunt
	};
};
