using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class timecout : MonoBehaviour
{
    public float Timelife;
    void Start()
    {

    }

    void Update()
    {
        Timelife = Time.deltaTime + Timelife;
        Debug.Log(Timelife);

        if (Timelife >= 43.0f)
        {
            SceneManager.LoadScene(1);
        }
    }
}