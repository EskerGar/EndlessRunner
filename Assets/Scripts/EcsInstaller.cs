using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Systems;
using UnityEngine;

public class EcsInstaller : MonoBehaviour
{
	[SerializeField] private Configs _configs;
	[SerializeField] private ViewsKeeper _viewsKeeper;
	
	private EcsWorld _ecsWorld;
	private EcsSystems _initSystems;
	private EcsSystems _updateSystems;

	private void Awake()
	{
		var obstaclesPool = new ObstaclesPool(_configs.ObstacleViewPrefab);
		_ecsWorld = new EcsWorld();

		_initSystems = new EcsSystems(_ecsWorld);
		_updateSystems = new EcsSystems(_ecsWorld);

		_initSystems
			.Add(new CreateGameEntitySystem())
			.Add(new CreateRoadSystem())
			.Add(new CreateBalloonSystem())
			.Add(new CreateSpawnTimerSystem())
			.Inject(_configs)
			.Inject(obstaclesPool)
			.Inject(_viewsKeeper)
			.Init();

		_updateSystems
			.Add(new InputSystem())
			.Add(new SpawnTimerSystem())
			.Add(new ObstaclesSpawnSystem())
			.Add(new MoveSystem())
			.Add(new GameSystem())
			.Add(new SwipeBalloonSystem())
			.Add(new RemoveObstaclesSystem())
			.Add(new RestartTimerSystem())
			.Add(new RestartGameSystem())
			.Add(new EventsDeletingSystem())
			.Inject(_configs)
			.Inject(obstaclesPool)
			.Init();
	}

	private void Update()
	{
		_updateSystems?.Run();
	}

	public void OnDestroy()
	{
		_initSystems?.Destroy();
		_updateSystems?.Destroy();
		_initSystems = null;
		_updateSystems = null;
		_ecsWorld?.Destroy();
		_ecsWorld = null;
	}
}