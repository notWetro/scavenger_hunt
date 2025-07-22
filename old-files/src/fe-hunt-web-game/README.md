# Frontend: Hunt Participation Web App

## Introduction

This folder contains the Web-Game. With the files in this folder you can register, login and most importantly play scavanger hunts. When you use the Hunt-Gui the following steps become obsolet.

## Svelte

Everything you need to build a Svelte project, powered by [`create-svelte`](https://github.com/sveltejs/kit/tree/main/packages/create-svelte).

```bash
# create a new project in the current directory
npm create svelte@latest

# create a new project in my-app
npm create svelte@latest my-app
```

## Development: Setting up environment

### Environment Variables

In order to successfully run the application, a set of environment-variables needs to be defined. Simply create a new `.env` file inside the root directory of this project. Open it and add the content below.

```env
PUBLIC_HUNT_API_URL=http://localhost:5500/hunts/api
PUBLIC_PARTICIPANT_API_URL=http://localhost:5500/participants/api
PUBLIC_API_URL=http://localhost:5500/participants/api
```

### Running or building web app

Once you've installed dependencies with `npm install` (or `pnpm install` or `yarn`), start a development server:

```bash
npm run dev

# or start the server and open the app in a new browser tab
npm run dev -- --open
```

To create a production version of your app:

```bash
npm run build
```

With the command:
```bash
`npm run preview -- --open --host`
```
You can start the Preview as a host and open it. The --host tag is important if you want other people to be able to play aswell.