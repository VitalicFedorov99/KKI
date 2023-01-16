using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CardGame.Core;
using CardGame.Cards.Effects;

public class Vampirism : MonoBehaviour
{
    public void AddVampirism()
    {
        var card = ObserverCard._instance.GetCurrentCard();
        card.gameObject.AddComponent<VampirismComponent>();
        card.GetComponent<VampirismComponent>().Setup();
        card.ActionFight.AddListener(card.GetComponent<VampirismComponent>().Call);
        ApplayEffect.instance.EffectComplite();
    }

}
