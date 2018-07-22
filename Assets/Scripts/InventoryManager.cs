using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private int sympathy_sum = 0;

    
    
    public int GetSympathy()
    {
        return sympathy_sum;
    }

    void Start()
    {
        int cnt = transform.childCount;

        for (int i = 0; i < cnt; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    void Update(){}

    public void InsertItem(Item it)
    {
        items.Add(it);
        sympathy_sum += it.sympathy;
        transform.Find(it.icon_path).gameObject.SetActive(true);
    }

    public void pop()
    {
        if (items.Count > 0)
        {
            Item it = items[0];
            transform.Find(it.icon_path).gameObject.SetActive(false);
            sympathy_sum -= it.sympathy;
            it.usable.ResetUse();
            items.Remove(it);
        }
    }
}
