using UnityEngine;

public class PlayerAnimator : CharAnimator
{
    [Header("States")]
    [SerializeField] private PlayerState _playerState;
    public PlayerState PlayerState
    {
        get
        {
            return _playerState;
        }
        set
        {
            _playerState = value;
            if(value != PlayerState.Death)
			{
                _animator.SetTrigger(value.ToString());
                
            }
            Debug.Log(_playerState);
        }
    }
}