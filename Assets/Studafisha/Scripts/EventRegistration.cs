using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EventRegistration : MonoBehaviour
{
    [SerializeField] Text passportSeries, passportNumber, giver;
    [SerializeField] GameObject signUpContent;
    [SerializeField] GameObject openCovidContentButton;
    [SerializeField] Text errorField;
    string rootURL = "http://database.com.masterhost.tech/"; // Path where php files are located
    string errorMessage;
    bool isVaccined;
    Color defaultColor;
    
    void Start() => defaultColor = errorField.color;
#region Buttons
    public void OnClickSignIn() => StartCoroutine(AutoSignUpEnumerator());
    public void OnClickOpenCovidContent()
    {
        GameObject covidContent = signUpContent.transform.GetChild(4).gameObject;

        if (covidContent.activeSelf == true)
        {
            covidContent.SetActive(false);
        }
        else
        {
            covidContent.SetActive(true);
        }
    }
    public void OnClickVerifyCovid()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text == "Есть")
            isVaccined = true;
        else if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text == "Нет") 
            isVaccined = false;
        signUpContent.transform.GetChild(4).gameObject.SetActive(false);
        openCovidContentButton.transform.GetChild(0).GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text;
    }
#endregion

    IEnumerator AutoSignUpEnumerator()
    {
        WWWForm form = new WWWForm();
        form.AddField("EventId", Poster.Instance.eventId);
        form.AddField("IsVolunteer", Poster.Instance.isVolunteer.ToString());
        form.AddField("UserId", UserData.Instance.userId);
        form.AddField("PassportSeries", passportSeries.text);
        form.AddField("PassportNumber", passportNumber.text);
        form.AddField("Giver", giver.text);
        if (openCovidContentButton.transform.GetChild(0).GetComponent<Text>().text != "")
            form.AddField("CovidVerification", isVaccined.ToString());
        byte[] rawData = form.data;
        Debug.Log(System.Text.Encoding.GetEncoding(1251).GetString(rawData)); // get text from form

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "signUp_participant.php", form))
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
                    ResetValues();
                    StartCoroutine(RegistartionVerification());
                }
                else
                {
                    errorMessage = responseText;
                }
            }    
        }
        if (errorMessage != "")
        {
            errorField.text = errorMessage;
        }
        errorMessage = "";
        yield break;
    }
    void ResetValues()
    {
        errorMessage = "";
        passportSeries.text = "";
        passportNumber.text = "";
        giver.text = "";
    }
    IEnumerator RegistartionVerification()
    {
        errorField.color = Color.green;
        errorField.text = "Зарегистрирован!";
        yield return new WaitForSeconds(2);

        SceneManager.LoadSceneAsync("Poster", LoadSceneMode.Single);
        openCovidContentButton.transform.GetChild(0).GetComponent<Text>().text = "";
        yield break;
    }
}
