using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryScreen: SingletonMonoBehaviour<InventoryScreen>
    {  
    
        [Serializable]
        public class ItemWithSprite
        {
            public InventoryItem InventoryItem;
            public Sprite Sprite;
        }
    
        public Transform ItemPrefab;
        public ItemWithSprite[] Items;
    
        private Canvas _screen;
        private readonly Dictionary<string, Sprite> _itemSprites = new Dictionary<string, Sprite>();
        private readonly List<Transform> _onScreenItemList = new List<Transform>();

        public override void SingletonStart()
        {
            _screen = GetComponent<Canvas>();
            _screen.enabled = false;
            
            foreach (var image in Items)
            {
                _itemSprites.Add(image.InventoryItem.Id, image.Sprite);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }
        }

        private void ToggleInventory()
        {
            if (!_screen.enabled)
            {
                _screen.enabled = true;
                DisplayItemsOnScreen();
            }
            else
            {
                _screen.enabled = false;
                ClearScreen();
            }
        }

        private void ClearScreen()
        {
            _onScreenItemList.ForEach((item => Destroy(item.gameObject)));
            _onScreenItemList.Clear();
        }

        private void DisplayItemsOnScreen()
        {
            var currentItemIndex = 0;
            foreach (var item in global::Inventory.Inventory.Instance.Items)
            {
                var id = item.Key;
                var quantity = item.Value;
            
                var onScreenItem = CreateItem(id, quantity);
                _onScreenItemList.Add(onScreenItem);
            
                var parentTransform = transform.Find(string.Format("Image ({0})", currentItemIndex));
                onScreenItem.SetParent(parentTransform);
                onScreenItem.transform.position = parentTransform.position;
            
                currentItemIndex += 1;
            }
        }

        private Transform CreateItem(string id, int quantity)
        {
            var item = Instantiate(ItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            item.Find("badge").Find("counter").GetComponent<Text>().text = quantity.ToString();
            item.GetComponent<Image>().sprite = _itemSprites[id];
            return item;
        }
    
    }
}