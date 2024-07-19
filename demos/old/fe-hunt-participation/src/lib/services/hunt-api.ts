import { PUBLIC_DEMO_MODE, PUBLIC_HUNT_API_URL } from "$env/static/public";
import type { Hunt } from "$lib/models/hunt";
import { error } from "@sveltejs/kit";

export async function postParticipation(username: string, password: string, huntId: number) {
    if (PUBLIC_DEMO_MODE === 'True') {
        return {
            id: 69
        };
    }

    const res = await fetch(`${PUBLIC_HUNT_API_URL}/Participation/Hunt/${huntId}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            username,
            password
        }),
    });

    if (!res.ok) {
        throw error(res.status, await res.text());
    }

    return await res.json();
}

export async function getHunt(huntId: string) {
    if (PUBLIC_DEMO_MODE === "True") {
        return {
            hunt: {
                id: huntId,
                title: 'Hs-Aalen Ersties Tour',
                description: 'Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies! Nur für newbies!'
            }
        };
    }

    const res = await fetch(`${PUBLIC_HUNT_API_URL}/Hunt/${huntId}`);

    if (!res.ok) {
        throw error(res.status, await res.text());
    }

    return await res.json() as Hunt;
}