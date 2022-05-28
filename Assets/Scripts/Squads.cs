using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squads : MonoBehaviour
{
    [SerializeField] Button archiveButton;
    [SerializeField] GameObject archiveContent;

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
}
