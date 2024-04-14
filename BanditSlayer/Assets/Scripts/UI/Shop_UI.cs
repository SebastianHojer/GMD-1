using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Common;
using Interactables;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    private Transform _container;
    private Transform _shopItemTemplate;
    private ICustomer _customer;
    private bool _shown;

    private void Awake()
    {
        _container = transform.Find("container");
        _shopItemTemplate = _container.Find("shopItemTemplate");
        _shopItemTemplate.gameObject.SetActive(false);
        _shown = false;
    }

    private void Start()
    {
        CreateItemButton(Item.ItemType.Dagger,Item.GetSprite(Item.ItemType.Dagger), "Dagger", Item.GetCost(Item.ItemType.Dagger), 0);
        CreateItemButton(Item.ItemType.Axe, Item.GetSprite(Item.ItemType.Axe), "Axe", Item.GetCost(Item.ItemType.Axe), 1);
        CreateItemButton(Item.ItemType.Spear, Item.GetSprite(Item.ItemType.Spear), "Spear", Item.GetCost(Item.ItemType.Spear), 2);
        CreateItemButton(Item.ItemType.Sword, Item.GetSprite(Item.ItemType.Sword), "Sword", Item.GetCost(Item.ItemType.Sword), 3);
        CreateItemButton(Item.ItemType.Katana, Item.GetSprite(Item.ItemType.Katana), "Katana", Item.GetCost(Item.ItemType.Katana), 4);
        CreateItemButton(Item.ItemType.Polearm, Item.GetSprite(Item.ItemType.Polearm), "Polearm", Item.GetCost(Item.ItemType.Polearm), 5);
        CreateItemButton(Item.ItemType.Mace, Item.GetSprite(Item.ItemType.Mace), "Mace", Item.GetCost(Item.ItemType.Mace), 6);
        
        Hide();
    }

    private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(_shopItemTemplate, _container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
        float shopItemHeight = 70f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        
        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
        shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

        shopItemTransform.GetComponent<Button>().onClick.AddListener(() => TryBuyItem(itemType));
    }

    private void TryBuyItem(Item.ItemType itemType)
    {
        _customer.BuyItem(itemType);
    }

    public void Show(ICustomer customer)
    {
        _customer = customer;
        if (!_shown)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        _shown = true;
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
        _shown = false;
    }
}