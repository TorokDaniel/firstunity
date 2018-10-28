using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public delegate void Visitor(string id, int quantity);
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

    public void VisitItems(Visitor visitor)
    {
        foreach (var item in _inventory)
        {
            var id = item.Key;
            var quantity = item.Value;
            visitor(id, quantity);
        }
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