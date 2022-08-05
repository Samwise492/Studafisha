using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using System.Linq;

public class Poster : MonoBehaviour
{
    public static Poster Instance {get; set;}
    [SerializeField] Image headerMain, headerEvent, footer;
    [SerializeField] Image headerEvent_mainTitle, headerEvent_participantTitle, headerEvent_volunteerTitle;
    [SerializeField] Image scrollViewMain, scrollViewEvent, scrollViewEvent_Participant, scrollViewEvent_Volunteer, scrollViewInfo_EventPlan;
    [SerializeField] Button creatorsButton, whatBringOnButton, signUpButton, infoButton;
    [SerializeField] GameObject creatorsContent, whatBringOnContent, signUpContent, infoContent;

    public string eventTitle, eventId;
    public bool isVolunteer;
    [SerializeField] GameObject photoLayoutContent, albumContent;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<string> organizers = new List<string>();
    List<string> organizerSquadIds = new List<string>(); // change to int
    List<string> organizerSquads = new List<string>();
    List<string> organizerPhoneNumbers = new List<string>();
    List<string> itemsToBring = new List<string>();
    [SerializeField] GameObject creatorShell, itemToBringShell;

    void Awake()
    {
        Instance = this;
        if (UserData.Instance.login == "/Guest")
            signUpButton.gameObject.SetActive(false);
    }

#region Buttons
    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
    public void OnClickEvent(string[] info)
    {
        headerMain.gameObject.SetActive(false);
        scrollViewMain.gameObject.SetActive(false);
        headerEvent.gameObject.SetActive(true);
        scrollViewEvent.gameObject.SetActive(true);

        var _event = scrollViewEvent.transform.GetChild(0).GetChild(0);
        eventTitle = info[0];
        eventId = info[16];
        _event.GetChild(2).GetComponent<Text>().text = eventTitle; // title
        _event.GetChild(3).GetComponent<Text>().text = info[1]; // type
        _event.GetChild(4).GetChild(1).GetComponent<Text>().text = info[3]; // description
        _event.GetChild(5).GetChild(0).GetComponent<Text>().text = info[9]; // address
        StartCoroutine(OrganizersQuery(info[4], info[5], info[6], info[7]));
        StartCoroutine(ItemsToBringQuery(info[0]));
    }
    public void OnClickBackToMenu()
    {
        if (scrollViewEvent.gameObject.activeSelf == true) // main event window is opened
        {
            headerEvent.gameObject.SetActive(false);
            scrollViewEvent.gameObject.SetActive(false);
            headerMain.gameObject.SetActive(true);
            scrollViewMain.gameObject.SetActive(true);
        }
        if (scrollViewEvent_Participant.gameObject.activeSelf == true) // participant event window is opened
        {
            headerEvent_participantTitle.gameObject.SetActive(false);
            scrollViewEvent_Participant.gameObject.SetActive(false);
            headerEvent_mainTitle.gameObject.SetActive(true);
            scrollViewEvent.gameObject.SetActive(true);
        }
        if (scrollViewEvent_Volunteer.gameObject.activeSelf == true) // volunteer event window is opened
        {
            headerEvent_volunteerTitle.gameObject.SetActive(false);
            scrollViewEvent_Volunteer.gameObject.SetActive(false);
            headerEvent_mainTitle.gameObject.SetActive(true);
            scrollViewEvent.gameObject.SetActive(true);
        }
        if (scrollViewInfo_EventPlan.gameObject.activeSelf == true) // event plan window is opened
        {
            headerEvent.gameObject.SetActive(false);
            scrollViewInfo_EventPlan.gameObject.SetActive(false);
            headerEvent.gameObject.SetActive(true);
            scrollViewEvent.gameObject.SetActive(true);
        }
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
    public void OnClickCall()
    {
        var organizer = EventSystem.current.currentSelectedGameObject.transform.parent.GetChild(1).GetComponent<Text>().text;
        int id = organizers.IndexOf(organizer);
        Application.OpenURL("tel://" + organizerPhoneNumbers[id]);
    }
    void OnClickOpenContent(Button button, GameObject content)
    {
        if (button.GetComponent<Image>().isActiveAndEnabled == true)
        {
            button.GetComponent<Image>().enabled = false;
            button.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(RefreshContent(content, true));
        }
        else
        {
            button.GetComponent<Image>().enabled = true;
            button.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(RefreshContent(content, false));
        }
    }
    IEnumerator RefreshContent(GameObject content, bool _true)
    {
        content.SetActive(_true);
        yield return new WaitForEndOfFrame();
        content.SetActive(!_true);
        yield return new WaitForEndOfFrame();
        content.SetActive(_true);
        yield break;
    }
    public void OnClickCreators() => OnClickOpenContent(creatorsButton, creatorsContent);
    public void OnClickWhatBringOn() => OnClickOpenContent(whatBringOnButton, whatBringOnContent);
    public void OnClickSignUp() => OnClickOpenContent(signUpButton, signUpContent);
    public void OnClickInfo() => OnClickOpenContent(infoButton, infoContent);
#endregion

#region SignUp
    public void OnClickSignUp_Participant()
    {
        scrollViewEvent.gameObject.SetActive(false);
        headerEvent_mainTitle.gameObject.SetActive(false);
        scrollViewEvent_Participant.gameObject.SetActive(true);
        headerEvent_participantTitle.gameObject.SetActive(true);

        scrollViewEvent_Participant.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>().text = eventTitle; // title
        isVolunteer = false;
    }
    public void OnClickSignUp_Volunteer()
    {
        scrollViewEvent.gameObject.SetActive(false);
        headerEvent_mainTitle.gameObject.SetActive(false);
        scrollViewEvent_Volunteer.gameObject.SetActive(true);
        headerEvent_volunteerTitle.gameObject.SetActive(true);

        scrollViewEvent_Volunteer.transform.GetChild(0).GetChild(0).GetChild(4).GetComponent<Text>().text = eventTitle; // title
        isVolunteer = true;
    }
#endregion

#region Info
    public void OnClickInfo_Plan() 
    {
        scrollViewEvent.gameObject.SetActive(false);
        headerMain.gameObject.SetActive(false);
        scrollViewInfo_EventPlan.gameObject.SetActive(true);
        headerEvent.gameObject.SetActive(true);
    }
    public void OnClickInfo_Group()
    {

    }
    public void OnClickInfo_Photos()
    {
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().isActiveAndEnabled == true)
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().enabled = false;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(true);
            photoLayoutContent.SetActive(true);
            albumContent.SetActive(true);
        }
        else
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().enabled = true;
            EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.SetActive(false);
            photoLayoutContent.SetActive(false);
            albumContent.SetActive(false);
        }
    }
