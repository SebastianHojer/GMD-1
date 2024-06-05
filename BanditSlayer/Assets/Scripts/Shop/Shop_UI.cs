using System;
using Common;
using GameLogic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class Shop_UI : MonoBehaviour
    {
        private Transform _container;
        private Transform _shopItemTemplate;
        private ICustomer _customer;
        private bool _shown;
        private string _messageToShow;
        private Button _firstButton;

        private void Awake()
        {
            _container = transform.Find("container");
            _shopItemTemplate = _container.Find("shopItemTemplate");
            _shopItemTemplate.gameObject.SetActive(false);
            _shown = false;
        }

        private void Start()
        {
            CreateItemButton(Item.ItemType.Dagger,Item.GetSprite(Item.ItemType.Dagger), "Dagger", Item.GetCost(Item.ItemType.Dagger), 0, () => TryBuyItem(Item.ItemType.Dagger));
            CreateItemButton(Item.ItemType.Axe, Item.GetSprite(Item.ItemType.Axe), "Axe", Item.GetCost(Item.ItemType.Axe), 1, () => TryBuyItem(Item.ItemType.Axe));
            CreateItemButton(Item.ItemType.Spear, Item.GetSprite(Item.ItemType.Spear), "Spear", Item.GetCost(Item.ItemType.Spear), 2, () => TryBuyItem(Item.ItemType.Spear));
            CreateItemButton(Item.ItemType.Sword, Item.GetSprite(Item.ItemType.Sword), "Sword", Item.GetCost(Item.ItemType.Sword), 3, () => TryBuyItem(Item.ItemType.Sword));
            CreateItemButton(Item.ItemType.Katana, Item.GetSprite(Item.ItemType.Katana), "Katana", Item.GetCost(Item.ItemType.Katana), 4, () => TryBuyItem(Item.ItemType.Katana));
            CreateItemButton(Item.ItemType.Polearm, Item.GetSprite(Item.ItemType.Polearm), "Polearm", Item.GetCost(Item.ItemType.Polearm), 5, () => TryBuyItem(Item.ItemType.Polearm));
            CreateItemButton(Item.ItemType.Mace, Item.GetSprite(Item.ItemType.Mace), "Mace", Item.GetCost(Item.ItemType.Mace), 6, () => TryBuyItem(Item.ItemType.Mace));
        
            Hide();
        
            SetFirstSelectedButton();
        }

        private void CreateItemButton(Item.ItemType itemType, Sprite itemSprite, string itemName, int itemCost, int positionIndex, Action onClickCallback)
        {
            Transform shopItemTransform = Instantiate(_shopItemTemplate, _container);
            shopItemTransform.gameObject.SetActive(true);
            RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        
            float shopItemHeight = 70f;
            shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);
        
            shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(itemName);
            shopItemTransform.Find("priceText").GetComponent<TextMeshProUGUI>().SetText(itemCost.ToString());
            shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = itemSprite;

            Button itemButton = shopItemTransform.GetComponent<Button>();
            itemButton.onClick.AddListener(onClickCallback.Invoke);
        
            if (_firstButton == null)
            {
                _firstButton = itemButton;
            }
        }
    
        private void SetFirstSelectedButton()
        {
            // Ensure a first button is stored
            if (_firstButton != null)
            {
                // Set the first button as the initially selected button
                _firstButton.Select();
            }
        }

        private void TryBuyItem(Item.ItemType itemType)
        {
            bool success = CoinManager.Instance.SpendGoldAmount(Item.GetCost(itemType));
            if (success)
            {
                Debug.Log("Successfully purchased item: " + itemType);
                _customer.BuyItem(itemType);
            }
            else
            {
                Debug.Log("Insufficient funds to purchase item: " + itemType);
                _messageToShow = "Insufficient funds to purchase item: " + itemType;
            }
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
    
        private void OnGUI()
        {
            if (!string.IsNullOrEmpty(_messageToShow))
            {
                // Calculate the position to center the label
                float labelWidth = 200;
                float labelHeight = 40;
                float x = (Screen.width - labelWidth) / 2;
                float y = Screen.height * 0.7f - labelHeight / 2; // Bottom 30% of the screen
                
                // Draw the label at the bottom 30% of the screen
                GUI.contentColor = Color.yellow;
                GUIStyle style = GUI.skin.label;
                style.fontSize = 20;
                style.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), _messageToShow, style);
                _messageToShow = null;
            }
        }
    }
}
