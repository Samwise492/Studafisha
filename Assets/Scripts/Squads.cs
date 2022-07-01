using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class Squads : MonoBehaviour
{
    [SerializeField] Button archiveButton;
    [SerializeField] GameObject archiveContent;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> squads = new List<string>();
    List<string> hqs = new List<string>();
    [SerializeField] GameObject squadShell;
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
    IEnumerator SquadListQuery()
    {       
        WWWForm squadForm = new WWWForm();
        squadForm.AddField("get_squads", "");

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squadList.php", squadForm))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string squad in responseText.Split('|'))
            {
                squads.Add(squad);
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

        for (var i = 0; i < squads.Count; i++)
        {
            if (squads[i] != "")
            {
                Debug.Log(hqs.Count);
                var squad = Instantiate(squadShell, nest.transform);
                squad.transform.GetChild(0).GetComponent<Text>().text = squads[i];
                squad.transform.GetChild(1).GetComponent<Text>().text = hqs[i];
            }
        }
        yield break;
    }
#endregion
}
