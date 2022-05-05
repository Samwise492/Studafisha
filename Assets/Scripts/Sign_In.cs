using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sign_In : MonoBehaviour
{
    [SerializeField] Button signUpButton;
    [SerializeField] Button continueButton;
    [SerializeField] Button amNotFromRSOButton;
    public Text login, password;

    public void SignUp()
    {
        // SQL Code

        SceneManager.LoadSceneAsync("Start Reg 1", LoadSceneMode.Single);
    }
    public void Continue()
    {
        // SQL Code

        SceneManager.LoadSceneAsync("Disclaimer", LoadSceneMode.Single);
    }
    public void AmNotFromRSO()
    {
        SceneManager.LoadSceneAsync("Disclaimer", LoadSceneMode.Single);
    }
    public void Disclaimer() => SceneManager.LoadSceneAsync("Poster", LoadSceneMode.Single);
    public void ForgotPassword()
    {

    }
}
