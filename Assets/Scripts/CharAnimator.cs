﻿using UnityEngine;

public class CharAnimator : MonoBehaviour
{
	[Header("Components")]
	protected Animator _animator;

    [Header("Input")]
    [SerializeField] protected Vector2 _lookDirection;
    public Vector2 LookDirection
    {
        get
        {
            return _lookDirection;
        }
        set
        {
            _lookDirection = value;
            _animator.SetFloat("Look X", value.x);
            _animator.SetFloat("Look Y", value.y);
        }
    }
    [SerializeField] protected float _moveDirection;
    public float MoveDirection
    {
        get
        {
            return _moveDirection;
        }
        set
        {
            _moveDirection = value;
            _animator.SetFloat("HorizontalDirection", value);
        }
    }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    /// <summary>
    /// Plays anim by setting strigger
    /// </summary>
    /// <param name="anim">trigger name</param>
    public void PlayAnim(string anim)
    {
        _animator.SetTrigger(anim);
    }
}
