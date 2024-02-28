using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int id;
    public int price;
    public int discountedPrice;
    public int scoresForDiscount;
    public string link;
    [SerializeField] Text priceText, discountedPriceText;

    void Start()
    {
        priceText.text = price.ToString();
        discountedPriceText.text = discountedPrice.ToString();

        this.GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(link));
    }
}
