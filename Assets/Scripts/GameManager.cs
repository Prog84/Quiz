using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _target;
    [SerializeField] private ParticleSystem _starsPatricle;
    [SerializeField] private GameOver _gameOver;

    private int _currentLevel;
    private int _countCubeAtLevel = 3;
    private int _maximumCubeCount = 9;
    private int _startLevel = 1;
    private int _finishLevel = 3;
    private string _randomTarget;

    private void Start()
    {
        ObjectPool.Instance.Initialize(_maximumCubeCount);

        for (int i = 0; i < ObjectPool.Instance.PoolCube.Count; i++)
        {
            ObjectPool.Instance.PoolCube[i].CubeSelected += OnCubeSelected;
        }

        StartLevel();
    }

    private void StartLevel()
    {
        _gameOver.StartGame();
        _currentLevel = _startLevel;
        ObjectPool.Instance.ClearCube();
        SetItems();
        SetTarget();
        SetBounceCubes();
    }

    private void SetBounceCubes()
    {
        for (int i = 0; i < ObjectPool.Instance.PoolCube.Count; i++)
        {
            if (i < _countCubeAtLevel)
            {
                ObjectPool.Instance.PoolCube[i].BounceEffect();
            }
        }
    }

    private ItemType RandomArrayItems()
    {
        Array values = Enum.GetValues(typeof(ItemType));
        System.Random random = new System.Random();
        ItemType randomItemType = (ItemType)values.GetValue(random.Next(values.Length));
        return randomItemType;
    }

    private void SetItems()
    {
        ObjectPool.Instance.FillCubeContainer(RandomArrayItems(), _currentLevel * _countCubeAtLevel);
    }

    private void SetTarget()
    {
        _randomTarget = ObjectPool.Instance.GetTarget(_currentLevel * _countCubeAtLevel);
        _target.text = "Find " + _randomTarget;

        if (_currentLevel == _startLevel)
        {
            _target.DOFade(1, 3);
        }
    }

    private void OnCubeSelected(CubeItem cubeItem)
    {
        if (cubeItem.Value == _randomTarget)
        {
            cubeItem.BounceEffect();
            _starsPatricle.transform.position = cubeItem.transform.position;
            _starsPatricle.Play();

            if (_currentLevel < _finishLevel)
            {
                NextLevel();
            }
            else
            {
                _gameOver.EndGame();
                _target.DOFade(0, 3);
            }
        }
        else
        {
            cubeItem.EaseInBounceEffect();
        }
    }

    private void OnDisable()
    {
        foreach (var item in ObjectPool.Instance.PoolCube)
        {
            item.CubeSelected -= OnCubeSelected;
        }
    }

    private void NextLevel()
    {
        _currentLevel++;
        SetItems();
        SetTarget();
    }
}
