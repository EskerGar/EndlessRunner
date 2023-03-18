using System;
using UnityEngine;

namespace Prefabs
{
	public class RoadPartView : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;

		public Vector2 Size => _spriteRenderer.size;
		public event Action OnInvisible;

		private void OnBecameInvisible()
		{
			OnInvisible?.Invoke();
		}
	}
}