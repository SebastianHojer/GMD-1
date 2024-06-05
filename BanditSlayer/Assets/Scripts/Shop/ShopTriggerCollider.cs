using System;
using Common;
using Interaction;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Shop
{
    public class ShopTriggerCollider : MonoBehaviour, IInteractable
    {
        private Shop_UI shopUI;
        private ICustomer _customer;

        private void Awake()
        {
            shopUI = Shop_UI.Instance;
        }

        public void Interact(GameObject interactor)
        {
            _customer = interactor.GetComponent<ICustomer>();
            if (_customer != null)
            {
                shopUI.Show(_customer);
            }
            else
            {
                _customer = interactor.GetComponent<ICustomer>();
                if (_customer != null)
                {
                    shopUI.Show(_customer);
                }
            }
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            shopUI.Hide();
        }
    }
}
