const config = {
	content: [
		'./src/**/*.{html,js,svelte,ts}',
		'./node_modules/flowbite-svelte/**/*.{html,js,svelte,ts}'
	],

	plugins: [require('flowbite/plugin')],

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
	}
};

module.exports = config;
