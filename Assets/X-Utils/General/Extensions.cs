using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace X_Utils
{

    public static class Collision2DExtension
    {
        /// <summary>
        /// Calculates the Impulse of this incoming collision by multiplying the force with the duration.
        /// </summary>
        /// <returns>The impulse, in (N * s).</returns>
        /// <param name="col">Collision information.</param>
        public static float Impulse(this Collision2D col)
        {
            float totalImpulse = 0f;
            ContactPoint2D[] contacts = new ContactPoint2D[16];
            int contactCount = col.GetContacts(contacts);
            for (int i = 0; i < contactCount; i++) {
                totalImpulse += contacts[i].normalImpulse * Time.fixedDeltaTime;
            }
            return totalImpulse;
        }


    }

    public static class Vector3Extension
    {
        // Get XY part of Vector3. Well, typecasting is better.
        public static Vector2 GetXY(this Vector3 v3)
        {
            return new Vector2(v3.x, v3.y);
        }

        /// <summary>
        /// Converts an array of Vector3 to a vector2 array, by throwing away z axis.
        /// </summary>
        /// <returns>The converted vector2 array.</returns>
        /// <param name="v3s">The given Vector3 array.</param>
        public static Vector2[] ToVector2Array(this Vector3[] v3s)
        {
            return System.Array.ConvertAll<Vector3, Vector2>(v3s, v => new Vector2(v.x, v.y));
        }

    }


    public static class Vector2Extension
    {
        /// <summary>
        /// Rotate the vector clockwise by specified degrees.
        /// </summary>
        /// <returns>The vector after rotation.</returns>
        /// <param name="v">Vector.</param>
        /// <param name="degrees">Degrees.</param>
        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;
            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
        }

        /// <summary>
        /// Converts an array of Vector2 to a vector3 array, by setting z to zero.
        /// </summary>
        /// <returns>The converted vector3 array.</returns>
        /// <param name="v2s">The given Vector2 array.</param>
        public static Vector3[] ToVector3Array(this Vector2[] v2s)
        {
            return System.Array.ConvertAll<Vector2, Vector3>(v2s, v => new Vector3(v.x, v.y, 0f));
        }

    }

    public static class MathfExtensions
    {
        /// <summary>
        /// Repeat the angle so it is between (-180, 180]
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float ToAngle180(this float angle)
        {
            return Mathf.Repeat(angle - 180f, 360f) + 180f;
        }
    }

    public static class LayerMaskExtension
    {
        /// <summary>
        /// Checks if a layermask contains a given layer.
        /// </summary>
        /// <returns><c>true</c>, if layer was contained in the layermask, <c>false</c> otherwise.</returns>
        /// <param name="mask">Given LayerMask.</param>
        /// <param name="layer">Target Layer.</param>
        public static bool ContainLayer(this LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) >= 1;
        }
    }

    public static class Physics2DExtension
    {
        /// <summary>
        /// Raycast onto a specified collider.
        /// </summary>
        /// <returns>If raycast could hit the collider.</returns>
        /// <param name="collider">Target Collider.</param>
        /// <param name="ray">Ray.</param>
        /// <param name="hitInfo">Hit info.</param>
        /// <param name="maxDistance">Maximum distance.</param>
        public static bool Raycast(this Collider2D collider, Ray2D ray, out RaycastHit2D hitInfo, float maxDistance)
        {
            var oriLayer = collider.gameObject.layer;
            const int tempLayer = 31;
            collider.gameObject.layer = tempLayer;
            hitInfo = Physics2D.Raycast(ray.origin, ray.direction, maxDistance, 1 << tempLayer);
            collider.gameObject.layer = oriLayer;
            if (hitInfo.collider && hitInfo.collider != collider)
            {
                Debug.LogError("Collider2D.Raycast() need a unique temp layer to work! Make sure Layer #" + tempLayer + " is unused!");
                return false;
            }
            return hitInfo.collider != null;
        }
    }

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
}
