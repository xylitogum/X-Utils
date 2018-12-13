using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorExtension
{
    
    
    
    /// <summary>
    /// Scales the alpha value of a color.
    /// </summary>
    /// <returns>The alpha-scaled color.</returns>
    /// <param name="color">Given Color.</param>
    /// <param name="alphaScale">The Scale to be applied on alpha.</param>
    public static Color AlphaScaled(this Color color, float alphaScale)
    {
        Color tempColor = new Vector4(color.r, color.g, color.b, color.a * alphaScale);
        return tempColor;
    }
}
