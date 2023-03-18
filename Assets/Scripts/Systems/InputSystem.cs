using Components;
using DefaultNamespace;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Systems
{
	public enum Swipe
	{
		Default,
		Left,
		Right
	}
	
	public class InputSystem : IEcsRunSystem
	{
		private const float AvailableSwipeOffsetX = .5f;
		private readonly EcsCustomInject<Configs> _configs;
		
		private Vector2 _firstPressPos;

		public void Run(IEcsSystems systems)
		{
			var world = systems.GetWorld();
			var filter = world.Filter<GameComponent>().End();
			var swipeEventPool = world.GetPool<SwipeEventComponent>();
			
#if UNITY_EDITOR
			var swipe = GetStandaloneSwipe();
#endif
			
#if UNITY_ANDROID && UNITY_EDITOR == false
			var swipe = GetMobileSwipe();
#endif

			if (swipe != Swipe.Default)
			{
				var entity = filter.GetRawEntities()[0];
				swipeEventPool.Add(entity);

				ref var swipeEventComponent = ref swipeEventPool.Get(entity);
				swipeEventComponent.Swipe = swipe;
			}
		}

		private Swipe GetMobileSwipe()
		{
			if (Input.touches.Length <= 0)
			{
				return Swipe.Default;
			}
			
			var t = Input.GetTouch(0);
				
			if (t.phase == TouchPhase.Began)
			{
				_firstPressPos = new Vector2(t.position.x, t.position.y);
			}
			else if (t.phase == TouchPhase.Ended)
			{
				var secondPressPos = new Vector2(t.position.x, t.position.y);
				var currentSwipe = secondPressPos - _firstPressPos;

				if (currentSwipe.magnitude < _configs.Value.MinSwipeDistance)
				{
					return Swipe.Default;
				}
						
				currentSwipe.Normalize();	
						
				if(currentSwipe is {x: < 0, y: > -AvailableSwipeOffsetX and < AvailableSwipeOffsetX})
				{
					return Swipe.Left;
				}
				else if(currentSwipe is {x: > 0, y: > -AvailableSwipeOffsetX and < AvailableSwipeOffsetX})
				{
					return Swipe.Right;
				}
			}

			return Swipe.Default;
		}

		private Swipe GetStandaloneSwipe()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}
			if (Input.GetMouseButtonUp(0))
			{
				var secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				var currentSwipe = secondPressPos - _firstPressPos;
           
				if (currentSwipe.magnitude < _configs.Value.MinSwipeDistance)
				{
					return Swipe.Default;
				}

				currentSwipe.Normalize();

				if(currentSwipe is {x: < 0, y: > -AvailableSwipeOffsetX and < AvailableSwipeOffsetX})
				{
					return Swipe.Left;
				}
				else if(currentSwipe is {x: > 0, y: > -AvailableSwipeOffsetX and < AvailableSwipeOffsetX})
				{
					return Swipe.Right;
				}
			}

			return Swipe.Default;
		}
	}
}