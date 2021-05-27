using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CubeItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _icon;

    private string _value;
    private RectTransform _transform;

    public event UnityAction<CubeItem> CubeSelected;

    public string Value => _value;

    private void Awake()
    {
        _transform = _icon.GetComponent<RectTransform>();
    }

    public void Init(Item item)
    {
        _icon.sprite = item.Icon;
        _value = item.ItemValue;
    }

    public void BounceEffect()
    {
        _transform.localScale = new Vector2(0f, 0f);
        var animationSequence = DOTween.Sequence();
        animationSequence.Append(_transform.DOScale(new Vector2(1.2f, 1.2f), 0.3f));
        animationSequence.Append(_transform.DOScale(new Vector2(0.95f, 0.95f), 0.3f));
        animationSequence.Append(_transform.DOScale(new Vector2(1f, 1f), 0.3f));   
    }

    public void EaseInBounceEffect()
    {
        Tweener tweener = _transform.DOMoveX(transform.position.x + transform.localScale.x / 4, 0.5f); 
        tweener.SetEase(Ease.InBounce); 
        tweener.SetLoops(2, LoopType.Yoyo);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CubeSelected?.Invoke(this);
    }
}
