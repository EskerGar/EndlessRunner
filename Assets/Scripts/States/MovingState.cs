using DefaultNamespace;
using DG.Tweening;
using Prefabs;
using Systems;

namespace States
{
	public class MovingState : IState
	{
		
		private readonly BalloonView _balloonView;
		private readonly Swipe _swipe;
		private readonly Configs _configs;

		public MovingState(BalloonView balloonView, Swipe swipe, Configs configs)
		{
			_balloonView = balloonView;
			_swipe = swipe;
			_configs = configs;
		}

		public void Do(StateMachine stateMachine)
		{
			var swipeValue = _configs.SwipeValue;
			var direction = _swipe == Swipe.Left ? -swipeValue : swipeValue;

			var currentBalloonPositionX = _balloonView.transform.position.x;
			var initialBalloonPositionX = _balloonView.InitialPos.x;

			if (currentBalloonPositionX + direction >= initialBalloonPositionX - swipeValue && 
			    currentBalloonPositionX + direction <= initialBalloonPositionX + swipeValue )
			{
				_balloonView.transform
				            .DOMoveX(currentBalloonPositionX + direction, _configs.SwipeDuration)
				            .OnComplete(() => stateMachine.ChangeState(new IdleState()));
			}
			else
			{
				stateMachine.ChangeState(new IdleState());
			}
		}
	}
}