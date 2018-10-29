using UnityEngine.UI;

public class HUDScreen: SingletonMonoBehaviour<HUDScreen>
{

    public Text PickupText;

    public void PickupItem(string id)
    {
        PickupText.text = "PRESS 'F' TO PICK UP: " + id;
    }

    public void DismissPickupItem()
    {
        PickupText.text = "";
    }
    
}