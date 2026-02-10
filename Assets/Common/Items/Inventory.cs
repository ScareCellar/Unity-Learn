using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<IItem> items = new List<IItem>();

    public IItem CurrentItem { get; private set; } = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentItem = items[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
