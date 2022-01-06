using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour 
{

    private static Music instance;

    private void Awake()
    {
        if (instance == null)
        {
            // the music become the instance and doesn't destroy between the scene transitions
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else {
            Destroy(gameObject);
        }
    }
}