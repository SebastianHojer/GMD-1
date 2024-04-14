using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace Interaction
{
    public class Interaction : MonoBehaviour
    {
        private List<IInteractable> _interactables = new List<IInteractable>();
        
        private LayerMask _interactionLayer;

        [SerializeField] private GameObject interactor;

        private void Start()
        {
            _interactionLayer = LayerMask.GetMask("Interactable");
            interactor = transform.parent.gameObject;
        }

        public void CheckForInteractions()
        {
            Debug.Log("Checking for interactions");
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
            foreach (var collider in colliders)
            {
                Debug.Log("Collider:" + collider);
                var interactable = collider.GetComponent<IInteractable>();
                Debug.Log("Interactable is null: " + (interactable == null));
                if (interactable != null)
                {
                    interactable.Interact(interactor);
                }
            }
        }
        
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 200, 20), "Press E to interact.");
        }
    }
}