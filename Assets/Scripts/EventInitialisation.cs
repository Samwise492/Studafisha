using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EventInitialisation : MonoBehaviour
{
    [SerializeField] GameObject eventPrefab;
    [SerializeField] GameObject eventNest;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> events = new List<string>();
    
    void Start()
    {
        StartCoroutine(EventsQuery());
    }

    public IEnumerator EventsQuery()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_events.php", ""))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string eventInfo in responseText.Split('|'))
            {
                if (eventInfo != "")
                    events.Add(eventInfo);
            }
        }
        CreateEvents();
        yield break;
    }
    void CreateEvents()
    {
        for (var i = 0; i < events.Count; i++)
        {
            var _event = Instantiate(eventPrefab, eventNest.transform);
            string[] splittedInfo = events[i].Split('ì'); // the whole line
            string name = splittedInfo[0];
            string type = splittedInfo[1];
            string date = DateConverter(splittedInfo[2], '.');
            _event.transform.GetChild(0).GetComponent<Text>().text = name;
            _event.transform.GetChild(1).GetComponent<Text>().text = type;
            _event.transform.GetChild(3).GetComponent<Text>().text = date;
            _event.GetComponent<Button>().onClick.AddListener(() => Poster.Instance.OnClickEvent(splittedInfo));
        }
    }
    string DateConverter(string date, char sign)
    {
        string[] splittedDate = date.Split('-');
        date = splittedDate[2] + sign + splittedDate[1] + sign + splittedDate[0];
        return date;
    }
}
