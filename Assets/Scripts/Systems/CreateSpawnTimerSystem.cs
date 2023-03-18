using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class CreateSpawnTimerSystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var spawnTimePool = world.GetPool<SpawnTimeComponent>();
			var timerEntity = world.NewEntity();
			spawnTimePool.Add(timerEntity);

			ref var spawnTimeComponent = ref spawnTimePool.Get(timerEntity);
			spawnTimeComponent.RemainingTime = _configs.Value.InitialSpawnTime;
		}
	}
}