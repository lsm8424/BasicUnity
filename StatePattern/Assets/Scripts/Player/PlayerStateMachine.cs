using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState { get; private set; }

    // 상태 머신 초기화
    public void Initialize(PlayerState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    // 현재 상태를 새로운 상태로 변경
    public void ChangeState(PlayerState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
