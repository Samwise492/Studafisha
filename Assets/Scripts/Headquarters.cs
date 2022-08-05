using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Headquarters : MonoBehaviour
{
    [SerializeField] ScrollRect scrollViewMenu, scrollViewHeadquarter;
    [SerializeField] GameObject headerMain, headerHeadquarter;
    [SerializeField] Button historyButton;
    [SerializeField] GameObject historyContent;
    [SerializeField] Button vkButton, mailButton;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> headquarters = new List<string>();
    [SerializeField] GameObject headquarterShell;
    [SerializeField] GameObject nest;

    void Start()
    {
        StartCoroutine(HeadquarterListQuery());
    }

#region Buttons
    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
    public void OnClickOpenHistory() 
    {
        historyContent.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = HeadquarterInitialisation.Instance.history;
        foreach(Transform child in scrollViewHeadquarter.transform.GetChild(0).GetChild(0))
        {
            if (child != historyContent)
                child.gameObject.SetActive(false);
        }
        historyContent.SetActive(true);
    }
    public void FooterSwitch()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Library":
                SceneManager.LoadSceneAsync("Library", LoadSceneMode.Single);
                break;
            case "Poster":
                SceneManager.LoadSceneAsync("Poster", LoadSceneMode.Single);
                break;
            case "Squads":
                SceneManager.LoadSceneAsync("Squads", LoadSceneMode.Single);
                break;
            case "Shop":
                SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Single);
                break;
        }   
    }
    public void OnClickBackToMenu()
    {
        if (historyContent.activeSelf == false)
        {
            if (SceneManager.sceneCount == 2)
            {
                SceneManager.UnloadSceneAsync("Headquarters");
            }
            else
            {
                headerHeadquarter.gameObject.SetActive(false);
                scrollViewHeadquarter.gameObject.SetActive(false);
                headerMain.gameObject.SetActive(true);
                scrollViewMenu.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach(Transform child in scrollViewHeadquarter.transform.GetChild(0).GetChild(0))
            {
                if (child != historyContent)
                    child.gameObject.SetActive(true);
            }
            historyContent.SetActive(false);
        }
    }
    public void OnClickOpenVK() => Application.OpenURL(HeadquarterInitialisation.Instance.vk);
    public void OnClickOpenInstagram() => Application.OpenURL("mailto:" + HeadquarterInitialisation.Instance.mail
     + "?subject=" + "" + "&body=" + "");
#endregion

#region SQL
    IEnumerator HeadquarterListQuery()
    {       
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_headquarterList.php", ""))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string headquarter in responseText.Split('|'))
            {
                headquarters.Add(headquarter);
            }
        }

        for (var i = 0; i < headquarters.Count; i++)
        {
            if (headquarters[i] != "")
            {
                var headquarter = Instantiate(headquarterShell, nest.transform);
                headquarter.transform.GetChild(0).GetComponent<Text>().text = headquarters[i];
                headquarter.GetComponent<Button>().onClick.AddListener(InitialiseHeadquarter);
            }
        }
        yield break;
    } 
#endregion
    void InitialiseHeadquarter()
    {
        headerMain.gameObject.SetActive(false);
        scrollViewMenu.gameObject.SetActive(false);
        headerHeadquarter.gameObject.SetActive(true);
        scrollViewHeadquarter.gameObject.SetActive(true);
        
        HeadquarterInitialisation.Instance._name = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text;
        StartCoroutine(HeadquarterInitialisation.Instance.InitialiseHeadquarterQuery());
        if (HeadquarterInitialisation.Instance.vk == "")
            vkButton.gameObject.SetActive(false);
        if (HeadquarterInitialisation.Instance.mail == "")
            mailButton.gameObject.SetActive(false);  
    }
}
