using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class TweenEffectCard : MonoBehaviour
{
    [SerializeField] private RectTransform _place;
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _cardEffect;
    [SerializeField] private Canvas _canvas;

    public void Setup(RectTransform card) 
    {
        _cardEffect = card;
    }


    public void UseCard()
    {
        StartCoroutine(CoroutineUseCard());
    }

    private IEnumerator CoroutineUseCard() 
    {
        _cardEffect.parent = _canvas.transform;
        // _image.do
        yield return new WaitForSeconds(1f);
    }
}
