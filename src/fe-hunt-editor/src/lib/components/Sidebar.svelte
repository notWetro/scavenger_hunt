<script>
	import { Sidebar, SidebarGroup, SidebarItem, SidebarWrapper } from 'flowbite-svelte';
	import { MapPin, Milestone, PieChart, Plus } from 'lucide-svelte';
</script>

<div class="p-10">
	<Sidebar>
		<SidebarWrapper>
			<SidebarGroup>
				<SidebarItem label="Dashboard">
					<svelte:fragment slot="icon">
						<PieChart class="w-6 h-6 text-gray-500" />
					</svelte:fragment>
				</SidebarItem>
				<SidebarItem label="Anzahl Schnitzeljagden: ">
					<svelte:fragment slot="icon">
						<Milestone
							class="w-6 h-6 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900"
						/>
					</svelte:fragment>
				</SidebarItem>
				<SidebarItem label="Anzahl Stationen: ">
					<svelte:fragment slot="icon">
						<MapPin
							class="w-6 h-6 text-gray-500 transition duration-75 dark:text-gray-400 group-hover:text-gray-900"
						/>
					</svelte:fragment>
				</SidebarItem>
			</SidebarGroup>
			<SidebarGroup border>
				<SidebarItem label="Neue Schnitzeljagd erstellen" href="/create">
					<svelte:fragment slot="icon">
						<Plus class="w-6 h-6 text-gray-500 transition duration-75 group-hover:text-gray-900" />
					</svelte:fragment>
				</SidebarItem>
			</SidebarGroup>
		</SidebarWrapper>
	</Sidebar>
</div>
