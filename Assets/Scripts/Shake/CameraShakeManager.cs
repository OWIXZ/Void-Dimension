using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;
    [SerializeField] private float globalShakeForce = 1f;
    [SerializeField] private CinemachineImpulseListener impulseListener;
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Référence à la CinemachineVirtualCamera

    private CinemachineImpulseDefinition impulseDefinition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Assurez-vous que la caméra est en mode perspective
        SetCameraToPerspective();
    }

    public void CameraShake(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }

    public void ScreenShakeFromProfile(ScreenShake profile, CinemachineImpulseSource impulseSource)
    {
        SetupScreenShakeSettings(profile, impulseSource);
        impulseSource.GenerateImpulseWithForce(profile.impactForce);
    }

    public void SetupScreenShakeSettings(ScreenShake profile, CinemachineImpulseSource impulseSource)
    {
        impulseDefinition = impulseSource.m_ImpulseDefinition;

        //change the impulse source settings
        impulseDefinition.m_ImpulseDuration = profile.impactTime;
        impulseSource.m_DefaultVelocity = profile.defaultVelocity;
        impulseDefinition.m_CustomImpulseShape = profile.impulsCurve;

        //change the impulse listener settings
        impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.ListenerAmplitude;
        impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
        impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
    }

    private void SetCameraToPerspective()
    {
        if (virtualCamera != null)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.orthographic = false;
            }

            // Optionnel: Ajuster d'autres paramètres spécifiques à la vue perspective
            virtualCamera.m_Lens.FieldOfView = 60f; // Exemple de réglage pour le champ de vision
        }
    }
}
