using Prefabs;
using UnityEngine;

namespace DefaultNamespace
{
	public class ViewsKeeper : MonoBehaviour
	{
		[SerializeField] private RoadView _roadView;
		[SerializeField] private BalloonView _balloonView;

		public RoadView RoadView => _roadView;
		public BalloonView BalloonView => _balloonView;
	}
}