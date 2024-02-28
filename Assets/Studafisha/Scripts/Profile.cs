using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Profile : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text hq;
    [SerializeField] Text squad;
    [SerializeField] Text position;

    void Start()
    {
        _name.text = UserData.Instance.firstName + " " + UserData.Instance.lastName;
        hq.text = UserData.Instance.hq;
        squad.text = UserData.Instance.squad;
        position.text = UserData.Instance.position;
    }

    public void OnClickBack() => SceneManager.LoadSceneAsync("Poster");
    public void FooterSwitch()
    {
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            case "Library":
                SceneManager.LoadSceneAsync("Library", LoadSceneMode.Single);
                break;
            case "Poster":
                SceneManager.LoadSceneAsync("Poster", LoadSceneMode.Single);
                break;
            case "Squads":
                SceneManager.LoadSceneAsync("Squads", LoadSceneMode.Single);
                break;
            case "Shop":
                SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Single);
                break;
        }   
    }
}
