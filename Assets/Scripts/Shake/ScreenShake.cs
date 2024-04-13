using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScreenShake/New Profile")]
public class ScreenShake : ScriptableObject
{
    [Header("Impulse Source Settings")]
    public float impactTime = 0.2f;
    public float impactForce = 1f;
    public Vector3 defaultVelocity = new Vector3(0f, -1f, 0f);
    public AnimationCurve impulsCurve;

    [Header ("Impulse Listener Settings")]
    public float ListenerAmplitude = 1f;
    public float listenerFrequency = 1f;
    public float listenerDuration = 1f;
}
