using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private Transform container;

    private Transform shopItemTemplate;

    [SerializeField] private float shopItemHeight = 30f;
    [SerializeField] private PlayerController player;

    private void Awake()
    {
        container = transform.Find("Container");
        shopItemTemplate = container.Find("ShopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        CreateItemButton(ShopItem.ItemType.Jump, "Extra Jump", ShopItem.GetCost(ShopItem.ItemType.Jump), 0);
        CreateItemButton(ShopItem.ItemType.WallGrab, "Wall Grab", ShopItem.GetCost(ShopItem.ItemType.WallGrab), 1);

        Hide();
    }

    private void CreateItemButton(ShopItem.ItemType type, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("itemName").GetComponent<Text>().text = itemName;
        shopItemTransform.Find("itemCost").GetComponent<Text>().text = itemCost + "";

        shopItemTransform.Find("Panel").GetComponent<Button>().onClick.AddListener(delegate { BuyItem(type); });
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void BuyItem(ShopItem.ItemType type)
    {
        player.BuyItem(type);
    }
}
