<script lang="ts">
	import { Navbar, NavBrand, NavLi, NavUl, NavHamburger } from 'flowbite-svelte';

	import logo from '$lib/images/logo.png';
	import NavContainer from 'flowbite-svelte/NavContainer.svelte';
</script>

<Navbar let:hidden let:toggle color="primary">
	<NavContainer class="bg-primary-50">
		<NavBrand href="/">
			<img src={logo} class="mr-3 h-6 sm:h-9" alt="HS-Aalen" />
			<span class="self-center whitespace-nowrap text-xl font-semibold">
				Scavenger-Hunt Editor
			</span>
		</NavBrand>
		<NavHamburger on:click={toggle} />
		<NavUl {hidden}>
			<NavLi href="/create">Create</NavLi>
		</NavUl>
	</NavContainer>
</Navbar>
