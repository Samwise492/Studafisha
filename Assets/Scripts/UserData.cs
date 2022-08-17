using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; set; }
    public string login, lastName, firstName, position, hq, squad;
    public int userId;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located

    void Awake()
    {
        DontDestroyOnLoad(this);
        Instance = this;
        StartCoroutine(InitialiseUser());
    }

    IEnumerator InitialiseUser()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            if (login == "/Guest")
            {
                lastName = "";
                firstName = "Гость";
                position = "–";
                squad = "–";
                hq = "–";

                yield break;
            }
            else if (login != "")
            {
                WWWForm form = new WWWForm();
                form.AddField("Login", login);

                using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_userData.php", form))
                {
                    yield return www.SendWebRequest();
                    string responseText = www.downloadHandler.text;

                    userId = Int32.Parse(responseText.Split(';')[0]);
                    lastName = responseText.Split(';')[1];
                    firstName = responseText.Split(';')[2];
                    position = responseText.Split(';')[3];
                    squad = responseText.Split(';')[4].Split('|')[0] + " " + responseText.Split(';')[4].Split('|')[1];
                    hq = responseText.Split(';')[5];
                }

                yield break;
            }
        }
    }
}
