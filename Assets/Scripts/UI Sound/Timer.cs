using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.InputSystem;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;
    private CinemachineImpulseSource impulseSource;
    private AudioManager audioManager;
    [SerializeField] private ScreenShake profile;
    private bool isVibrating = false;
    private float vibrationIntensity = 0.005f;
    private float vibrationStartTime = -1;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        if (remainingTime > 0 && Time.timeScale > 0)
        {
            remainingTime -= Time.deltaTime;
        }

        if (remainingTime <= 60)
        {
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);
            StartContinuousVibration(vibrationIntensity);
            if (!isVibrating)
            {
                isVibrating = true;
                timerText.color = Color.red;
                audioManager.PlaySFX(audioManager.timersound);
                audioManager.PlaySFX(audioManager.shockwave);
            }
        }

        if (remainingTime <= 1)
        {
            SceneManager.LoadScene("Game_Over");
            StopVibration();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void StartContinuousVibration(float intensity)
    {
        if (Time.time - vibrationStartTime < 0.5f && Time.timeScale > 0)
            return;

        var gamepad = Gamepad.current;
        if (gamepad != null && Time.timeScale > 0)
        {
            gamepad.SetMotorSpeeds(intensity, intensity);
        }

        vibrationStartTime = Time.time;
    }

    private void StopVibration()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.ResetHaptics();
        }
    }
}
