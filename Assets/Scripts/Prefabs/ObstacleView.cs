using System;
using System.Collections.Generic;
using UnityEngine;

namespace Prefabs
{
	public class ObstacleView : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D _boxCollider;
		[SerializeField] private SpriteRenderer _spriteRenderer;
		
		public event Action OnInvisible;

		public void SetSprite(Sprite sprite)
		{
			var boundsSize = sprite.bounds.size;
			_spriteRenderer.sprite = sprite;
			_boxCollider.size = boundsSize;
		}
		
		private void OnBecameInvisible()
		{
			OnInvisible?.Invoke();
			OnInvisible = null;
		}
	}
}