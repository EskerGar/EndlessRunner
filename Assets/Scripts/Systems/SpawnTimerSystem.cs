using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class SpawnTimerSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var spawnTimeFilter = world.Filter<SpawnTimeComponent>().End();
			var spawnTimePool = world.GetPool<SpawnTimeComponent>();
			var spawnTimeEventPool = world.GetPool<SpawnTimeEventComponent>();
			var gameFilter = world.Filter<GameComponent>().End();
			var gamePool = world.GetPool<GameComponent>();
			var gameEntity = gameFilter.GetRawEntities()[0];
			ref var gameComponent = ref gamePool.Get(gameEntity);

			var timerEntity = spawnTimeFilter.GetRawEntities()[0];
			ref var spawnTimeComponent = ref spawnTimePool.Get(timerEntity);
			spawnTimeComponent.RemainingTime -= Time.deltaTime;

			if (spawnTimeComponent.RemainingTime <= 0)
			{
				spawnTimeEventPool.Add(timerEntity);
				spawnTimeComponent.RemainingTime = gameComponent.SpawnTime;
			}
		}
	}
}