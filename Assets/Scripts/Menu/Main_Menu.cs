using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool gameIsPaused = false;
    [SerializeField] Movement Moving;

    public void ButtonPlay()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
