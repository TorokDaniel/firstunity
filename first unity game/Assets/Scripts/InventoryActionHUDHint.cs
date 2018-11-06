using Inventory;

public class InventoryActionHUDHint : HUDHint
{

    protected override string TransformText(string text)
    {
        var inventoryAction = GetComponent<InventoryAction>();
        var requiredItem = inventoryAction.RequiredItems[0].InventoryItem.Id;

        var hasItem = Inventory.Inventory.Instance.Items.ContainsKey(requiredItem);
        return (hasItem ? "Use: " : "Required: ") + requiredItem;
    }
    
}