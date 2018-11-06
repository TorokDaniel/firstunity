using Inventory;

public class CrateSearchAction : Action
{
    
    public InventoryItem[] ItemsInside;

    protected override void OnAction()
    {
        if (ItemsInside != null && ItemsInside.Length > 0)
        {
            foreach (var item in ItemsInside)
            {
                Inventory.Inventory.Instance.AddItem(item.Id);
            }
        }

        var hint = GetComponent<HUDHint>();
        if (hint != null)
        {
            hint.UpdateText("Empty");
        }
    }
    
}