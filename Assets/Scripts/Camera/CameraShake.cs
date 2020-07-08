using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Transform _transform;

    Vector2 _originalPos;

    [Header("Shake")]
    [SerializeField] private AnimationCurve _shakeCurve;

    void Awake()
    {
        _originalPos = _transform.localPosition;
    }

    public void Shake(float shakeAmount, float shakeDuration)
    {
        StopAllCoroutines();
        StartCoroutine(Shaking(shakeAmount, shakeDuration));
    }

    public IEnumerator Shaking(float shakeAmount, float shakeDuration)
    {
        float curShakeTime = 0;
        float t = 0;
        while (curShakeTime < shakeDuration)
        {
            curShakeTime += Time.deltaTime;
            t = curShakeTime / shakeDuration;
            t = _shakeCurve.Evaluate(t);
            _transform.localPosition = _originalPos + Random.insideUnitCircle * shakeAmount * t;
            yield return null;
        }
        _transform.localPosition = _originalPos;
    }
}