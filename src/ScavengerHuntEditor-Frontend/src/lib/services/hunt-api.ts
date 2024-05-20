import type { Hunt } from "$lib/models/Hunt";
import { PUBLIC_HUNT_API_ADRESS } from '$env/static/public';

export async function postHunt(hunt: Hunt): Promise<boolean> {

    const jsonApiHunt = JSON.stringify({
        title: hunt.title,
        description: hunt.description,
        assignments: hunt.assignments.map((assignment) => ({
            hint: {
                hintType: assignment.hint.hintType,
                data: assignment.hint.data
            },
            solution: {
                type: assignment.solution.solutionType,
                data: assignment.solution.data
            }
        }))
    });
    console.log(jsonApiHunt);

    const response = await fetch(`${PUBLIC_HUNT_API_ADRESS}/Hunts`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonApiHunt
    });

    if (!response) throw new Error('Failed to save hunt via hunt-api');

    return true;
}