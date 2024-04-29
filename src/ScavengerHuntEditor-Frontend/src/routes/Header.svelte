<script>
	import { page } from '$app/stores';
	import { Plus } from 'lucide-svelte';
</script>

<div class="navbar bg-base-100">
	<div class="navbar-start">
		<!--		TODO: Add logo here-->
		<a class="btn btn-ghost text-xl hover:no-underline" href="/">Scavenger Hunt Editor</a>
	</div>
	<div class="navbar-center hidden lg:flex">
		<ul class="menu menu-horizontal px-1">
			<li aria-current={$page.url.pathname === '/' ? 'page' : undefined}><a href="/">Home</a></li>
			<li aria-current={$page.url.pathname === '/about' ? 'page' : undefined}>
				<a href="/about">About</a>
			</li>
		</ul>
	</div>
	<div class="navbar-end">
		<a class="btn" href="/create">
			<Plus />
			Add new
		</a>
	</div>
</div>
