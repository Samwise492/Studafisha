using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class HeadquarterInitialisation : MonoBehaviour
{
    public static HeadquarterInitialisation Instance { get; set; }
    public string _name, vk, mail, address, history;
    public string commander1, commander1Vk;
    public string commander2, commander2Vk;
    public string commissioner, commissionerVk;
    public string press, pressVk;
    public string master1, master1Vk;
    public string master2, master2Vk;
    public string master3, master3Vk;
    [SerializeField] GameObject staffShell;
    [SerializeField] GameObject staffNest;
    [SerializeField] GameObject hqNest;

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
        if (commander1 != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = commander1;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(commander1Vk));
        }
        if (commander2 != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = commander2;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(commander2Vk));
        }
        if (commissioner != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = commissioner;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(commissionerVk));
        }
        if (press != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = press;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(pressVk));
        }
        if (master1 != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = master1;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(master1Vk));
        }
        if (master2 != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = master2;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(master2Vk));
        }
        if (master3 != "")
        {
            var staffMember = Instantiate(staffShell, staffNest.transform);
            staffMember.transform.GetChild(1).GetComponent<Text>().text = master3;
            staffMember.transform.GetChild(2).GetComponent<Text>().text = _name;
            staffMember.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => 
            Application.OpenURL(master3Vk));
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
    public IEnumerator InitialiseHeadquarterQuery()
    {       
        WWWForm headquarterForm = new WWWForm();
        headquarterForm.AddField("Name", _name);

        using (UnityWebRequest www = UnityWebRequest.Post(rootURL + "get_headquarterInfo.php", headquarterForm))
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
                    mail = responseText.Split('|')[1];
                    break;
                case 3:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    break;
                case 4:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    break;
                case 5:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    break;
                case 6:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    break;
                case 7:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    break;
                case 8:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    break;
                case 9:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    break;
                case 10:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    break;
                case 11:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    break;
                case 12:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    break;
                case 13:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    master1Vk = responseText.Split('|')[12];
                    break;
                case 14:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    master1Vk = responseText.Split('|')[12];
                    master2 = responseText.Split('|')[13];
                    break;
                case 15:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    master1Vk = responseText.Split('|')[12];
                    master2 = responseText.Split('|')[13];
                    master2Vk = responseText.Split('|')[14];
                    break;
                case 16:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    master1Vk = responseText.Split('|')[12];
                    master2 = responseText.Split('|')[13];
                    master2Vk = responseText.Split('|')[14];
                    master3 = responseText.Split('|')[15];
                    break;
                case 17:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    master1Vk = responseText.Split('|')[12];
                    master2 = responseText.Split('|')[13];
                    master2Vk = responseText.Split('|')[14];
                    master3 = responseText.Split('|')[15];
                    master3Vk = responseText.Split('|')[16];
                    break;
                case 18:
                    vk = responseText.Split('|')[0];
                    mail = responseText.Split('|')[1];
                    address = responseText.Split('|')[2];
                    commander1 = responseText.Split('|')[3];
                    commander1Vk = responseText.Split('|')[4];
                    commander2 = responseText.Split('|')[5];
                    commander2Vk = responseText.Split('|')[6];
                    commissioner = responseText.Split('|')[7];
                    commissionerVk = responseText.Split('|')[8];
                    press = responseText.Split('|')[9];
                    pressVk = responseText.Split('|')[10];
                    master1 = responseText.Split('|')[11];
                    master1Vk = responseText.Split('|')[12];
                    master2 = responseText.Split('|')[13];
                    master2Vk = responseText.Split('|')[14];
                    master3 = responseText.Split('|')[15];
                    master3Vk = responseText.Split('|')[16];
                    history = responseText.Split('|')[17];
                    break;
                case 0:
                    Debug.Log("No data");
                    break;
            }

            hqNest.transform.GetChild(1).GetComponent<Text>().text = "Штаб " + _name;
            hqNest.transform.GetChild(2).GetChild(5).GetChild(0).GetComponent<Text>().text = address;
            CreateStaffShells();
            StartCoroutine(RefreshContent(staffNest, true));
        }

        yield break;
    }
}
