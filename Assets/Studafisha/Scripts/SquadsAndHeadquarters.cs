using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Linq;

public class SquadsAndHeadquarters : MonoBehaviour
{
    [SerializeField] ScrollRect scrollViewMenu;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> headquarters = new List<string>();
    List<string> squads = new List<string>();
    List<string> squadHqs = new List<string>();
    List<string> squadCities = new List<string>();
    [SerializeField] GameObject headquarterShell, squadShell, dividerShell;
    [SerializeField] GameObject nest, hqNest;
    [SerializeField] SideMenu sideMenu;

    void Start()
    {
        StartCoroutine(AllListQuery());
    }

#region Buttons
    public void OnClickAvatar() => sideMenu.gameObject.SetActive(true);
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
                var headquarter = Instantiate(headquarterShell, hqNest.transform);
                headquarter.transform.GetChild(0).GetComponent<Text>().text = headquarters[i];
                headquarter.GetComponent<Button>().onClick.AddListener(OnClickOpenHeadquarter);
                headquarter.GetComponent<RectTransform>().sizeDelta = new Vector2(920, 175);
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
                squadHqs.Add(hq);
            }
        }

        List<GameObject> instantiatedSquads = new List<GameObject>();
        for (var i = 0; i < squads.Count; i++)
        {
            if (squads[i] != "")
            {
                var squad = Instantiate(squadShell, nest.transform);
                squad.transform.GetChild(0).GetComponent<Text>().text = squads[i];
                squad.transform.GetChild(1).GetComponent<Text>().text = squadHqs[i];
                squad.GetComponent<Button>().onClick.AddListener(OnClickOpenSquad);

                instantiatedSquads.Add(squad);
            }
        }
        SortSquads(instantiatedSquads);

        yield break;
    } 
#endregion

    public void OnClickOpenHeadquarter()
    {   
        PosterTransition.Instance.TransitToHq
        (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
    }
    public void OnClickOpenSquad()
    {   
        PosterTransition.Instance.TransitToSquad
        (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
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
