using System.Collections.Generic;
using Prefabs;
using UnityEngine;

namespace DefaultNamespace
{
	public class ObstaclesPool
	{
		private readonly ObstacleView _prefab;
		private readonly Stack<ObstacleView> _pool = new();

		public ObstaclesPool(ObstacleView prefab)
		{
			_prefab = prefab;
		}

		public ObstacleView Get()
		{
			if (_pool.Count > 0)
			{
				var view = _pool.Pop();
				view.gameObject.SetActive(true);
				
				return view;
			}
			else
			{
				return Object.Instantiate(_prefab);
			}
		}

		public void Put(ObstacleView obstacleUiView)
		{
			_pool.Push(obstacleUiView);
			obstacleUiView.gameObject.SetActive(false);
		}
	}
}