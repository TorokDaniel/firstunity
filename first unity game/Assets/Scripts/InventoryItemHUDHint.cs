using Inventory;

public class InventoryItemHUDHint : HUDHint
{
    
    protected override string TransformText(string text)
    {
        var inventoryItem = GetComponent<InventoryItem>();
        return inventoryItem.Id;
    }
    
}