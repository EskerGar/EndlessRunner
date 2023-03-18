using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class RestartTimerSystem : IEcsRunSystem
	{
		private const float RestartTime = 3f;

		private float _time;
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var startRestartFilter = world.Filter<StartRestartComponent>().End();
			var moveFilter = world.Filter<MoveComponent>().End();
			var gameFilter = world.Filter<GameComponent>().End();
			
			var movePool = world.GetPool<MoveComponent>();
			var startRestartPool = world.GetPool<StartRestartComponent>();
			var restartEventPool = world.GetPool<RestartEventComponent>();
			var gameEntity = gameFilter.GetRawEntities()[0];

			if (startRestartFilter.GetEntitiesCount() > 0)
			{
				foreach (var entity in moveFilter)
				{
					ref var moveComponent = ref movePool.Get(entity);
					moveComponent.Speed = 0;
				}
				
				_time += Time.deltaTime;

				if (_time >= RestartTime)
				{
					restartEventPool.Add(gameEntity);
					startRestartPool.Del(gameEntity);
					_time = 0;
				}
			}
		}
	}
}