using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils
{
    public static class MathfExtensions
    {
        /// <summary>
        /// Repeat the angle so it is between (-180, 180]
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float ToAngle180 (this float angle)
        {
            return Mathf.Repeat(angle - 180f, 360f) + 180f;
        }
    }


    
}
