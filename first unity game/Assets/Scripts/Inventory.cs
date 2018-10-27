using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public static Inventory Instance { get; private set; } 
    private readonly Dictionary<string, int> _inventory = new Dictionary<string, int>();

    public void AddItem(string id)
    {
        if (!_inventory.ContainsKey(id))
        {
            _inventory[id] = 0;
        }
        _inventory[id] = _inventory[id] + 1;
        Debug.Log(id);
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