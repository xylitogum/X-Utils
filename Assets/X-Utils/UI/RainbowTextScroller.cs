using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace X-Utils.UI
{
    [RequireComponent(typeof(Text))]
    public class RainbowTextScroller : MonoBehaviour
    {
        public Color color;
        public float interval = 0.4f;
        private float t_lastStep;
        private int index = 0;
        private Text UIText;
        private string originalText = "";
        // Use this for initialization
        void Start()
        {
            UIText = GetComponent<Text>();
            originalText = UIText.text;
            t_lastStep = 0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Time.time - t_lastStep >= interval)
            {
                t_lastStep = Time.time;
                index += 1;
                if (index >= originalText.Length)
                {
                    index = 0;
                }
                UpdateText(index);
            }
        }

        void UpdateText(int i)
        {

            string startTag = "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">";
            string endTag = "</color>";
            string result = originalText;
            result = result.Insert(i + 1, endTag);
            result = result.Insert(i, startTag);
            UIText.text = result;
        }
    }
}
