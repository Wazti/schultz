using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform _camera;
    [SerializeField]
    private float _relativeMove = .3f;
    [SerializeField]
    private bool _verticalEnabled = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector2(_camera.position.x * _relativeMove, _verticalEnabled  ? _camera.position.y * _relativeMove : transform.position.y);
    }
}
