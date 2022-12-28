using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestDotween : MonoBehaviour
{
   [SerializeField] RectTransform _object1;
   [SerializeField] RectTransform _object2;
    Tween _tween;
    private void Start()
    {
        _object1.DOMove(_object2.transform.position, 5f, true);
    }
}
