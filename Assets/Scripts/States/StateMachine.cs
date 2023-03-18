namespace States
{
	public class StateMachine
	{
		private IState _currentState;

		public StateMachine()
		{
			_currentState = new IdleState();
		}

		public void ChangeState(IState state)
		{
			if (_currentState.GetType() != state.GetType())
			{
				_currentState = state;
				_currentState.Do(this);
			}
		}
	}
}