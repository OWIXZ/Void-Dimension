using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    private CinemachineImpulseSource impulseSource;
    AudioManager audioManager;
    [SerializeField] private ScreenShake profile;

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
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        if (remainingTime <= 60)
        {
            //CameraShakeManager.instance.CameraShake(impulseSource);
            CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);
            // GameOver();
            timerText.color = Color.red;
            audioManager.PlaySFX(audioManager.timersound);
            audioManager.PlaySFX(audioManager.shockwave);
        }
        if (remainingTime <= 1)
        {
            SceneManager.LoadScene("Game_Over");
        }
            

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
