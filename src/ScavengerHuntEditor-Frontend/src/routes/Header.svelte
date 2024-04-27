<script>
	import { page } from '$app/stores';
	import { Plus } from 'lucide-svelte';
</script>

<div class="navbar bg-base-100">
	<div class="navbar-start">
		<div class="dropdown">
			<div tabindex="0" role="button" class="btn btn-ghost lg:hidden">
				<svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h8m-8 6h16" /></svg>
			</div>
		</div>
		<a class="btn btn-ghost text-xl" href="/">Scavenger Hunt Editor</a>
	</div>
	<div class="navbar-center hidden lg:flex">
		<ul class="menu menu-horizontal px-1">
			<li aria-current={$page.url.pathname === '/' ? 'page' : undefined}><a href="/">Home</a></li>
			<li aria-current={$page.url.pathname === '/about' ? 'page' : undefined}>
				<details>
					<summary>Parent</summary>
					<ul class="p-2">
						<li><a href="/about">About</a></li>
					</ul>
				</details>
			</li>
			<li aria-current={$page.url.pathname.startsWith('/sverdle') ? 'page' : undefined}><a href="/sverdle">Sverdle</a></li>
		</ul>
	</div>
	<div class="navbar-end">
		<a class="btn" href="/create">
			<Plus/>
			Add new
		</a>
	</div>
</div>
