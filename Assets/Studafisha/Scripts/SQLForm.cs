using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SQLForm : MonoBehaviour
{
    public static SQLForm Instance {get;set;}
    [SerializeField] GameObject hqPrefab, squadPrefab;
    List<string> headquarters = new List<string>();
    List<string> squads = new List<string>();
    List<GameObject> positions = new List<GameObject>();
    string headquarter, squad, position;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located

    void Awake()
    {
        Instance = this;
    }

    // hq
    public IEnumerator HeadquartersQuery(GameObject hqPopUp, Text value)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_headquarters.php", ""))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string line in responseText.Split('|'))
                headquarters.Add(line);
        }
        CreateHeadquarters(hqPopUp, value);
        yield break;
    }
    void CreateHeadquarters(GameObject hqPopUp, Text value)
    {
        for (var i = 0; i < headquarters.Count; i++)
        {
            if (headquarters[i] != "")
            {
                if (headquarters[i] != "0 results")
                {
                    var hq = Instantiate(hqPrefab, hqPopUp.transform.GetChild(0).GetChild(0));
                    hq.transform.GetChild(1).GetComponent<Text>().text = headquarters[i];
                    hq.GetComponent<Button>().onClick.AddListener(() => OnClickInfoValue(hq.GetComponent<Button>(), hqPopUp, value));
                }
            }
        }
    }

    // squad
    public IEnumerator SquadsQuery(string headquarter, GameObject squadPopUp, Text value)
    {
        squads.Clear();
        WWWForm form = new WWWForm();
        form.AddField("University", headquarter);
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squads.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string line in responseText.Split('|'))
                squads.Add(line);  
        }
        CreateSquads(squadPopUp, value);
        yield break;
    }
    void CreateSquads(GameObject squadPopUp, Text value)
    {
        foreach (Transform child in squadPopUp.transform.GetChild(0).GetChild(0))
            GameObject.Destroy(child.gameObject);
        for (var i = 0; i < squads.Count; i++)
        {
            if (squads[i] != "")
            {
                if (squads[i] != "0 results")
                {
                    var squad = Instantiate(squadPrefab, squadPopUp.transform.GetChild(0).GetChild(0));
                    squad.transform.GetChild(1).GetComponent<Text>().text = squads[i];
                    squad.GetComponent<Button>().onClick.AddListener(() => OnClickInfoValue(squad.GetComponent<Button>(), squadPopUp, value));
                }
            }
        }
    }

    // position
    public IEnumerator PositionsQuery(string headquarter, GameObject positionPopUp, Text value) 
    {
        foreach (Transform position in positionPopUp.transform.GetChild(0).GetChild(0))
        {
            positions.Add(position.gameObject);
            position.GetComponent<Button>().onClick.AddListener(() => OnClickInfoValue(position.GetComponent<Button>(), positionPopUp, value));
        }
        yield break;
    }
    // common
    void OnClickInfoValue(Button button, GameObject popUp, Text value)
    {
        value.text = button.transform.GetChild(1).GetComponent<Text>().text;
        popUp.SetActive(false);
    }
}
