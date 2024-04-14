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
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position);
            foreach (var collider in colliders)
            {
                var interactable = collider.GetComponent<IInteractable>();
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