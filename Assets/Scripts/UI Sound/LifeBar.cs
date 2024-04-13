using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeBar : MonoBehaviour
{

    [SerializeField] Image healthBar;
    [SerializeField] float healtQuantity = 100f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (healtQuantity <= 0)
        {
            SceneManager.LoadScene("Game_Over");
        }
        //DamageDebug();
    }

    public void Damage(float damage)
    {
        healtQuantity -= damage;
        healthBar.fillAmount = healtQuantity / 100f;
    }

    /*public void DamageDebug()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(10);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            Damage(5);
        }
    }
}
