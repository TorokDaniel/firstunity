using System.Collections;
using Inventory;
using UnityEngine;

namespace InventoryTestLevelScripts
{
    public class BushFadeOutAction : InventoryAction
    {
        public override void OnUseMethod()
        {
            GetComponent<HUDHint>().OnSceneHudHint.GetComponent<Canvas>().enabled = false;
            
            var fadingCoroutine = Scripts.FadeTo(GetComponent<Renderer>().material, 0f, 2);
            CoroutineHelper.StartCoroutine(fadingCoroutine, () => Destroy(gameObject));
            CoroutineHelper.StartCoroutine(Wait(1), DisableCollider);
        }

        private void DisableCollider()
        {
            Destroy(GetComponent<Collider>());
        }

        private IEnumerator Wait(float seconds)
        {
            var t = 0f;
            while (t < seconds)
            {
                t += Time.deltaTime;
                yield return null;                
            }
        }
        
    }
}