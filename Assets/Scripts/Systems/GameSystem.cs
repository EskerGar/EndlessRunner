using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class GameSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var gameFilter = world.Filter<GameComponent>().End();
			var gamePool = world.GetPool<GameComponent>();
			var gameEntity = gameFilter.GetRawEntities()[0];
			ref var gameComponent = ref gamePool.Get(gameEntity);
			gameComponent.Time += Time.deltaTime;

			if (gameComponent.Time > 10)
			{
				var filter = world.Filter<MoveComponent>().Inc<PositionComponent>().End();
				var movePool = world.GetPool<MoveComponent>();
				gameComponent.CurrentSpeed *= gameComponent.SpeedMultiplier;

				if (gameComponent.SpawnTime >= .5)
				{
					gameComponent.SpawnTime /= gameComponent.SpawnTimeDivider;
				}
				
				foreach (var entity in filter)
				{
					ref var moveComponent = ref movePool.Get(entity);
					moveComponent.Speed = gameComponent.CurrentSpeed;
				}

				gameComponent.Time = 0;
			}
		}
	}
}