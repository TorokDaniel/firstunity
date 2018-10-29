using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory: SingletonMonoBehaviour<Inventory>
{
    
    private readonly Dictionary<string, int> _items = new Dictionary<string, int>();
    public Dictionary<string, int> Items
    {
        get { return _items.ToDictionary(e => e.Key, e => e.Value); }
    }

    public void AddItem(string id)
    {
        if (!_items.ContainsKey(id))
        {
            _items[id] = 0;
        }

        _items[id] = _items[id] + 1;
        Debug.Log(id);
    }
    
}