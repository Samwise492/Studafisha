using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public void OnDrag(PointerEventData data)
    {
        
    }
    public void OnEndDrag(PointerEventData data)
    {
        if (this.transform.GetChild(0).gameObject.activeSelf)
            {
            if (SceneManager.GetActiveScene().name == "Headquarters")
            {
                SceneManager.LoadSceneAsync("Squads", LoadSceneMode.Single);
            }
            else if (SceneManager.GetActiveScene().name == "Squads")
            {
                SceneManager.LoadSceneAsync("Headquarters", LoadSceneMode.Single);
            }
        }
    }
}
