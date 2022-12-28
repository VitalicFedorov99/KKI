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

        //_canvasFight.gameObject.SetActive(true);
        //_CanvasStart.gameObject.SetActive(false);
        //_placeObj = _object1.transform.position;
        card.parent = _CanvasStart.transform;

        //_object1.parent = _CanvasStart.transform;
        //_object2.transform.position = _placePlayer.transform.position;
        //_object1.transform.position = _placeEnemy.transform.position;
        _tween = card.DOMove(new Vector2(place.transform.position.x, place.transform.position.y), 0.5f);
        yield return new WaitForSeconds(0.7f);
        //_CanvasStart.gameObject.SetActive(true);
        //_canvasFight.gameObject.SetActive(false);
        card.parent = place.transform;
        //_tween = _object1.DOMove(_placeObj, 5f);
    }
}
