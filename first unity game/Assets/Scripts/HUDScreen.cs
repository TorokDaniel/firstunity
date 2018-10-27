using UnityEngine;
using UnityEngine.UI;

public class HUDScreen: MonoBehaviour
{
    public static HUDScreen Instance { get; private set; }
    public Text PickupText;

    public void PickupItem(string id)
    {
        PickupText.text = "PRESS 'F' TO PICK UP: " + id;
    }

    public void DismissPickupItem()
    {
        PickupText.text = "";
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    
}