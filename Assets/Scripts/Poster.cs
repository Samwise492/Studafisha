using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Poster : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;
    [SerializeField] Image headerMain, headerEvent, footer;
    [SerializeField] Image headerEvent_mainTitle, headerEvent_participantTitle, headerEvent_volunteerTitle;
    [SerializeField] Image viewportMain, viewportEvent, viewportEvent_Participant, viewportEvent_Volunteer;
    [SerializeField] Button creatorsButton, whatBringOnButton, signUpButton, infoButton;
    [SerializeField] GameObject creatorsContent, whatBringOnContent, signUpContent, infoContent;

    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
    public void OnClickEvent()
    {
        headerMain.gameObject.SetActive(false);
        viewportMain.gameObject.SetActive(false);
        headerEvent.gameObject.SetActive(true);
        viewportEvent.gameObject.SetActive(true);

        // change content scroll view unity
        //scrollView.set = viewportEvent;
    }
    public void OnClickBackToMenu()
    {
        if (viewportEvent.IsActive() == true) // main event window is opened
        {
            headerEvent.gameObject.SetActive(false);
            viewportEvent.gameObject.SetActive(false);
            headerMain.gameObject.SetActive(true);
            viewportMain.gameObject.SetActive(true);
        }
        if (viewportEvent_Participant.IsActive() == true) // participant event window is opened
        {
            headerEvent_participantTitle.gameObject.SetActive(false);
            viewportEvent_Participant.gameObject.SetActive(false);
            headerEvent_mainTitle.gameObject.SetActive(true);
            viewportEvent.gameObject.SetActive(true);
        }
        if (viewportEvent_Volunteer.IsActive() == true) // volunteer event window is opened
        {
            headerEvent_volunteerTitle.gameObject.SetActive(false);
            viewportEvent_Volunteer.gameObject.SetActive(false);
            headerEvent_mainTitle.gameObject.SetActive(true);
            viewportEvent.gameObject.SetActive(true);
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
    public void OnClickSignUp_Participant() => viewportEvent_Participant.gameObject.SetActive(true);
    public void OnClickSignUp_Volunteer() => viewportEvent_Volunteer.gameObject.SetActive(true);

    // Info
    public void OnClickInfo_Plan()
    {

    }
    public void OnClickInfo_Group()
    {

    }
    public void OnClickInfo_Photos()
    {

    }
}
