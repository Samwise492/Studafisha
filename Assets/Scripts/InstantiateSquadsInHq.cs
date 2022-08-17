using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InstantiateSquadsInHq : MonoBehaviour
{
    [SerializeField] GameObject squadShell;
    List<string> squads = new List<string>();
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    string hq;

    void Start()
    {
        StartCoroutine(SquadListInHqQuery());
    }
    

    IEnumerator SquadListInHqQuery()
    {       
        yield return new WaitForSeconds(0.5f);

        string[] splittedHqName = transform.parent.parent.GetChild(0).GetComponent<Text>().text.Split(' ');
        List<string> wholeHqName = new List<string>();
        foreach(var word in splittedHqName)
        {
            if (word != "Штаб")
                wholeHqName.Add(word);
        }
        foreach(var word in wholeHqName)
        {
            hq += word + " ";
        }
        hq.Trim(' ');

        WWWForm squadForm = new WWWForm();
        squadForm.AddField("Hq_name", hq);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squadListForHq.php", squadForm))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            for (int i = 0; i < responseText.Split('|').Length; i++)
            {
                if (responseText.Split('|')[i] != "")
                {
                    squads.Add(responseText.Split('|')[i]);
                }
            }
        }

        for (var i = 0; i < squads.Count; i++)
        {
            if (squads[i] != "")
            {
                var squad = Instantiate(squadShell, this.gameObject.transform);
                squad.transform.GetChild(0).GetComponent<Text>().text = squads[i];
                squad.transform.GetChild(1).GetComponent<Text>().text = hq;
                squad.GetComponent<Button>().onClick.AddListener(OnClickOpenSquad);
            }
        }

        yield break;
    }

    public void OnClickOpenSquad()
    {   
        PosterTransition.Instance.TransitToSquad
        (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
    }
}
