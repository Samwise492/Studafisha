using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Text;
public class Sign_Up : MonoBehaviour
{
    [SerializeField] RectTransform regMenu1, regMenu2, regMenu3, regMenu4;
    [SerializeField] RectTransform loginMenu;
    [SerializeField] Text login;
    [SerializeField] InputField password, password_verification;
    [SerializeField] Text firstName, lastName; 
    public Text headquarter, squad, position;
    [SerializeField] Text phone;
    [SerializeField] Toggle personalData, rules;
    [SerializeField] Text errorText;
    public Image hqPopUp, squadPopUp, positionPopUp;
    int squadId; // change to int
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    string errorMessage = "";
    RectTransform[] menus = new RectTransform[5];
    WWWForm form;

    void Awake()
    {
        //StartCoroutine(Test());
        form = new WWWForm();
        menus[0] = loginMenu;
        menus[1] = regMenu1;
        menus[2] = regMenu2;
        menus[3] = regMenu3;
        menus[4] = regMenu4;
    }

#region Buttons
    void TurnOnMenu(RectTransform menu, Action form, IEnumerator coroutine)
    {
        if (form != null)
            form();
        if (coroutine != null)
            StartCoroutine(coroutine);
        if (menu != null)
        {
            foreach (var _menu in menus)
            {
                _menu.gameObject.SetActive(false);
            }
            menu.gameObject.SetActive(true);
        }
    }
    public void EnterToSignIn() => TurnOnMenu(loginMenu, null, null);
    public void OpenReg1() => TurnOnMenu(regMenu1, null, null);
    public void OpenReg2() => TurnOnMenu(regMenu2, FormAssession_Reg1, null);
    public void OpenReg3() => TurnOnMenu(regMenu3, FormAssession_Reg2, null);
    public void OpenReg4() => TurnOnMenu(null, FormAssession_Reg3, RegisterEnumerator());
#endregion

#region Drop-down windows
    void OpenMore(Image content, IEnumerator coroutine)
    {
        if (content.gameObject.activeSelf == true)
            content.gameObject.SetActive(false);
        else
        {
            content.gameObject.SetActive(true);
            StartCoroutine(coroutine);
        }
    } 
    public void OpenMoreHq() => OpenMore(hqPopUp, SQLForm.Instance.HeadquartersQuery(hqPopUp.gameObject, headquarter));
    public void OpenMoreSquad() => OpenMore(squadPopUp, SQLForm.Instance.SquadsQuery(headquarter.text, squadPopUp.gameObject, squad));
    public void OpenMorePosition() => OpenMore(positionPopUp, SQLForm.Instance.PositionsQuery(position.text, positionPopUp.gameObject, position));
#endregion
    
#region MySQL
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
    void FormAssession_Reg1()
    {
        form.AddField("Login", login.text);
        form.AddField("password1", password.text);
        form.AddField("password2", password_verification.text);
    }
    void FormAssession_Reg2()
    {
        StartCoroutine(GetSquadId());
        form.AddField("LastName", lastName.text);
        form.AddField("Name", firstName.text);
        form.AddField("Position", position.text);
    }
    void FormAssession_Reg3()
    {
        form.AddField("Phone", "8" + phone.text);
        if (!personalData.isOn)
        {
            errorMessage = "Подтвердите согласие на обработку данных";
        }
        if (!rules.isOn)
        {
            errorMessage = "Подтвердите правила сервиса";
        }
        if (rules.isOn && personalData.isOn) 
        {
            errorMessage = "";   
        }
    }
    IEnumerator GetSquadId()
    {
        WWWForm idForm = new WWWForm();
        if (squad.text != "")
        {
            string[] squadName = squad.text.Split(' ');
            idForm.AddField("Squad", squadName[1]);
        }

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squads_id.php", idForm))
        {
            yield return www.SendWebRequest();
            squadId = Int32.Parse(www.downloadHandler.text);
            form.AddField("Squad_Id", squadId);
        }
        yield break;
    }
    IEnumerator RegisterEnumerator()
    {
        byte[] rawData = form.data;
        Debug.Log(System.Text.Encoding.GetEncoding(1251).GetString(rawData)); // get text from form

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                errorMessage = www.error;
            }
            else if (errorMessage == "")
            {
                string responseText = www.downloadHandler.text;

                if (responseText.StartsWith("Success")) // user's created
                {
                    Debug.Log("User's created");
                    ResetValues();
                    foreach (var _menu in menus)
                    {
                        _menu.gameObject.SetActive(false);
                    }
                    regMenu4.gameObject.SetActive(true); // open login scene
                    errorText.text = "";
                }
                else
                {
                    errorMessage = responseText;
                }
            }
        }
        if (errorMessage != "")
        {
            Debug.Log("User's not created");
            errorText.text = errorMessage;
        }
        errorMessage = "";
        yield break;
    }
#endregion

#region Debug code
    public void AutoFill()
    {
        form.AddField("Login", "Reglog2111212");
        form.AddField("password1", "Debian234");
        form.AddField("password2", "Debian234");
        form.AddField("Squad_Id", 11000008);
        form.AddField("LastName", "Денисов");
        form.AddField("Name", "Алех");
        form.AddField("Position", "Мастер");
        form.AddField("Phone", "89191111441");
        StartCoroutine(RegisterEnumerator());
    }
#endregion
}
