using System;
using UnityEngine;

public class test : MonoBehaviour
{
    private float Timer = 0;

    private float goal = 10;

    private void Start()
    {
        
        Debug.Log("Sono partito");
    }

    private void Update()
    {
        Timer = Timer + Time.deltaTime;
        if (Timer >= goal)
        {
            Debug.Log("Hai vinto");
            Destroy(this.gameObject);
        }
    } 
}
