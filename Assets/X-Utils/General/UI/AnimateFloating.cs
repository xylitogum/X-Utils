using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace X_Utils.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class AnimateFloating : MonoBehaviour
    {
        public float range = 5f;
        public float period = 1f;
        RectTransform rect;
        Vector3 pivotPos;
        // Use this for initialization

        void Awake()
        {
            rect = GetComponent<RectTransform>();
        }
        
        
        void Start()
        {
            
            pivotPos = rect.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            rect.localPosition = pivotPos + Vector3.up * Mathf.Sin(Time.time / period * 2 * Mathf.PI) * range;
        }
    }
}
