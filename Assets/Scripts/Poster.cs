using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Poster : MonoBehaviour
{
    [SerializeField] Image headerMain, headerEvent, footer;
    [SerializeField] Image headerEvent_mainTitle, headerEvent_participantTitle, headerEvent_volunteerTitle;
    [SerializeField] Image viewpointMain, viewpointEvent;
    [SerializeField] Button creatorsButton, whatBringOnButton, signUpButton, infoButton;
    [SerializeField] GameObject creatorsContent, whatBringOnContent, signUpContent, infoContent;

    public void OnClickAvatar() => SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
    public void OnClickEvent()
    {
        headerMain.enabled = false;
        headerEvent.enabled = true;
        viewpointMain.enabled = false;
        viewpointEvent.enabled = true;
    }
    public void OnClickBackToMenu(Image presentHeader, Image nextHeader, Image presentViewpoint, Image nextViewpoint)
    {
        if (nextHeader == headerMain)
        {
            nextHeader.enabled = true;
            presentHeader.enabled = false;
        }
        else
        {
            // поменяй логику на прошлой кнопке бэка
            //if 
        }
        nextViewpoint.enabled = true;
        presentViewpoint.enabled = false;
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

    }
    public void OnClickSignUp_Volunteer()
    {

    }
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
