using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class SquadsAndHeadquarters : MonoBehaviour
{
    [SerializeField] ScrollRect scrollViewMenu;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> headquarters = new List<string>();
    List<string> squads = new List<string>();
    [SerializeField] GameObject headquarterShell, squadShell;
    [SerializeField] GameObject nest;

    void Start()
    {
        StartCoroutine(AllListQuery());
    }

#region Buttons
    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single); historyContent.SetActive(true);
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
#endregion

    void OpenHq()
    {
        SceneManager.LoadSceneAsync("Headquarters", LoadSceneMode.Single);
        GameObject.Find("CanvasHq").GetComponent<Headquarters>().BroadcastMessage("InitialiseHeadquarter");
    }
#region SQL
    IEnumerator AllListQuery()
    {       
        // Hqs
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
                headquarter.GetComponent<Button>().onClick.AddListener(OpenHq);
            }
        }

        // Squads
        WWWForm squadForm = new WWWForm();
        squadForm.AddField("get_squads", "");

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squadList.php", squadForm))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            for (int i = 0; i < responseText.Split('|').Length; i+=2)
            {
                if (responseText.Split('|')[i] != "")
                {
                    squadCities.Add(responseText.Split('|')[i]);
                    squads.Add(responseText.Split('|')[i+1]);
                }
            }
        }

        WWWForm hqForm = new WWWForm();
        hqForm.AddField("get_hqs", "");

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squadList.php", hqForm))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string hq in responseText.Split('|'))
            {
                hqs.Add(hq);
            }
        }

        List<GameObject> instantiatedSquads = new List<GameObject>();
        for (var i = 0; i < squads.Count; i++)
        {
            if (squads[i] != "")
            {
                var squad = Instantiate(squadShell, nest.transform);
                squad.transform.GetChild(0).GetComponent<Text>().text = squads[i];
                squad.transform.GetChild(1).GetComponent<Text>().text = hqs[i];
                squad.GetComponent<Button>().onClick.AddListener(InitialiseSquad);

                instantiatedSquads.Add(squad);
            }
        }

        yield break;
    } 
#endregion
}
