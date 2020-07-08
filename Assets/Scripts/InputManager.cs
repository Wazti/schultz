using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public enum ControllerType { Keyboard, Gamepad };

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerControls input;
    private Action _interact;
    public UnityEvent evt;

    [Header("Controllers")]
    [SerializeField] private ControllerType _controllerType;

    [Header("Gamepad Vibrtation")]
    [SerializeField] private Vector2 _curVibrationForce;

    [Header("Scripts")]
    private PlayerController _playerController;

    public ControllerType ControllerType
    {
        get
        {
            return _controllerType;
        }
        set
        {
            _controllerType = value;
            _playerController.controllerType = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        _playerController = GameObject.FindObjectOfType<PlayerController>();

        input = new PlayerControls();
        input.Gameplay.Enable();

        if (Gamepad.current == null)
        {
            ControllerType = ControllerType.Keyboard;
        }
        else
        {
            ControllerType = ControllerType.Gamepad;
        }

        input.Gameplay.Interact.performed += context => evt?.Invoke();
        input.Gameplay.Restart.performed += context => Restart();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
