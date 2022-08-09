using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Library : MonoBehaviour
{
    [SerializeField] ScrollRect scrollViewMain;
    [SerializeField] Image header;
    [SerializeField] GameObject articleShell;
    [SerializeField] Button pdfButton1, pdfButton2;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    string articleText;
    string link1, link2;

#region Buttons
    public void OnClickOpenArticle()
    {
        pdfButton1.gameObject.SetActive(false);
        pdfButton2.gameObject.SetActive(false);

        header.transform.GetChild(0).gameObject.SetActive(false); // avatar
        header.transform.GetChild(1).gameObject.SetActive(true); // back button
        foreach (Transform child in articleShell.transform.parent)
        {
            if (child.gameObject != articleShell)
                child.gameObject.SetActive(false);
        }
        articleShell.SetActive(true);

        articleShell.gameObject.SetActive(true);
        StartCoroutine(LibraryArticleQuery(EventSystem.current.currentSelectedGameObject.transform.GetChild(0).name.Split(' ')[1]));
    }
    public void OnClickOpenPDF1() => Application.OpenURL(link1);
    public void OnClickOpenPDF2() => Application.OpenURL(link2);

    public void OnClickBack()
    {
        header.transform.GetChild(0).gameObject.SetActive(true); // avatar
        header.transform.GetChild(1).gameObject.SetActive(false); // back button
        foreach (Transform child in articleShell.transform.parent)
        {
            if (child.gameObject != articleShell)
                child.gameObject.SetActive(true);
        }
        articleShell.SetActive(false);
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
#endregion

#region SQL
    IEnumerator LibraryArticleQuery(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("Id", id);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_libraryArticleData.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            Debug.Log(responseText);

            articleText = responseText.Split('|')[0];
            link1 = responseText.Split('|')[1];
            link2 = responseText.Split('|')[2];
        }

        articleShell.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = articleText;
        if (link1 != "")
        {
            pdfButton1.gameObject.SetActive(true);
            if (link2 != "")
            {
                pdfButton2.gameObject.SetActive(true);
            }
        }

        yield break;
    }
#endregion

}
