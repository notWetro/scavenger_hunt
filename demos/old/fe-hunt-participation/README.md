# Frontend: Hunt Participation Web App

## Introduction

This smaller web app is required for participating in scavenger-hunts. Participants can sign up with their username and password to a desired scavenger-hunt that was provided by the organizer via a link or QR-Code.

## Svelte

Everything you need to build a Svelte project, powered by [`create-svelte`](https://github.com/sveltejs/kit/tree/main/packages/create-svelte).

```bash
# create a new project in the current directory
npm create svelte@latest

# create a new project in my-app
npm create svelte@latest my-app
```

# Development: Setting up environment

## Environment Variables

In order to successfully run the application, a set of environment-variables needs to be defined. Simply create a new `.env` file inside the root directory of this project. Open it and add the content below.

```env
PUBLIC_HUNT_API_ADRESS="https://localhost:7161/api"
PUBLIC_DEMO_MODE="False"
#PUBLIC_DEMO_MODE="True"
```

## Running or building web app

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

You can preview the production build with `npm run preview`.
