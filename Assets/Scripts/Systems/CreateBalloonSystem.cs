using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using States;

namespace Systems
{
	public class CreateBalloonSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var positionPool = world.GetPool<PositionComponent>();
			var balloonPool = world.GetPool<BalloonComponent>();
			var balloonEntity = world.NewEntity();
			positionPool.Add(balloonEntity);
			balloonPool.Add(balloonEntity);
			
			ref var positionComponent = ref positionPool.Get(balloonEntity);	
			positionComponent.Transform = _viewsKeeper.Value.BalloonView.transform;
			positionComponent.InitialPos = positionComponent.Transform.position;
			
			ref var balloonComponent = ref balloonPool.Get(balloonEntity);
			balloonComponent.StateMachine = new StateMachine();
			balloonComponent.BalloonView = _viewsKeeper.Value.BalloonView;

			_viewsKeeper.Value.BalloonView.OnTriggerEntered += () =>
			{
				var restartPool = world.GetPool<StartRestartComponent>();
				var gameFilter = world.Filter<GameComponent>().End();
				var gameEntity = gameFilter.GetRawEntities()[0];
				
				restartPool.Add(gameEntity);
			};
		}
	}
}