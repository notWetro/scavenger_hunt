/**
 * Pauses execution for a specified number of milliseconds.
 * 
 * @param ms - The number of milliseconds to wait.
 * @returns A promise that resolves after the specified time.
 */
export async function wait(ms: number) {
	await new Promise((resolve) => setTimeout(resolve, ms));
}
