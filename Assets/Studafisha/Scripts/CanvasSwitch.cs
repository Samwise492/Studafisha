using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasSwitch : MonoBehaviour
{
    Canvas canvas;
    void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.sceneCount == 1)
            canvas.gameObject.SetActive(true);
    }
}
