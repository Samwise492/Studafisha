using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sign_Up : MonoBehaviour
{
    [SerializeField] Button signInButton;
    [SerializeField] Button continueButton;
    [SerializeField] Text login, password, password_verification;
    [SerializeField] Text firstName, lastName, headquarter, squad, position;
    [SerializeField] Toggle personalData, rules;
    [SerializeField] Image scrollViewHq, scrollViewSquad, scrollViewPosition;

    public void SignIn()
    {
        // SQL Code

        SceneManager.LoadSceneAsync("Start Login", LoadSceneMode.Single);
    }
    public void Continue_Reg1()
    {
        // SQL Code

        SceneManager.LoadSceneAsync("Start Reg 2", LoadSceneMode.Single);
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

    // more buttons
    void OpenMore(Image content)
    {
        if (content.gameObject.activeSelf == true)
            content.gameObject.SetActive(false);
        else content.gameObject.SetActive(true);
    }
    public void OpenMoreHq() => OpenMore(scrollViewHq);
    public void OpenMorePosition() => OpenMore(scrollViewPosition);
    public void OpenMoreSquad() => OpenMore(scrollViewSquad);
}
