import type { Hunt } from "$lib/models/Hunt";

export async function postHunt(hunt: Hunt): Promise<boolean> {
    const jsonHunt = JSON.stringify({ hunt });
    console.log(jsonHunt);

    const response = await fetch('http://127.0.0.1:3000/receipt', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonHunt
    });

    if (!response) throw new Error('Failed to save hunt via hunt-api');

    return true;
}