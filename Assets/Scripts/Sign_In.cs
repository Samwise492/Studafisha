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
    public Text login;
    public InputField password;
    [SerializeField] Text errorText;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    string errorMessage = "";

#region Buttons
    public void EnterToSignUp() => SceneManager.LoadSceneAsync("Start Reg 1", LoadSceneMode.Single);
    public void Disclaimer() => SceneManager.LoadSceneAsync("Poster", LoadSceneMode.Single);
    public void Continue() => StartCoroutine(LoginEnumerator());
    public void NotFromRSO()
    {
        UserData.Instance.login = "/Guest";
        SceneManager.LoadSceneAsync("Disclaimer", LoadSceneMode.Single);
    }
    public void ForgotPassword()
    {

    }
#endregion

#region SQL
    void ResetValues()
    {
        errorMessage = "";
        login.text = "";
        password.text = "";
    }
    IEnumerator LoginEnumerator()
    {
        WWWForm form = new WWWForm();
        form.AddField("login", login.text);
        form.AddField("password", password.text);
        byte[] rawData = form.data;
        Debug.Log(System.Text.Encoding.GetEncoding(1251).GetString(rawData)); // get text from form

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
                    SceneManager.LoadSceneAsync("Disclaimer", LoadSceneMode.Single);
                    string[] dataChunks = responseText.Split('|');
                    login.text = dataChunks[1];
                    UserData.Instance.login = login.text;

                    ResetValues();
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
        if (errorMessage != "")
        {
            Debug.Log("Can't log in");
            errorText.text = errorMessage;
        }
        errorMessage = "";
        yield break;
    }
#endregion
}
