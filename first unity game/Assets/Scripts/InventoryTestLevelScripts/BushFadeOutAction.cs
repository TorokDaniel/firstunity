using Inventory;
using UnityEngine;

namespace InventoryTestLevelScripts
{
    public class BushFadeOutAction : InventoryAction
    {
        public override void OnUseMethod()
        {
            var fadingCoroutine = Scripts.FadeTo(GetComponent<Renderer>().material, 0f, 2);
            CoroutineHelper.StartCoroutine("bush_fade_out", fadingCoroutine, () =>
            {
                Destroy(gameObject);
            });
        }
        
    }
}