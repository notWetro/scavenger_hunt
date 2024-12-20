import flowbitePlugin from 'flowbite/plugin';
import type { Config } from 'tailwindcss';

const customUtilities = function ({ addUtilities }) {
	addUtilities({
		'.grid-area-custom': {
			'grid-area': '1 / 1'
		}
	});
};

export default {
	content: [
		'./src/**/*.{html,js,svelte,ts}',
		'./node_modules/flowbite-svelte/**/*.{html,js,svelte,ts}'
	],

	darkMode: 'class', 

	theme: {
		extend: {
			colors: {
				// flowbite-svelte
				primary: {
					50: '#EBF5FF',
					100: '#D1E8FF',
					200: '#A6D4FF',
					300: '#78BDFF',
					400: '#3EA2FF',
					500: '#0087FF',
					600: '#006FCC',
					700: '#005DAE',
					800: '#004C91',
					900: '#003874'
				}
			}
		}
	},

	plugins: [
		flowbitePlugin, 
		customUtilities
	]
} as Config;
