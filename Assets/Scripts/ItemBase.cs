using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Items Database", menuName = "Databases/Items")]

public class ItemBase : ScriptableObject
{
    [SerializeField, HideInInspector] private List<Item> _items;

    [SerializeField] private Item _currentItem;

    private int _currentIndex;

    public void CreateItem()
    {
        if (_items == null)
            _items = new List<Item>();
        Item item = new Item();
        _items.Add(item);
        _currentItem = item;
        _currentIndex = _items.Count - 1;
    }

    public void RemoveItem()
    {
        if (_items == null)
            return;

        if (_currentItem == null)
            return;

        _items.Remove(_currentItem);

        if (_items.Count > 0)
        {
            _currentItem = _items[0];
        }
        else
        {
            CreateItem();
        }
        _currentIndex = 0;
    }

    public void NextItem()
    {
        if (_currentIndex + 1 < _items.Count)
        {
            _currentIndex++;
            _currentItem = _items[_currentIndex];
        }
    }

    public void PrevItem()
    {
        if (_currentIndex > 0)
        {
            _currentIndex--;
            _currentItem = _items[_currentIndex];
        }
    }

    public Item GetItemOfID(int id)
    {
        return _items.Find(t => t.ID == id);
    }

    public int GetItemsCount()
    {
        return _items.Count;
    }
}

[System.Serializable]
public class Item
{
    [SerializeField] private int _id;
    public int ID => _id;

    [SerializeField] private string _itemValue;
    public string ItemValue => _itemValue;

    [SerializeField] private Sprite _icon;
    public Sprite Icon => _icon;

    [SerializeField] private ItemType _itemType;
    public ItemType ItemType => _itemType;
}

[System.Serializable]
public enum ItemType
{
    LETTERS = 0, 
    NUMBERS = 1
}



