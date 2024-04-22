﻿using System.Collections.Generic;
using Interactables;
using UnityEngine;

namespace Interaction
{
    public class Interaction : MonoBehaviour
    {
        private IInteractable _closestInteractable;
        
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
            
            _closestInteractable = null;
            
            foreach (var collider in colliders)
            {
                var interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    _closestInteractable = interactable;
                    break; // Stop searching after finding the first interactable
                }
            }
        }
        
        public void InteractWithClosest()
        {
            if (_closestInteractable != null)
            {
                _closestInteractable.Interact(interactor);
            }
        }
        
        private void OnGUI()
        {
            if (_closestInteractable != null)
            {
                float labelWidth = 200;
                float labelHeight = 40;
                float x = (Screen.width - labelWidth) / 2;
                float y = Screen.height * 0.7f - labelHeight / 2;
                GUI.contentColor = Color.yellow;
                GUIStyle style = GUI.skin.label;
                style.fontSize = 20;
                style.alignment = TextAnchor.MiddleCenter;
                GUI.Label(new Rect(x, y, labelWidth, labelHeight), "[Press E to interact]", style);
            }
        }
    }
}