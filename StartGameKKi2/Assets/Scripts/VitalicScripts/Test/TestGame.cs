using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Cards.Model;
using CardGame.Core;
using CardGame.Ui;
namespace CardGame.Test {
    public class TestGame : MonoBehaviour
    {
        [SerializeField] private Card _prefabCard;

        [SerializeField] private List<ModelCard> _listCardModel;
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Player _prefabPlayer;
        [SerializeField] private Player _player;
        [SerializeField] private UpdaterPopap _updaterPopap;
        private void Start()
        {
            _player = Instantiate(_prefabPlayer);
        }

        public void CreateCard()
        {
            var card = Instantiate(_prefabCard, _canvas.transform);
            var rand = Random.Range(0, _listCardModel.Count);
            card.Setup(_player, _listCardModel[rand],_updaterPopap);
            card.TogelIsCanDrag(true);
        }
    }
}
