using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class CreateRoadSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		private readonly EcsCustomInject<ViewsKeeper> _viewsKeeper;

		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var movePool = world.GetPool<MoveComponent>();
			var positionPool = world.GetPool<PositionComponent>();
			var roadEntity = world.NewEntity();
			movePool.Add(roadEntity);
			positionPool.Add(roadEntity);
			
			ref var moveComponent = ref movePool.Get(roadEntity);
			moveComponent.Speed = _configs.Value.InitialGameSpeed;

			ref var positionComponent = ref positionPool.Get(roadEntity);
			positionComponent.Transform = _viewsKeeper.Value.RoadView.transform;
			positionComponent.InitialPos = positionComponent.Transform.position;
		}
	}
}