using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum PlayerState { Idle, Walk, Shoot, Death, Dash, Hit };

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    [Header("Controllers")]
    [SerializeField]private UnityEngine.InputSystem.Utilities.ReadOnlyArray<InputDevice> devices;
    public ControllerType controllerType;

    [Header("Input Values")]
    [SerializeField] private float _moveDirection;
    private float MoveDirection
    {
        get
        {
            return _moveDirection;
        }
        set
        {
            _moveDirection = value;
            //LookDirection = value;

            //_animator.MoveDirection = value;

            _movement.Move(value, PlayerState != PlayerState.Dash);

            if (value == 0)
            {
                PlayerState = PlayerState.Idle;
            }

            else
            {
                PlayerState = PlayerState.Walk;
            }
        }
    }

    [SerializeField] private Vector2 _lookDirection;
    private Vector2 LookDirection
    {
        get
        {
            return _lookDirection;
        }
        set
        {
            if (value != Vector2.zero)
            {
                _lookDirection = value;
                _animator.LookDirection = LookDirection;
            }
        }
    }


    [SerializeField] private Vector2 _shootDirection;
    public Vector2 shootDirection {
        get 
        {
            return _shootDirection;
        }
        set 
        {
            _shootDirection = value;
        }
    }

    [Header("Rotation")]
    [SerializeField] private Vector2 _mouseScreenPos;
    [SerializeField] private Vector2 _mousePos;
    [SerializeField] private float _rotation;
    [SerializeField] private Camera _camera;
    

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
            //_animator.PlayerState = value;
        }
    }

    [Header("Scripts")]
    public Rigidbody2D _rigidbody;
    private Transform _transform;
    private PlayerMovement _movement;
    private PlayerJump _jump;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _jump = GetComponent<PlayerJump>();
        _movement = GetComponent<PlayerMovement>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Start()
    {
        InputManager.instance.input.Gameplay.Move.performed += context => MoveDirection = context.ReadValue<float>();
        InputManager.instance.input.Gameplay.Move.canceled += context => MoveDirection = 0;
        InputManager.instance.input.Gameplay.Jump.performed += context => Jump();
        if (controllerType == ControllerType.Gamepad)
        {
            InputManager.instance.input.Gameplay.Rotate.performed += context => shootDirection = context.ReadValue<Vector2>();
        }
    }

    private void Update()
    {
        /*if (controllerType == ControllerType.Keyboard)
        {
            //Rotation
            _mouseScreenPos = InputManager.instance.input.Gameplay.MousePos.ReadValue<Vector2>();
            _mousePos = _camera.ScreenToWorldPoint(_mouseScreenPos);
            Vector2 diff = _mousePos - (Vector2)_transform.position;
            diff.Normalize();
            shootDirection = diff;
            _rotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 180;
            //Later replace with gun rotation:
            //_transform.rotation = Quaternion.Euler(0f, 0f, _rotation);
        }*/
    }

    public void Jump()
    {
        _jump.Jump();
    }
    public void Hit()
	{
        PlayerState = PlayerState.Hit;
	}
     
    public void OnHitFinished()
	{
        PlayerState = PlayerState.Idle;
	}

    public void Death(string anim = "Death")
    {
        PlayerState = PlayerState.Death;
        InputManager.instance.input.Gameplay.Disable();
        _animator.PlayAnim(anim);
        CameraEffects.instance.Shake(0.5f, 0.25f);

        //Destroy all components
        var components = GetComponents<Component>();
        foreach (var component in components)
        {
            if (!component is Transform)
            {
                Destroy(component);
            }
        }
    }
}
