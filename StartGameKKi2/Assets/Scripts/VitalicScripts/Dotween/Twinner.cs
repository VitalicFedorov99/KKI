using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twinner : MonoBehaviour
{
    public static Twinner _instance;

    [SerializeField] private TweenAttack _twinnerAttack;
    [SerializeField] private TweenGoInDump _tweenGoInDump;
    [SerializeField] private TweenEffectCard _tweenEffectCard;

    private void Awake()
    {
        _instance = this;
    }
    public void Attack(RectTransform obj1, RectTransform obj2) 
    {
       
        _twinnerAttack.Setup(obj1, obj2);
        _twinnerAttack.Attack();
    }

    public void AttackInHero(RectTransform obj1, RectTransform obj2)
    {
     
        _twinnerAttack.Setup(obj1, obj2);
        _twinnerAttack.AttackInHero();
    }

    public void ApplyEffect(RectTransform card) 
    {
        _tweenEffectCard.Setup(card);
    }


    public void CardMove(RectTransform card, RectTransform place) 
    {
        _tweenGoInDump.MoveCard(card, place);
    }
}
