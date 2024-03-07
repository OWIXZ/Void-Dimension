using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Player : MonoBehaviour
{
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;
    
    private void OnTriggerEnter2D()
   
    {
            Debug.Log("aaaaa");

            camera1.SetActive(false);
            camera2.SetActive(true);

    }
    private void OnTriggerExit2D()
    {
            Debug.Log("bbbbbb");
        camera1.SetActive(true);
        camera2.SetActive(false);

    }
    void Start()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
