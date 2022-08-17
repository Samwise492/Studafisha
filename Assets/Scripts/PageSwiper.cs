using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PageSwiper : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    float startPos, endPos;
    [SerializeField] SideMenu sideMenu;

    public void OnDrag(PointerEventData eventData) { }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 0)
        {
            startPos = Input.GetTouch(0).position.x;
        }
        #if UNITY_EDITOR
        startPos = Input.mousePosition.x;
        #endif
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.touchCount > 0)
        {
            endPos = Input.GetTouch(0).position.x;
        }
        #if UNITY_EDITOR
        endPos = Input.mousePosition.x;
        #endif

        if (this.transform.GetChild(0).gameObject.activeSelf && !sideMenu.gameObject.activeSelf) // if main scroll view is active
        {
            if (SceneManager.GetActiveScene().name == "All")
            {
                if (endPos > startPos)
                    SceneManager.LoadSceneAsync("Headquarters", LoadSceneMode.Single);
                else if (endPos < startPos)
                    SceneManager.LoadSceneAsync("Squads", LoadSceneMode.Single);
            }
            else if (SceneManager.GetActiveScene().name == "Squads")
            {
                if (endPos > startPos)
                    SceneManager.LoadSceneAsync("All", LoadSceneMode.Single);
            }
            else if (SceneManager.GetActiveScene().name == "Headquarters")
            {
                if (endPos < startPos)
                    SceneManager.LoadSceneAsync("All", LoadSceneMode.Single);
            }
        }
    }
}