#endregion

#region SQL
    IEnumerator OrganizersQuery(string organizer1, string organizer2, string organizer3, string organizer4) //change to int
    {
        WWWForm form = new WWWForm();
        if (organizer1 != null || organizer1 != "")
            form.AddField("Organizer1_Id", organizer1);
        if (organizer2 != null || organizer2 != "")
            form.AddField("Organizer2_Id", organizer2);
        if (organizer3 != null || organizer3 != "")
            form.AddField("Organizer3_Id", organizer3);
        if (organizer4 != null || organizer4 != "")
            form.AddField("Organizer4_Id", organizer4);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_eventOrganizers.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string organizer in responseText.Split('|'))
            {
                var splittedInfo = organizer.Split('ì');
                if (splittedInfo[0] != "")
                {
                    organizers.Add(splittedInfo[0]);
                    organizerSquadIds.Add(splittedInfo[1]);
                    organizerPhoneNumbers.Add(splittedInfo[2]);
                }
            }
        }
        StartCoroutine(OrganizerIdsQuery());
        yield break;
    }
    IEnumerator OrganizerIdsQuery()
    {
        WWWForm form = new WWWForm();

        switch (organizerSquadIds.Count)
        {
            case 0:
                break;
            case 1:
                form.AddField("Organizer1_SquadId", organizerSquadIds[0]);
                break;
            case 2:
                form.AddField("Organizer1_SquadId", organizerSquadIds[0]);
                form.AddField("Organizer2_SquadId", organizerSquadIds[1]);
                break;
            case 3:
                form.AddField("Organizer1_SquadId", organizerSquadIds[0]);
                form.AddField("Organizer2_SquadId", organizerSquadIds[1]);
                form.AddField("Organizer3_SquadId", organizerSquadIds[2]);
                break;
            case 4:
                form.AddField("Organizer1_SquadId", organizerSquadIds[0]);
                form.AddField("Organizer2_SquadId", organizerSquadIds[1]);
                form.AddField("Organizer3_SquadId", organizerSquadIds[2]);
                form.AddField("Organizer4_SquadId", organizerSquadIds[3]);
                break;
        }
        
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_eventOrganizers.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string squad in responseText.Split('|'))
            {
                organizerSquads.Add(squad);
            }
        }

        for (var i = 0; i < organizers.Count; i++)
        {
            var creator = Instantiate(creatorShell, creatorsContent.transform);
            creator.transform.GetChild(1).GetComponent<Text>().text = organizers[i];
            creator.transform.GetChild(2).GetComponent<Text>().text = organizerSquads[i];
            creator.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(OnClickCall);
        }
        yield break;
    }
    IEnumerator ItemsToBringQuery(string eventName)
    {       
        WWWForm form = new WWWForm();
        form.AddField("EventName", eventName);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_eventItems.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            foreach(string squad in responseText.Split(' '))
            {
                itemsToBring.Add(squad);
            }
        }

        for (var i = 0; i < itemsToBring.Count; i++)
        {
            if (itemsToBring[i] != "")
            {
                var itemToBring = Instantiate(itemToBringShell, whatBringOnContent.transform);
                itemToBring.transform.GetChild(0).GetComponent<Text>().text = itemsToBring[i];
            }
        }
        yield break;
    }
#endregion
}
