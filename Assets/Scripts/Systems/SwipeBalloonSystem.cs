using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using States;

namespace Systems
{
	public class SwipeBalloonSystem : IEcsRunSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var swipeFilter = world.Filter<SwipeEventComponent>().End();
			var balloonFilter = world.Filter<BalloonComponent>().End();
			var swipePool = world.GetPool<SwipeEventComponent>();
			var balloonPool = world.GetPool<BalloonComponent>();

			if (swipeFilter.GetEntitiesCount() > 0)
			{
				var entity = swipeFilter.GetRawEntities()[0];
				var balloonEntity = balloonFilter.GetRawEntities()[0];
				var swipeEventComponent = swipePool.Get(entity);
				ref var balloonComponent = ref balloonPool.Get(balloonEntity);
				
				balloonComponent.StateMachine.ChangeState(new MovingState(balloonComponent.BalloonView, swipeEventComponent.Swipe, _configs.Value));
			}
		}
	}
}