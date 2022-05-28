using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Sign_In : MonoBehaviour
{
    [SerializeField] Button enterSignUpMenuButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button amNotFromRSOButton;
    public Text login, password;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    string errorMessage = "";

    // Buttons
    public void EnterToSignUp() => SceneManager.LoadSceneAsync("Start Reg 1", LoadSceneMode.Single);
    public void Disclaimer() => SceneManager.LoadSceneAsync("Poster", LoadSceneMode.Single);
    public void Continue()
    {
        StartCoroutine(LoginEnumerator());

        //SceneManager.LoadSceneAsync("Disclaimer", LoadSceneMode.Single);
    }
    public void NotFromRSO()
    {
        SceneManager.LoadSceneAsync("Disclaimer", LoadSceneMode.Single);
    }
    public void ForgotPassword()
    {

    }

    // MySQL
    void ResetValues()
    {
        errorMessage = "";
        login.text = "";
        password.text = "";
    }
    IEnumerator LoginEnumerator()
    {
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("login", login.text);
        form.AddField("password", password.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success"))
                {
                    string[] dataChunks = responseText.Split('|');
                    login.text = dataChunks[1];

                    ResetValues();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
    }
}
