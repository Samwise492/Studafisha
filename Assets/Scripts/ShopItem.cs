using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] int price;
    [SerializeField] int scoresForDiscount;
    [SerializeField] string url;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }
}
