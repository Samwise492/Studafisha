using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] SideMenu sideMenu;
    [SerializeField] GameObject itemNest;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    List<int> ids = new List<int>();
    List<string> links = new List<string>();
    List<int> prices = new List<int>();
    List<int> scoresForDiscount = new List<int>();
    List<int> discountedPrices = new List<int>();

    void Start()
    {
        StartCoroutine(ShopQuery());
    }

    public void OnClickAvatar() => sideMenu.gameObject.SetActive(true);
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

    IEnumerator ShopQuery()
    {
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_shopList.php", ""))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;

            ids.Add(Int32.Parse(responseText.Split('|')[0]));
            links.Add(responseText.Split('|')[1]);
            prices.Add(Int32.Parse(responseText.Split('|')[2]));
            discountedPrices.Add(Int32.Parse(responseText.Split('|')[3]));
        }

        yield break;
    }
}
