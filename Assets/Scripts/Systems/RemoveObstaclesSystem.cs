using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class RemoveObstaclesSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<ObstaclesPool> _obstaclesPool;
		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var obstacleInvisibleEventFilter = world.Filter<ObstacleInvisibleEventComponent>().End();
			var obstacleViewPool = world.GetPool<ObstacleViewComponent>();

			foreach (var obstacleEntity in obstacleInvisibleEventFilter)
			{
				var viewComponent = obstacleViewPool.Get(obstacleEntity);
				
				_obstaclesPool.Value.Put(viewComponent.ObstacleUiView);
				
				world.DelEntity(obstacleEntity);
			}
		}
	}
}