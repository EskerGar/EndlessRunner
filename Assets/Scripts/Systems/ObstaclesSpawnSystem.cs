using System.Collections.Generic;
using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
	public class ObstaclesSpawnSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<ObstaclesPool> _obstaclesPool;
		private readonly EcsCustomInject<Configs> _configs;

		private readonly List<Vector2> _remainingSpawnPoints = new();
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var spawnTimeFilter = world.Filter<SpawnTimeEventComponent>().End();
			
			if (spawnTimeFilter.GetEntitiesCount() > 0)
			{
				var movePool = world.GetPool<MoveComponent>();
				var positionPool = world.GetPool<PositionComponent>();
				var obstacleViewPool = world.GetPool<ObstacleViewComponent>();
				var obstacleInvisibleEventPool = world.GetPool<ObstacleInvisibleEventComponent>();
				var gameFilter = world.Filter<GameComponent>().End();
				var gamePool = world.GetPool<GameComponent>();
				var gameEntity = gameFilter.GetRawEntities()[0];
				ref var gameComponent = ref gamePool.Get(gameEntity);

				if (_remainingSpawnPoints.Count <= 0)
				{
					foreach (var point in _configs.Value.SpawnPoints)
					{
						_remainingSpawnPoints.Add(point);
					}
				}
				
				var obstacleEntity = world.NewEntity();
				var randSpawnPoint = Random.Range(0, _remainingSpawnPoints.Count);
				var randSpritePoint = Random.Range(0, _configs.Value.ObstacleSprites.Length);
				var spawnPoint = _remainingSpawnPoints[randSpawnPoint];
				var sprite = _configs.Value.ObstacleSprites[randSpritePoint];
				var obstacleUiView = _obstaclesPool.Value.Get();
				obstacleUiView.SetSprite(sprite);
				obstacleUiView.transform.position = spawnPoint;
				obstacleUiView.OnInvisible += () => obstacleInvisibleEventPool.Add(obstacleEntity);
				
				movePool.Add(obstacleEntity);
				obstacleViewPool.Add(obstacleEntity);
				positionPool.Add(obstacleEntity);

				_remainingSpawnPoints.Remove(spawnPoint);
				
				ref var moveComponent = ref movePool.Get(obstacleEntity);
				moveComponent.Speed = gameComponent.CurrentSpeed;

				ref var positionComponent = ref positionPool.Get(obstacleEntity);
				positionComponent.Transform = obstacleUiView.transform;
				
				ref var viewComponent = ref obstacleViewPool.Get(obstacleEntity);
				viewComponent.ObstacleUiView = obstacleUiView;
			}
		}
	}
}