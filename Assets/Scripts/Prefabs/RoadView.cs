using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Prefabs
{
	public class RoadView : MonoBehaviour
	{
		[SerializeField] private RoadPartView[] _roadPartViews;

		private readonly Queue<RoadPartView> _roadPartQueue = new();

		private void Awake()
		{
			foreach (var roadPartView in _roadPartViews)
			{
				roadPartView.OnInvisible += ResetRoadPart;
				_roadPartQueue.Enqueue(roadPartView);
			}
		}

		private void ResetRoadPart()
		{
			var roadPartView = _roadPartQueue.Dequeue();
			var lastRoadPartView = _roadPartQueue.Last();
			roadPartView.transform.position = lastRoadPartView.transform.position + new Vector3(0, lastRoadPartView.Size.y);
			_roadPartQueue.Enqueue(roadPartView);
		}
	}
}