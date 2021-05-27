using UnityEngine;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    [SerializeField] private CanvasGroup _gameOverPanel;

    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        _gameOverPanel.DOFade(0, 1f);
        _gameOverPanel.interactable = false;
        _gameOverPanel.blocksRaycasts = false;
    }

    public void EndGame()
    {
        _gameOverPanel.DOFade(1, 1f);
        _gameOverPanel.interactable = true;
        _gameOverPanel.blocksRaycasts = true;
    }
}
