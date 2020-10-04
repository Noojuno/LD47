using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public string nextScene;
    public bool hasLoaded = false;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("TRIGGERED");
    }
}
