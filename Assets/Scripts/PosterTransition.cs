using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PosterTransition : MonoBehaviour
{
    public static PosterTransition Instance { get; set; }
    Squads squadCanvas;
    Camera squadCamera;

    void Start()
    {
        DontDestroyOnLoad(this);
        Instance = this;

        squadCanvas = GameObject.FindObjectOfType<Squads>();
        squadCamera = GameObject.FindObjectOfType<Camera>();
    }
    public void TransitToHqs(string hqName)
    {
        StartCoroutine(LoadScene("Headquarters", hqName));   
        StartCoroutine(UnloadScene());   
    }
    public void SquadSceneHandler(bool _switch) // _switch(true) = turn on; _switch(off) = turn off
    {
        if (_switch == true)
        {
            squadCanvas.gameObject.SetActive(true);
            squadCamera.gameObject.SetActive(true);
        }
        else
        {
            squadCanvas.gameObject.SetActive(false);
            squadCamera.gameObject.SetActive(false);
        }
    }
    IEnumerator LoadScene(string sceneName, string hqName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.1f);

        SquadSceneHandler(false);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        var hqCanvas = GameObject.FindObjectOfType<Headquarters>();

        // switch scroll views
        hqCanvas.transform.GetChild(0).gameObject.SetActive(false);
        hqCanvas.transform.GetChild(1).gameObject.SetActive(true);

        // switch headers
        hqCanvas.transform.GetChild(2).gameObject.SetActive(false);
        hqCanvas.transform.GetChild(3).gameObject.SetActive(true);

        HeadquarterInitialisation.Instance._name = hqName;
        StartCoroutine(HeadquarterInitialisation.Instance.InitialiseHeadquarterQuery());
        
        yield break;
    }
    IEnumerator UnloadScene()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (SceneManager.sceneCount == 1)
            {
                SquadSceneHandler(true);
                yield break;
            }
        }
    }
}
