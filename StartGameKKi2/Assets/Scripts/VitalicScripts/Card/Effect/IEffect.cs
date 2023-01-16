using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffect
{
    void Setup();

    void Call();


    TypeEffect typeEffect { get;}

}

public enum TypeEffect 
{
    StartEffect,
    FightEffect,
    DeathEffect
}
