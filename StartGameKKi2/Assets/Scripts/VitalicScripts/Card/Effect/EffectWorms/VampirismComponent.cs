using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardGame.Cards.Base;
public class VampirismComponent : MonoBehaviour, IEffect
{
    public TypeEffect Teffect = TypeEffect.FightEffect;

    private Unit _unit;

    public TypeEffect typeEffect => Teffect;

    public void Call()
    {
        _unit.Healthing(_unit.Damage);
    }

    public void Setup()
    {
        _unit = GetComponent<Unit>();

    }
}
