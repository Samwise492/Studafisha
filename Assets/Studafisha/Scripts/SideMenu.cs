using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SideMenu : MonoBehaviour
{
    [SerializeField] Text userInfoField;
    [SerializeField] Text nameField;
    string _name, squad, position;
    CanvasGroup scrollViewMain;
    Canvas canvas;
    Camera squadCamera;
    Squads squadscrollViewMain;

    void Awake()
    {
        scrollViewMain = GameObject.FindObjectOfType<CanvasGroup>();
        canvas = GameObject.FindObjectOfType<Canvas>();

        if (UserData.Instance.login != "/Guest")
        {
            _name = UserData.Instance.firstName + " " + UserData.Instance.lastName;
            squad = UserData.Instance.squad;
            position = UserData.Instance.position;

            nameField.text = _name;
            userInfoField.text = position + ": " + squad;
        }
        else
        {
            _name = "Гость";

            nameField.text = _name;
            userInfoField.text = "";
        }
    }
    void OnEnable()
    {
        scrollViewMain.blocksRaycasts = false;
    }
    void OnDisable()
    {
        scrollViewMain.blocksRaycasts = true;
    }

    public void MyProfile()
    {
        SceneManager.LoadSceneAsync("Profile", LoadSceneMode.Single);
        this.gameObject.SetActive(false);
    }

    public void MySquad()
    { 
        if (UserData.Instance.login != "/Guest")
            StartCoroutine(LoadSquad());
    }
    
    public void SquadSceneHandler(bool _switch) // _switch(true) = turn on; _switch(off) = turn off
    {
        if (_switch == true)
        {
            squadscrollViewMain.gameObject.SetActive(true);
            squadCamera.gameObject.SetActive(true);
        }
        else
        {
            squadscrollViewMain.gameObject.SetActive(false);
            squadCamera.gameObject.SetActive(false);
        }
    }
    IEnumerator LoadSquad()
    {
        SceneManager.LoadSceneAsync("Squads", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.9f);

        squadscrollViewMain = GameObject.FindObjectOfType<Squads>();
        squadCamera = GameObject.FindObjectOfType<Camera>();

        SquadSceneHandler(true);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Squads"));

        // switch scroll views
        squadscrollViewMain.transform.GetChild(0).gameObject.SetActive(false);
        squadscrollViewMain.transform.GetChild(1).gameObject.SetActive(true);

        // switch headers
        squadscrollViewMain.transform.GetChild(2).gameObject.SetActive(false);
        squadscrollViewMain.transform.GetChild(3).gameObject.SetActive(true);

        SquadInitialisation.Instance._name = squad;
        StartCoroutine(SquadInitialisation.Instance.InitialiseSquadQuery());

        canvas.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        
        yield break;
    }

    public void Groups()
    {

    }
    public void Successes()
    {

    }
    public void Search()
    {

    }
    public void Settings()
    {

    }
    
    public void WhatsNew()
    {

    }
    public void Developers()
    {

    }
    public void Help()
    {

    }

    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider == null)
            {
                this.gameObject.SetActive(false);
            }
        }
        #endif
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (hit.collider == null)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}
