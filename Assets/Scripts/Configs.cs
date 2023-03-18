using Prefabs;
using UnityEngine;

namespace DefaultNamespace
{
	[CreateAssetMenu(fileName = "Configs", menuName = "Configs/Configs")]
	public class Configs : ScriptableObject
	{
		[SerializeField] private float _initialGameSpeed;
		[SerializeField] private float _speedMultiplier;
		[SerializeField] private ObstacleView _obstacleViewPrefab;
		[SerializeField] private float _initialSpawnTime;
		[SerializeField] private float _spawnTimeDivider;
		[SerializeField] private Vector2[] _spawnPoints;
		[SerializeField] private Sprite[] _obstacleSprites;
		[SerializeField] private int _swipeValue;
		[SerializeField] private float _swipeDuration;
		[SerializeField] private float _minSwipeDistance;

		public float InitialGameSpeed => _initialGameSpeed;
		public float SpeedMultiplier => _speedMultiplier;
		public ObstacleView ObstacleViewPrefab => _obstacleViewPrefab;
		public float InitialSpawnTime => _initialSpawnTime;
		public float SpawnTimeDivider => _spawnTimeDivider;
		public Vector2[] SpawnPoints => _spawnPoints;
		public Sprite[] ObstacleSprites => _obstacleSprites;
		public int SwipeValue => _swipeValue;
		public float SwipeDuration => _swipeDuration;
		public float MinSwipeDistance => _minSwipeDistance;
	}
}