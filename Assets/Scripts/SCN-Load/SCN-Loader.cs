using UnityEngine;
using UnityEngine.SceneManagement;

public class SCN_Loader : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "SCN_NIV-2"; // Le nom de la scène à charger

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad); // Charger la scène spécifiée dans sceneToLoad
        }
    }
}
