using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CardGame.Cards.Model;

namespace CardGame.Ui
{
    public class UpdaterPopap : MonoBehaviour
    {
        [SerializeField] private Image _iconPers;
        [SerializeField] private Text _textName;
        [SerializeField] private Text _textClass;
        [SerializeField] private Text _textCost;
        [SerializeField] private GameObject _popap;
        [SerializeField] private Text _textDescription;
        


        public void UpdatePopap(ModelCard card)
        {
         

            _popap.SetActive(true);
            _iconPers.sprite = card.SpriteCard;
            _textName.text = card.Name;
            _textClass.text = card.Class.ToString();
            _textCost.text = card.Price.ToString();
            _textDescription.text = card.Description;
        }

        public void OffPopap() 
        {
            _popap.SetActive(false);
        }



    }
}
