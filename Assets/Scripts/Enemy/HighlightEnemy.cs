using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class HighlightEnemy : MonoBehaviour
    {

        public Outline outline;

        void Start() {
            outline = GetComponent<Outline>();
            outline.eraseRenderer = true;
            outline.enabled = false;
        }

        void OnMouseEnter() {
            outline.enabled = true;
            outline.eraseRenderer = false;
        }
        void OnMouseExit() {
            outline.enabled = false;
        }
    }
}