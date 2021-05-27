using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private GameObject _template;

    #region Singleton
    public static ObjectPool Instance { get; private set; }
    #endregion

    [SerializeField] private ItemBase _itemDataBase;

    public ItemBase ItemBase => _itemDataBase;

    private List<Item> _items = new List<Item>();
    public List<Item> Items => _items;

    private List<CubeItem> _poolCube = new List<CubeItem>();

    public List<CubeItem> PoolCube => _poolCube;

    private List<string> _blackList = new List<string>();

    private void Awake()
    {
        Instance = this;
        InitGameItems();
    }

    private void InitGameItems()
    {
        for (int i = 0; i < _itemDataBase.GetItemsCount(); i++)
        {
            _items.Add(_itemDataBase.GetItemOfID(i));
        }
    }

    public void Initialize(int countCube)
    {
        for (int i = 0; i < countCube; i++)
        {
            var cubeItem = Instantiate(_template, _container.transform).GetComponent<CubeItem>();
            _poolCube.Add(cubeItem);
        }
    }

    public void ClearCube()
    {
        for (int i = 0; i < _poolCube.Count; i++)
        {
            _poolCube[i].gameObject.SetActive(false);
        }
    }

    public void FillCubeContainer(ItemType itemType, int countItems)
    {
        var resultItems = _items.Where(p => p.ItemType == itemType).ToList();

        if (resultItems.Count < countItems)
            return;

        for (int i = 0; i < countItems; i++)
        {
            var randomItem = resultItems[Random.Range(0, resultItems.Count)];

            _poolCube[i].Init(randomItem);
            resultItems.Remove(randomItem);

            if (_poolCube[i].gameObject.activeSelf == false)
            {
                _poolCube[i].gameObject.SetActive(true);
            }
        }
    }

    public string GetTarget(int countItems)
    {       
        return CheckBlackList(countItems);
    }

    private string CheckBlackList(int countItems)
    {
        List<string> whiteListTargets = new List<string>();
        for (int i = 0; i < countItems; i++)
        {
            if (_blackList.FirstOrDefault(p => p == _poolCube[i].Value) == null)
            {
                whiteListTargets.Add(_poolCube[i].Value);
            }
        }

        if (whiteListTargets.Count == 0)
        {
            return _poolCube[Random.Range(0, countItems)].Value;
        }
        else
        {
            string randomTarget = whiteListTargets[Random.Range(0, whiteListTargets.Count)];
            _blackList.Add(randomTarget);
            return randomTarget;
        }
    }
}
