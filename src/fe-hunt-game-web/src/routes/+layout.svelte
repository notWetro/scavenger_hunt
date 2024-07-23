<script>
	import '../app.css';
</script>

<div
	class="flex flex-col justify-center items-center min-h-screen bg-gradient-to-tr from-violet-300 from-0% via-sky-300 via-20% to-violet-100"
>
	<div class="container xl:m-4 xl:rounded-xl p-5 bg-white bg-opacity-80">
		<slot />
	</div>
</div>
