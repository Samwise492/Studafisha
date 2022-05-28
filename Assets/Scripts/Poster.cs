using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Poster : MonoBehaviour
{
    public static Poster Instance {get; set;}
    [SerializeField] ScrollRect scrollView;
    [SerializeField] Image headerMain, headerEvent, footer;
    [SerializeField] Image headerEvent_mainTitle, headerEvent_participantTitle, headerEvent_volunteerTitle;
    [SerializeField] Image scrollViewMain, scrollViewEvent, scrollViewEvent_Participant, scrollViewEvent_Volunteer, scrollViewInfo_EventPlan;
    [SerializeField] Button creatorsButton, whatBringOnButton, signUpButton, infoButton;
    [SerializeField] GameObject creatorsContent, whatBringOnContent, signUpContent, infoContent;

    [SerializeField] Button autoSignUpParticipantButton, manualSignUpParticipantButton, autoSignUpVolunteerButton, manualSignUpVolunteerButton;
    [SerializeField] GameObject autoSignUpParticipantContent, manualSignUpParticipantContent, autoSignUpVolunteerContent, manualSignUpVolunteerContent;
    [SerializeField] GameObject photoLayoutContent, albumContent;

    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
    public void OnClickEvent()
    {
        headerMain.gameObject.SetActive(false);
        scrollViewMain.gameObject.SetActive(false);
        headerEvent.gameObject.SetActive(true);
        scrollViewEvent.gameObject.SetActive(true);
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
    public void OnClickCreators() => OnClickOpenContent(creatorsButton, creatorsContent);
    public void OnClickWhatBringOn() => OnClickOpenContent(whatBringOnButton, whatBringOnContent);
    public void OnClickSignUp() => OnClickOpenContent(signUpButton, signUpContent);
    public void OnClickInfo() => OnClickOpenContent(infoButton, infoContent);

    // Sign Up
    public void OnClickSignUp_Participant()
    {
        scrollViewEvent.gameObject.SetActive(false);
        headerEvent_mainTitle.gameObject.SetActive(false);
        scrollViewEvent_Participant.gameObject.SetActive(true);
        headerEvent_participantTitle.gameObject.SetActive(true);
    }
    public void OnClickSignUp_Volunteer()
    {
        scrollViewEvent.gameObject.SetActive(false);
        headerEvent_mainTitle.gameObject.SetActive(false);
        scrollViewEvent_Volunteer.gameObject.SetActive(true);
        headerEvent_volunteerTitle.gameObject.SetActive(true);
    }
    public void OnClickAutoSignUpParticipant() => OnClickOpenContent(autoSignUpParticipantButton, autoSignUpParticipantContent);
    public void OnClickManualSignUpParticipant() => OnClickOpenContent(manualSignUpParticipantButton, manualSignUpParticipantContent);
    public void OnClickAutoSignUpVolunteer() => OnClickOpenContent(autoSignUpVolunteerButton, autoSignUpVolunteerContent);
    public void OnClickManualSignUpVolunteer() => OnClickOpenContent(manualSignUpVolunteerButton, manualSignUpVolunteerContent);

    // Info
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
}
