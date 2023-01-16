using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
public class TweenGoInDump : MonoBehaviour
{
    Tween _tween;
    [SerializeField] private GameObject _CanvasStart;
    
    public void MoveCard(RectTransform card,RectTransform place) 
    {
        StartCoroutine(CoroutuneMove( card,  place));
    }

    IEnumerator CoroutuneMove(RectTransform card, RectTransform place)
    {
        card.parent = _CanvasStart.transform;
        _tween = card.DOMove(new Vector2(place.transform.position.x, place.transform.position.y), 0.5f);
        yield return new WaitForSeconds(0.7f);
        card.parent = place.transform;
    }
}
