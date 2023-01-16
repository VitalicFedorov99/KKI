using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CardGame.Cards.Base;
using CardGame.Cards.Effects;
namespace CardGame.Cards.Model
{
    public class ModelCard : ScriptableObject
    {
        public string Name;
        [TextArea] public string Description;
        public Sprite SpriteCard;
        public int Price;
        public TypeCard TypeCard;
        public CardClass Class;
        


        public UnityEvent ActionStart;

        public UnityEvent ActionRegulary;
        public UnityEvent ActionDead;

        public UnityEvent ActionFight;
        public virtual void UpdateType()
        {

        }
    }



    public enum TypeCard
    {
        Unit = 1,
        Effect = 2
    }
}
