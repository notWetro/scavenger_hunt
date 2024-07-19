export interface Hint {
	hintType: HintType;
	data: string;
}

export enum HintType {
	Text,
	Image
}
