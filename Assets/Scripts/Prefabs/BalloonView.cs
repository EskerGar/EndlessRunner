using System;
using UnityEngine;

namespace Prefabs
{
	public class BalloonView : MonoBehaviour
	{
		public event Action OnTriggerEntered;
		public Vector2 InitialPos {get; private set;}

		private void Start()
		{
			InitialPos = transform.position;
		}

		private void OnTriggerEnter2D(Collider2D col)
		{
			OnTriggerEntered?.Invoke();
		}
	}
}