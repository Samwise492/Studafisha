using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Sign_Up : MonoBehaviour
{
    [SerializeField] Button enterSignInMenuButton;
    [SerializeField] Button continueButton;
    [SerializeField] Text login, password, password_verification;
    [SerializeField] Text firstName, lastName, headquarter, squad, position;
    [SerializeField] Toggle personalData, rules;
    [SerializeField] Image scrollViewHq, scrollViewSquad, scrollViewPosition;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    string errorMessage = "";

    // Buttons
    public void EnterToSignIn() => SceneManager.LoadSceneAsync("Start Login", LoadSceneMode.Single);

    public void Continue_Reg1()
    {
        StartCoroutine(RegisterEnumerator());

        //SceneManager.LoadSceneAsync("Start Reg 2", LoadSceneMode.Single);
    }
    public void Continue_Reg2()
    {
        // SQL Code

        SceneManager.LoadSceneAsync("Start Reg 3", LoadSceneMode.Single);
    }
    public void Continue_Reg3()
    {
        // SQL Code

        if (personalData.isOn && rules.isOn)
            SceneManager.LoadSceneAsync("Start Reg 4", LoadSceneMode.Single);
    }
    public void Continue_Reg4()
    {
        SceneManager.LoadSceneAsync("Start Login", LoadSceneMode.Single);
    }

    // Drop-down windows
    void OpenMore(Image content)
    {
        if (content.gameObject.activeSelf == true)
            content.gameObject.SetActive(false);
        else content.gameObject.SetActive(true);
    } 
    public void OpenMoreHq() => OpenMore(scrollViewHq);
    public void OpenMorePosition() => OpenMore(scrollViewPosition);
    public void OpenMoreSquad() => OpenMore(scrollViewSquad);

    // MySQL
    void ResetValues()
    {
        errorMessage = "";
        login.text = "";
        password.text = "";
        password_verification.text = "";
        firstName.text = ""; 
        lastName.text = ""; 
        headquarter.text = ""; 
        squad.text = ""; 
        position.text = "";
    }
    IEnumerator RegisterEnumerator()
    {
        errorMessage = "";

        WWWForm form = new WWWForm();
        form.AddField("Login", login.text);
        form.AddField("password1", password.text);
        form.AddField("password2", password_verification.text);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success")) // user's created
                {
                    ResetValues();
                    Debug.Log("Gotcha!"); // open login scene
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
    }
}
