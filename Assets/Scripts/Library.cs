using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Library : MonoBehaviour
{
    [SerializeField] ScrollRect scrollViewMain, scrollViewBuild, scrollViewTeachers, scrollViewPins;
    [SerializeField] Image header;
    [SerializeField] Button labourProjectsButton;
    [SerializeField] GameObject labourProjectsContent;

    void OnClickOpenButton(ScrollRect viewToOn)
    {
        header.transform.GetChild(0).gameObject.SetActive(false); // avatar
        header.transform.GetChild(1).gameObject.SetActive(true); // back button
        scrollViewMain.gameObject.SetActive(false);
        viewToOn.gameObject.SetActive(true);
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
    public void OnClickOpenBuild() => OnClickOpenButton(scrollViewBuild);
    public void OnClickOpenTeachers() => OnClickOpenButton(scrollViewTeachers);
    public void OnClickOpenPins() => OnClickOpenButton(scrollViewPins);
    public void OnClickLaborProjects() => OnClickOpenContent(labourProjectsButton, labourProjectsContent);

    public void OnClickBack()
    {
        header.transform.GetChild(0).gameObject.SetActive(true); // avatar
        header.transform.GetChild(1).gameObject.SetActive(false); // back button
        scrollViewMain.gameObject.SetActive(true);
        scrollViewBuild.gameObject.SetActive(false);
        scrollViewTeachers.gameObject.SetActive(false);
        scrollViewPins.gameObject.SetActive(false);
    }
}
