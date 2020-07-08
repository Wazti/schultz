using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Input Values")]
    public float moveInput;
    public Vector2 lookDirection;

    [Header("Stats")]
    public float moveSpeed;
    private float _value;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _dashForce;
    

    [Header("Acceleration")]
    [SerializeField] private float _accelTime;
    [SerializeField] private AnimationCurve _accelCurve;
    [SerializeField] private float _decelTime;
    [SerializeField] private AnimationCurve _decelCurve;

    [SerializeField] 
    private bool _isActive;

    [Header("Scripts")]
    public Rigidbody2D _rigidbody;

    private void Update()
    {
        //Acelleration
        if (Mathf.Abs(moveInput) > 0.5f)
        {
            _value = Mathf.MoveTowards(_value, 1, 1 / _accelTime * Time.deltaTime);
            _speedMultiplier = _accelCurve.Evaluate(_value);
        }

        //Deceleration
        else if (moveInput == 0)
        {
            _value = Mathf.MoveTowards(_value, 0, 1 / _decelTime * Time.deltaTime);
            _speedMultiplier = _decelCurve.Evaluate(_value);
        }
    }

    public void Move(float input, bool isActive){
        moveInput = input;
        _isActive = isActive;
    }

    private void FixedUpdate()
    {
        //Movement
        _rigidbody.velocity = new Vector2(moveInput * moveSpeed * _speedMultiplier, _rigidbody.velocity.y);
        Debug.Log(moveInput + " "+ moveSpeed + " "+ _speedMultiplier);
        //_rigidbody.MovePosition(moveInput.x * moveSpeed * _speedMultiplier, moveInput.y * moveSpeed * _speedMultiplier);

        ////Rotation
        //if (lookDirection.magnitude > 0.1f && _isActive )
        //    this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(lookDirection.x, lookDirection.y) * Mathf.Rad2Deg * -1 - 90f);
    }
}
