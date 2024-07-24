export interface Solution {
	solutionType: SolutionType;
	data: string;
}

export enum SolutionType {
	QRCode,
	Text,
	Location
}
