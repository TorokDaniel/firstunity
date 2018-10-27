using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScreen: MonoBehaviour
{
    [Serializable]
    public class NamedImage
    {
        public string Id;
        public Texture2D Image;
    }
    
    public static InventoryScreen Instance { get; private set; }

    public NamedImage[] Images;
    
    private Canvas _screen;
    private Dictionary<string, Texture2D> _images;
    
    private void Start()
    {
        if (Instance == null)
        {
            _screen = GetComponent<Canvas>();
            _screen.enabled = false;
            
            foreach (var image in Images)
            {
                _images.Add(image.Id, image.Image);
            }
            
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            _screen.enabled = !_screen.enabled;
        }
    }
}