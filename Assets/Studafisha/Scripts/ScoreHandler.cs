using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] Text participantScoreText;
    [SerializeField] Text volunteerScoreText;
    [SerializeField] Text organiserScoreText;
    [SerializeField] Text totalScoreText;
    int participantScore, volunteerScore, organiserScore, totalScore;
    int participantMultiplier = 1;
    int volunteerMultiplier = 2;
    int organiserMultiplier = 3;
    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located
    public Dictionary<int, int> scoresByMonths = new Dictionary<int, int>(); // month, value

    void Start()
    {
        if (UserData.Instance != null && UserData.Instance.login != "/Guest")
        {
            StartCoroutine(ScoreQuery());
            StartCoroutine(DateQuery());
        }
    }
    void InitialiseScoresByMonths(string[] months)
    {
        int[] _count = new int[13];

        foreach (var value in months)
        {
            switch (value)
            {
                case "01":
                    _count[1]++;
                    break;
                case "02":
                    _count[2]++;
                    break;
                case "03":
                    _count[3]++;
                    break;
                case "04":
                    _count[4]++;
                    break;
                case "05":
                    _count[5]++;
                    break;
                case "06":
                    _count[6]++;
                    break;
                case "07":
                    _count[7]++;
                    break;
                case "08":
                    _count[8]++;
                    break;
                case "09":
                    _count[9]++;
                    break;
                case "10":
                    _count[10]++;
                    break;
                case "11":
                    _count[11]++;
                    break;
                case "12":
                    _count[12]++;
                    break;
            }
        }

        for (int i = 0; i < _count.Length; i++)
        {
            scoresByMonths.Add(i, _count[i]);
        }
    }

    IEnumerator ScoreQuery()
    {
        WWWForm form = new WWWForm();
        form.AddField("User_Id", UserData.Instance.userId);
        form.AddField("P_Multiplier", participantMultiplier);
        form.AddField("V_Multiplier", volunteerMultiplier);
        form.AddField("O_Multiplier", organiserMultiplier);
        
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_userScore.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
   
            if (responseText.Split('|')[0] == "hasBeenInitialised")
            {
                participantScore = Int32.Parse(responseText.Split('|')[1].Split(' ')[0]);
                volunteerScore = Int32.Parse(responseText.Split('|')[1].Split(' ')[1]);
                organiserScore = Int32.Parse(responseText.Split('|')[1].Split(' ')[2]);
                totalScore = Int32.Parse(responseText.Split('|')[1].Split(' ')[3]);
            }
            
            else
            {
                participantScore = Int32.Parse(responseText.Split('|')[0]);
                volunteerScore = Int32.Parse(responseText.Split('|')[1]);
                organiserScore = Int32.Parse(responseText.Split('|')[2]);
                totalScore = participantScore + volunteerScore + organiserScore;
            }
        }

        participantScoreText.text = participantScore.ToString();
        volunteerScoreText.text = volunteerScore.ToString();
        organiserScoreText.text = organiserScore.ToString();
        totalScoreText.text = totalScore.ToString();

        switch (totalScore)
        {
            case 1:
                totalScoreText.transform.parent.GetChild(0).GetComponent<Text>().text = "Балл";
                break;
            case 2:
                totalScoreText.transform.parent.GetChild(0).GetComponent<Text>().text = "Балла";
                break;
            case 3:
                totalScoreText.transform.parent.GetChild(0).GetComponent<Text>().text = "Балла";
                break;
            case 4:
                totalScoreText.transform.parent.GetChild(0).GetComponent<Text>().text = "Балла";
                break;
            default:
                totalScoreText.transform.parent.GetChild(0).GetComponent<Text>().text = "Баллов";
                break;
        }

        yield break;
    }
    IEnumerator DateQuery()
    {
        WWWForm form = new WWWForm();
        form.AddField("User_Id", UserData.Instance.userId);
        form.AddField("Get_Dates", "");
        
        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_userScore.php", form))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;
            
            string[] dates = responseText.Split('|');
            string[] months = new string[dates.Length];
            for (int i = 0; i < dates.Length; i++)
            {
                if (dates[i] != "")
                    months[i] = dates[i].Split('-')[1];
            }

            InitialiseScoresByMonths(months);
        }

        yield break;
    }
}
