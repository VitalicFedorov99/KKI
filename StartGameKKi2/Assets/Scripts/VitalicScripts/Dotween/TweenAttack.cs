using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TweenAttack : MonoBehaviour
{
    [SerializeField] RectTransform _object1;
    [SerializeField] RectTransform _object2;
    //[SerializeField] private Canvas _canvasStart;
    [SerializeField] private Canvas _canvasFight;
    [SerializeField] private Canvas _CanvasStart;
    [SerializeField] private Transform _enemyBoard;
    [SerializeField] private Transform _playerBoard;
    [SerializeField] private Transform _placeEnemy;
    [SerializeField] private Transform _placePlayer;
    private Vector3 _placeObj;
    Tween _tween;


    public void Setup(RectTransform object1, RectTransform object2) 
    {
        _object1 = object1;
        _object2 = object2;
    }
    public void Attack() 
    {
        StartCoroutine(CoroutineAttack());
    }

    public void AttackInHero()
    {
        StartCoroutine(CoroutineAttackInHero());
    }

    IEnumerator CoroutineAttack() 
    {

        //_canvasFight.gameObject.SetActive(true);
        //_CanvasStart.gameObject.SetActive(false);
        _placeObj = _object1.transform.position;
        _object2.parent = _CanvasStart.transform;
       
        _object1.parent = _CanvasStart.transform;
        //_object2.transform.position = _placePlayer.transform.position;
        //_object1.transform.position = _placeEnemy.transform.position;
        _tween = _object1.DOMove( new Vector2(_object2.transform.position.x, _object2.transform.position.y), 0.5f);
        yield return new WaitForSeconds(0.7f);
        _tween = _object1.DOMove(new Vector2(_placeObj.x, _placeObj.y), 0.5f);
        yield return new WaitForSeconds(1f);
        //_CanvasStart.gameObject.SetActive(true);
        //_canvasFight.gameObject.SetActive(false);
        _object1.parent = _enemyBoard;
        _object2.parent = _playerBoard;
        //_tween = _object1.DOMove(_placeObj, 5f);
    }

    IEnumerator CoroutineAttackInHero() 
    {
      
     
        _placeObj = _object1.transform.position;
        

        _object1.parent = _CanvasStart.transform;
        
        _tween = _object1.DOMove(new Vector2(_object2.transform.position.x, _object2.transform.position.y), 0.5f);
        yield return new WaitForSeconds(0.7f);
        _tween = _object1.DOMove(new Vector2(_placeObj.x, _placeObj.y), 0.5f);
        yield return new WaitForSeconds(1f);
        
        _object1.parent = _enemyBoard;
    }


}
