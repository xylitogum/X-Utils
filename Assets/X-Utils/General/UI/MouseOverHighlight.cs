using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace X_Utils.UI
{
    public class MouseOverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public HighlightMethod highlightMethod;
        public enum HighlightMethod
        {
            None,
            Enlarged,
            EnableShadow
        }

        //[EnumHide("highlightMethod", 1, true)]
        public Vector3 enlargeScale = Vector3.one;

        private Vector3 savedScale = Vector3.one;
        private Shadow m_shadow;

        void Awake()
        {
            m_shadow = GetComponent<Shadow>();
        }
        
        // Use this for initialization
        void Start()
        {
            savedScale = transform.localScale;
            if (m_shadow) m_shadow.enabled = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            switch (highlightMethod)
            {
                case HighlightMethod.Enlarged:
                    {
                        savedScale = transform.localScale;
                        transform.localScale = enlargeScale;
                        break;
                    }
                case HighlightMethod.EnableShadow:
                    {
                        m_shadow.enabled = true;
                        break;
                    }
            }

        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (highlightMethod)
            {
                case HighlightMethod.Enlarged:
                    {
                        transform.localScale = savedScale;
                        break;
                    }
                case HighlightMethod.EnableShadow:
                    {
                        m_shadow.enabled = false;
                        break;
                    }
            }

        }
    }

}
