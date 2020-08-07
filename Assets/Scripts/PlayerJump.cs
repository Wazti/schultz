using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    private float _velocity;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private BoxCollider2D _boxCollider; 
    private Rigidbody2D _rigidbody;
    private PlayerController _controller;

    [SerializeField]
    private bool isGround = true;
    // Start is called before the first frame update
    public float deltaHeight = 0.1f;
    void Start()
    {
        _rigidbody = GetComponentInParent<Rigidbody2D>();
        //_boxCollider = GetComponent<BoxCollider2D>();
        _controller = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    public void Jump()
    {
        if (isGround) {
            _rigidbody.velocity = Vector2.up * _velocity;
        }
    }
    //
    private void OnJumpFinished() {
        _controller.PlayerState = PlayerState.Fall;
        _controller.OnJumpFinished();
    }
    private void OnTriggerStay2D(Collider2D collider){
        var res = collider != null && (((1<<collider.gameObject.layer) & layerMask) != 0);
            if (!isGround && res) {
                Debug.Log(res);
                OnJumpFinished();
        }
        isGround = res;
    }
    private void OnTriggerExit2D(Collider2D collider){
       isGround = false;
    }
}
