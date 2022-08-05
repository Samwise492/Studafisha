using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SquadInitialisation : MonoBehaviour
{
    public static SquadInitialisation Instance { get; set; }
    public string _name;
    public string type;
    public string hq;
    public string vk;
    public string mail;
    public string commander, commanderVk;
    public string commissioner, commissionerVk;
    public string press, pressVk;
    public string master, masterVk;
    public string address;
    public string history;
    [SerializeField] GameObject staffShell;
    [SerializeField] GameObject staffNest;
    [SerializeField] GameObject squadNest;

    string rootURL = "http://database.com.masterhost.tech/"; //Path where php files are located

    void Awake()
    {
        Instance = this;
    }
    void CreateStaffShells()
    {
         foreach (Transform child in staffNest.transform) 
         {
            GameObject.Destroy(child.gameObject);
        }
        if (commander != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = commander;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = hq;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(commanderVk));
        }
        if (commissioner != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = commissioner;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = hq;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(commissionerVk));
        }
        if (press != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = press;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = hq;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(pressVk));
        }
        if (master != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = master;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = hq;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(masterVk));
        }
    }
    IEnumerator RefreshContent(GameObject content, bool _true)
    {
        content.SetActive(_true);
        yield return new WaitForEndOfFrame();
        content.SetActive(!_true);
        yield return new WaitForEndOfFrame();
        content.SetActive(_true);
        yield break;
    }
    public IEnumerator InitialiseSquadQuery()
    {       
        WWWForm squadForm = new WWWForm();
        squadForm.AddField("Type", type);
        squadForm.AddField("Name", _name);
        squadForm.AddField("Uni", hq);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_squadInfo.php", squadForm))
        {
            yield return www.SendWebRequest();
            string responseText = www.downloadHandler.text;

            switch (responseText.Split('|').Length)
            {
                case 1:
                    vk = responseText.Split('|')[0];
                    break;
                case 2:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    break;
                case 3:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    break;
                case 4:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    break;
                case 5:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    break;
                case 6:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    break;
                case 7:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    pressVk = responseText.Split('|')[6];
                    break;
                case 8:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    pressVk = responseText.Split('|')[6];
                    master = responseText.Split('|')[7];
                    break;
                case 9:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    pressVk = responseText.Split('|')[6];
                    master = responseText.Split('|')[7];
                    masterVk = responseText.Split('|')[8];
                    break;
                case 10:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    pressVk = responseText.Split('|')[6];
                    master = responseText.Split('|')[7];
                    masterVk = responseText.Split('|')[8];
                    history = responseText.Split('|')[9];
                    break;
                case 11:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    pressVk = responseText.Split('|')[6];
                    master = responseText.Split('|')[7];
                    masterVk = responseText.Split('|')[8];
                    history = responseText.Split('|')[9];
                    mail = responseText.Split('|')[10];
                    break;
                case 12:
                    vk = responseText.Split('|')[0];
                    commander = responseText.Split('|')[1];
                    commanderVk = responseText.Split('|')[2];
                    commissioner = responseText.Split('|')[3];
                    commissionerVk = responseText.Split('|')[4];
                    press = responseText.Split('|')[5];
                    pressVk = responseText.Split('|')[6];
                    master = responseText.Split('|')[7];
                    masterVk = responseText.Split('|')[8];
                    history = responseText.Split('|')[9];
                    mail = responseText.Split('|')[10];
                    address = responseText.Split('|')[11];
                    break;
                case 0:
                    Debug.Log("No data");
                    break;
            }

            squadNest.transform.GetChild(1).GetComponent<Text>().text = type + " " + _name;
            squadNest.transform.GetChild(2).GetComponent<Text>().text = hq;
            squadNest.transform.GetChild(3).GetChild(5).GetChild(0).GetChild(1).GetComponent<Text>().text = hq;
            squadNest.transform.GetChild(3).GetChild(6).GetChild(0).GetComponent<Text>().text = address;
            CreateStaffShells();
            StartCoroutine(RefreshContent(staffNest, true));
        }

        yield break;
    }
}
