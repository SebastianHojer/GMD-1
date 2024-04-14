using Interactables;
using UnityEngine;

public class ShopTriggerCollider : MonoBehaviour, IInteractable
{
    [SerializeField] private Shop_UI _shopUI;
    private ICustomer _customer;

    public void Interact(GameObject interactor)
    {
        _customer = interactor.GetComponent<ICustomer>();
        Debug.Log("Interacting with shop");
        if (_customer != null)
        {
            _shopUI.Show(_customer);
        }
        else
        {
            _customer = interactor.GetComponent<ICustomer>();
            if (_customer != null)
            {
                _shopUI.Show(_customer);
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        _shopUI.Hide();
    }
}
