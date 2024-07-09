export interface Solution {
	solutionType: SolutionType;
	data: string;
}

export enum SolutionType {
	QRCode = 'QRCode',
	Text = 'Text',
	Location = 'Location'
}
