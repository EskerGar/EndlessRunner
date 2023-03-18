using Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
	public class MoveSystem : IEcsRunSystem
	{
		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var filter = world.Filter<MoveComponent>().Inc<PositionComponent>().End();
			
			var movePool = world.GetPool<MoveComponent>();
			var positionPool = world.GetPool<PositionComponent>();

			foreach (var entity in filter)
			{
				ref var moveComponent = ref movePool.Get(entity);
				ref var positionComponent = ref positionPool.Get(entity);

				positionComponent.Transform.position -= new Vector3(0, moveComponent.Speed * Time.deltaTime);
			}
		}
	}
}