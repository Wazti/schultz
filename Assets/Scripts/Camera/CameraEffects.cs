using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects instance;

    [Header("Components")]
    [SerializeField] private CameraShake _shake;


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
    }

    public void Shake(float shakeAmount, float shakeDuration)
    {
        _shake.Shake(shakeAmount, shakeDuration);
    }

}
