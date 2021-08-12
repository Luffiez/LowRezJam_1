using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance = null;

    private float shakeDuration = 0f;
    float shakeMagnitude = 0.7f;
    float dampingSpeed = 1.0f;
    Vector3 initialPosition;

    void Awake()
    {
        instance = this;
    }
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void TriggerShake(float duration = 0.1f, float magnitude = 0.2f)
    {
        shakeMagnitude = magnitude;
        shakeDuration = duration;
    }
}
