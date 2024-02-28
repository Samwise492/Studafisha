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

    // Transitions
    public void TransitToHqsViaSquad(string hqName)
    {
        StartCoroutine(LoadScene("Headquarters", hqName, LoadSceneMode.Additive));   
        StartCoroutine(UnloadScene());   
    }
    public void TransitToHq(string hqName)
    {
        StartCoroutine(LoadScene("Headquarters", hqName, LoadSceneMode.Single));   
    }
    public void TransitToSquad(string squadName)
    {
        StartCoroutine(LoadScene("Squads", squadName, LoadSceneMode.Single));  
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
    
    // Scene management
    IEnumerator LoadScene(string sceneName, string entityName, LoadSceneMode sceneMode)
    {
        if (sceneMode == LoadSceneMode.Additive)
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        else if (sceneMode == LoadSceneMode.Single)
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        yield return new WaitForSeconds(0.9f);

        if (sceneMode == LoadSceneMode.Additive)
        {
            SquadSceneHandler(false);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        }

        if (sceneName == "Headquarters")
        {
            var hqCanvas = GameObject.FindObjectOfType<Headquarters>();

            // switch scroll views
            hqCanvas.transform.GetChild(0).gameObject.SetActive(false);
            hqCanvas.transform.GetChild(1).gameObject.SetActive(true);

            // switch headers
            hqCanvas.transform.GetChild(2).gameObject.SetActive(false);
            hqCanvas.transform.GetChild(3).gameObject.SetActive(true);

            HeadquarterInitialisation.Instance._name = entityName;
            StartCoroutine(HeadquarterInitialisation.Instance.InitialiseHeadquarterQuery());
        }
        else if (sceneName == "Squads")
        {
            var squadCanvas = GameObject.FindObjectOfType<Squads>();

            // switch scroll views
            squadCanvas.transform.GetChild(0).gameObject.SetActive(false);
            squadCanvas.transform.GetChild(1).gameObject.SetActive(true);

            // switch headers
            squadCanvas.transform.GetChild(2).gameObject.SetActive(false);
            squadCanvas.transform.GetChild(3).gameObject.SetActive(true);

            SquadInitialisation.Instance._name = entityName;
            StartCoroutine(SquadInitialisation.Instance.InitialiseSquadQuery());
        }
        
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
