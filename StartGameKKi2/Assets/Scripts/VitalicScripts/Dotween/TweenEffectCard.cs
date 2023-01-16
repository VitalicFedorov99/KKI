using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TweenEffectCard : MonoBehaviour
{
    Tween _tween;
    [SerializeField] private RectTransform _place;
 //   [SerializeField] private Image _image;
    [SerializeField] private RectTransform _cardEffect;
    [SerializeField] private Canvas _canvas;

    public void Setup(RectTransform card) 
    {
        _cardEffect = card;
        UseCard();
    }


    public void UseCard()
    {
        StartCoroutine(CoroutineUseCard());
    }

    private IEnumerator CoroutineUseCard() 
    {
        _cardEffect.parent = _canvas.transform;
        _tween = _cardEffect.DOMove(new Vector2(_place.transform.position.x, _place.transform.position.y), 0.5f);
        yield return new WaitForSeconds(0.5f);
        //card.parent = place.transform;
       
    }
}
