using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Linq;

public class Squads : MonoBehaviour
{
    [SerializeField] ScrollRect scrollViewMenu, scrollViewSquad;
    [SerializeField] GameObject headerMain, headerSquad;
    [SerializeField] Button archiveButton;
    [SerializeField] GameObject archiveContent, historyContent, historicalPeopleContent;
    Dictionary<string, string> historicalPeople = new Dictionary<string, string>();
    [SerializeField] Button vkButton, mailButton;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> squads = new List<string>();
    List<string> squadCities = new List<string>();
    List<string> hqs = new List<string>();
    [SerializeField] GameObject squadShell, historicalPersonShell, dividerShell;
    [SerializeField] GameObject nest;

    void Start()
    {
        StartCoroutine(SquadListQuery());
    }

#region Buttons
    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
    void OnClickOpenContent(Button button, GameObject content)
    {
        if (button.GetComponent<Image>().isActiveAndEnabled == true)
        {
            button.GetComponent<Image>().enabled = false;
            button.transform.GetChild(0).gameObject.SetActive(true);
            content.SetActive(true);
        }
        else
        {
            button.GetComponent<Image>().enabled = true;
            button.transform.GetChild(0).gameObject.SetActive(false);
            content.SetActive(false);
        }
    }
    public void OnClickArchive() => OnClickOpenContent(archiveButton, archiveContent);
    public void OnClickOpenHistory() 
    {
        if (SquadInitialisation.Instance.history != "")
        {
            historyContent.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = SquadInitialisation.Instance.history;
            foreach(Transform child in scrollViewSquad.transform.GetChild(0).GetChild(0))
            {
                if (child != historyContent)
                    child.gameObject.SetActive(false);
            }
            historyContent.SetActive(true);
        }
    }
    public void OnClickOpenHistoricalPeople() 
    {
        StartCoroutine(SquadHistoricalPeopleQuery());
    }
    public void OnClickOpenHeadquarter()
    {   
        PosterTransition.Instance.TransitToHqs(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text);
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
        if (historyContent.activeSelf == false && historicalPeopleContent.activeSelf == false)
        {
            headerSquad.gameObject.SetActive(false);
            scrollViewSquad.gameObject.SetActive(false);
            headerMain.gameObject.SetActive(true);
            scrollViewMenu.gameObject.SetActive(true);
        }
        else if (historyContent.activeSelf == true)
        {
            foreach(Transform child in scrollViewSquad.transform.GetChild(0).GetChild(0))
            {
                if (child != historyContent)
                    child.gameObject.SetActive(true);
            }
            historyContent.SetActive(false);
            historicalPeopleContent.SetActive(false);
        }
        else if (historicalPeopleContent.activeSelf == true)
        {
            foreach(Transform child in scrollViewSquad.transform.GetChild(0).GetChild(0))
            {
                if (child != historicalPeopleContent)
                    child.gameObject.SetActive(true);
            }
            historyContent.SetActive(false);
            historicalPeopleContent.SetActive(false);
        }
    }
    public void OnClickOpenVK() => Application.OpenURL(SquadInitialisation.Instance.vk);
    public void OnClickOpenMail() => Application.OpenURL(SquadInitialisation.Instance.mail);
#endregion

#region SQL
    IEnumerator SquadListQuery()
    {       
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
        SortSquads(instantiatedSquads);

        yield break;
    } 
    IEnumerator SquadHistoricalPeopleQuery()
    {
        historicalPeople.Clear();
        foreach (Transform child in historicalPeopleContent.transform)
        {
            Destroy(child);
        }

        WWWForm squadForm = new WWWForm();
        squadForm.AddField("Type", SquadInitialisation.Instance.type);
        squadForm.AddField("Name", SquadInitialisation.Instance._name);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squad_historicalPeople.php", squadForm))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            
            for (var i = 0; i < responseText.Split('|').Length; i+=2)
            {
                if (responseText.Split('|')[i] != "")
                {
                    if (i+1 < responseText.Split('|').Length)
                    {
                        historicalPeople.Add(responseText.Split('|')[i], responseText.Split('|')[i+1]);
                    }
                }
            }
        }
        
        if (historicalPeople.Count > 0)
        {
            for (var i = 0; i < historicalPeople.Count; i++)
            {
                var historicalPerson = Instantiate(historicalPersonShell, historicalPeopleContent.transform);
                historicalPerson.transform.GetChild(3).GetComponent<Text>().text = historicalPeople.ElementAt(i).Key;
                historicalPerson.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = historicalPeople.ElementAt(i).Value;
            }

            foreach(Transform child in scrollViewSquad.transform.GetChild(0).GetChild(0))
            {
                if (child != historicalPeopleContent)
                    child.gameObject.SetActive(false);
            }
            historicalPeopleContent.SetActive(true);
        }

        yield break;
    }
#endregion
    void InitialiseSquad()
    {
        headerMain.gameObject.SetActive(false);
        scrollViewMenu.gameObject.SetActive(false);
        headerSquad.gameObject.SetActive(true);
        scrollViewSquad.gameObject.SetActive(true);
        
        SquadInitialisation.Instance._name = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text.Split(' ')[1];
        SquadInitialisation.Instance.type = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text.Split(' ')[0];
        SquadInitialisation.Instance.hq = EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Text>().text;
        StartCoroutine(SquadInitialisation.Instance.InitialiseSquadQuery());
        if (SquadInitialisation.Instance.vk == "" && SquadInitialisation.Instance.mail == "")
            vkButton.transform.parent.gameObject.SetActive(false);
        if (SquadInitialisation.Instance.vk == "")
            vkButton.gameObject.SetActive(false);
        if (SquadInitialisation.Instance.mail == "")
            mailButton.gameObject.SetActive(false);  
    }
    void SortSquads(List<GameObject> _squads)
    {
        // instantiate dividers
        int uniqueCitiesNumber = (from x in squadCities select x).Distinct().Count();
        List<string> addedCities = new List<string>();
        List<GameObject> dividers = new List<GameObject>();

        for (int i = 0; i < uniqueCitiesNumber; i++)
        {
            var divider = Instantiate(dividerShell, nest.transform);
            dividers.Add(divider);

            for (int j = 0; j < squadCities.Count; j++)
            {
                if (!addedCities.Contains(squadCities[j]))
                    divider.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = squadCities[j];
            }
            addedCities.Add(divider.transform.GetChild(0).GetChild(0).GetComponent<Text>().text);
        }

        // sort
        foreach (var divider in dividers)
        {
            for (int i = 0; i < squadCities.Count; i++)
            {
                if (squadCities[i] == divider.transform.GetChild(0).GetChild(0).GetComponent<Text>().text)
                {
                    _squads[i].transform.SetParent(divider.transform.GetChild(1));
                }
            }
        }
    }
}
