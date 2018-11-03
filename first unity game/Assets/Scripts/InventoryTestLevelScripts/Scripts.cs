using System.Collections;
using UnityEngine;

namespace InventoryTestLevelScripts
{
    
    public class Scripts
    {
        public static IEnumerator FadeTo(Material material, float targetOpacity, float duration) {

            // Cache the current color of the material, and its initiql opacity.
            var color = material.color;
            var startOpacity = color.a;

            // Track how many seconds we've been fading.
            var t = 0f;

            while(t < duration) {
                // Step the fade forward one frame.
                t += Time.deltaTime;
                // Turn the time into an interpolation factor between 0 and 1.
                var blend = Mathf.Clamp01(t / duration);

                // Blend to the corresponding opacity between start & target.
                color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

                // Apply the resulting color to the material.
                material.color = color;

                // Wait one frame, and repeat.
                yield return null;
            }
        }
    }
}