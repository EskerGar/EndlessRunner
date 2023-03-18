using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Systems
{
	public class CreateGameEntitySystem : IEcsInitSystem
	{
		private readonly EcsCustomInject<Configs> _configs;
		
		public void Init(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var gamePool = world.GetPool<GameComponent>();
			var gameEntity = world.NewEntity();
			gamePool.Add(gameEntity);
			
			ref var gameComponent = ref gamePool.Get(gameEntity);
			gameComponent.CurrentSpeed = _configs.Value.InitialGameSpeed;
			gameComponent.SpeedMultiplier = _configs.Value.SpeedMultiplier;
			gameComponent.SpawnTime = _configs.Value.InitialSpawnTime;
			gameComponent.SpawnTimeDivider = _configs.Value.SpawnTimeDivider;
		}
	}
}