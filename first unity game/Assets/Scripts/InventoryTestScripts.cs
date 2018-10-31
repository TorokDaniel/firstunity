using UnityEngine;

public class InventoryTestScripts : MonoBehaviour
{
    public void OnWallItemUsed(GameObject triggeredGameObject)
    {
        Destroy(triggeredGameObject);
    }
    
}