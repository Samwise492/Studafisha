using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserData : MonoBehaviour
{
    public static UserData Instance { get; set; }
    public string login, lastName, firstName, position, hq, squad;
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
                Debug.Log("got ya");
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

                    lastName = responseText.Split(';')[0];
                    firstName = responseText.Split(';')[1];
                    position = responseText.Split(';')[2];
                    squad = responseText.Split(';')[3].Split('|')[0] + " " + responseText.Split(';')[3].Split('|')[1];
                    hq = responseText.Split(';')[4];
                }

                yield break;
            }
        }
    }
}