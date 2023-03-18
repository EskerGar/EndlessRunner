using Components;
using Leopotam.EcsLite;

namespace Systems
{
	public class EventsDeletingSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			
			var obstacleInvisibleFilter = world.Filter<ObstacleInvisibleEventComponent>().End();
			var restartFilter = world.Filter<RestartEventComponent>().End();
			var spawnTimeFilter = world.Filter<SpawnTimeEventComponent>().End();
			var swipeFilter = world.Filter<SwipeEventComponent>().End();
			
			var obstacleInvisiblePool = world.GetPool<ObstacleInvisibleEventComponent>();
			var restartPool = world.GetPool<RestartEventComponent>();
			var spawnTimePool = world.GetPool<SpawnTimeEventComponent>();
			var swipePool = world.GetPool<SwipeEventComponent>();

			DelEvent(obstacleInvisibleFilter, obstacleInvisiblePool);
			DelEvent(restartFilter, restartPool);
			DelEvent(spawnTimeFilter, spawnTimePool);
			DelEvent(swipeFilter, swipePool);
		}

		private void DelEvent<T>(EcsFilter filter, EcsPool<T> pool) 
			where T : struct
		{
			foreach (var entity in filter)
			{
				pool.Del(entity);
			}
		}
	}
}