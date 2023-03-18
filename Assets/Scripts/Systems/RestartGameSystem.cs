using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class RestartGameSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		private readonly EcsCustomInject<ObstaclesPool> _obstaclesPool;
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;
		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var restartFilter = world.Filter<RestartEventComponent>().End();

			if (restartFilter.GetEntitiesCount() > 0)
			{
				var gameFilter = world.Filter<GameComponent>().End();
				var spawnTimeFilter = world.Filter<SpawnTimeComponent>().End();
				var obstacleFilter = world.Filter<ObstacleViewComponent>().End();
				var positionFilter = world.Filter<PositionComponent>().End();
				var moveFilter = world.Filter<MoveComponent>().End();
			
				var positionPool = world.GetPool<PositionComponent>();
				var gamePool = world.GetPool<GameComponent>();
				var spawnTimePool = world.GetPool<SpawnTimeComponent>();
				var obstacleViewPool = world.GetPool<ObstacleViewComponent>();
				var movePool = world.GetPool<MoveComponent>();

				var gameEntity = gameFilter.GetRawEntities()[0];
				var spawnTimerEntity = spawnTimeFilter.GetRawEntities()[0];
				
				foreach (var entity in positionFilter)
				{
					ref var positionComponent = ref positionPool.Get(entity);
					positionComponent.Transform.position = positionComponent.InitialPos;
				}

				ref var gameComponent = ref gamePool.Get(gameEntity);
				gameComponent.CurrentSpeed = _configs.Value.InitialGameSpeed;
				gameComponent.SpawnTime = _configs.Value.InitialSpawnTime;
				gameComponent.Time = 0;

				ref var spawnTimeComponent = ref spawnTimePool.Get(spawnTimerEntity);
				spawnTimeComponent.RemainingTime = gameComponent.SpawnTime;
			
				foreach (var obstacleEntity in obstacleFilter)
				{
					var viewComponent = obstacleViewPool.Get(obstacleEntity);
				
					_obstaclesPool.Value.Put(viewComponent.ObstacleUiView);
				
					world.DelEntity(obstacleEntity);
				}
				
				foreach (var entity in moveFilter)
				{
					ref var moveComponent = ref movePool.Get(entity);
					moveComponent.Speed = _configs.Value.InitialGameSpeed;
				}
			}
		}
	}
}