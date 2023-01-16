using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CardGame.Core;
using CardGame.Cards.Base;
using CardGame.Cards.Model;
using CardGame.Ui;
using CardGame.Cards.Effects;

namespace CardGame.Field
{
    public class GeneratorGameBoard : MonoBehaviour
    {
        [SerializeField] private GameObject _casing;
        [SerializeField] public Transform _playerArea, _playerBoard, _playerDeck, _playerDump;
        [SerializeField] public Transform _enemyArea, _enemyBoard, _enemyDeck, _enemyDump;
        [SerializeField] private Text _playerDeckCountText, _enemyDeckCountText;
        [SerializeField] private Image _imagePlayerHero, _imageEnemyHero;
        [SerializeField] private Transform _canvas;
        [SerializeField] private PlaceHero _heroPlayer, _heroEnemy;
        [SerializeField] private UpdaterPopap _popap;
        [SerializeField] private UiLineRender _lineRenderer;
        [SerializeField] private Card _prefabCard;
        [SerializeField] private PlaceHero _currentPlaceHero;


        [SerializeField] private Canvas _canvasChooseCard;
        [SerializeField] private Transform _areaChooseCard;
        [SerializeField] private ApplayEffect _applay;

        public Transform GetAreaChooseCard() 
        {
            return _areaChooseCard;
        }

        public void SwitchAreaChooseCard(bool flag) 
        {
            _areaChooseCard.gameObject.SetActive(flag);
        }
      
        public void StartGame(ActionsWithCards current, ActionsWithCards enemy)
        {
            GenerateCards(current.AllModelCards, _playerDeck, current);
            GenerateCards(enemy.AllModelCards, _enemyDeck, enemy);


            for (int i = 0; i < 5; i++)
            {
                CardFromDeckInHand(enemy, enemy, _enemyArea);
                enemy._inHandCards[i].CloseCard();
            }
            Generate(current, enemy);
        }



        public void Generate(ActionsWithCards current, ActionsWithCards enemy)
        {
            ClearAllEndStep(current, enemy);
            ReplacePlayers(current, enemy);
            for (int i = 0; i < 5; i++)
            {
                CardFromDeckInHand(current, current, _playerArea);
            }
            foreach (Card card in current._inHandCards)
            {
                card.OpenCard();
            }
            foreach (Card card in enemy._inHandCards)
            {
                card.CloseCard();
            }
            foreach (Card card in enemy._inDeckCards)
            {
                card.CloseCard();
            }
            foreach (Card card in current._inDeckCards)
            {
                card.CloseCard();
            }
            UpdateDeckCount(current._inDeckCards.Count, _playerDeckCountText);
        }


        public GameObject GetHero(PlayerType player) 
        {
            if (_heroPlayer.playerType == player) 
            {
                _currentPlaceHero = _heroPlayer;
            }
            if(_heroEnemy.playerType == player) 
            {
                _currentPlaceHero = _heroEnemy;
            }
            return _currentPlaceHero.gameObject;
        }

        private void CardFromDeckInHand(ActionsWithCards who, ActionsWithCards whereFrom, Transform area)
        {
            if (whereFrom._inDeckCards.Count > 0 && who._inHandCards.Count < 5)
            {
                Card card = whereFrom._inDeckCards[whereFrom._inDeckCards.Count - 1];
                who.AddCardInList(who._inHandCards, card);
                whereFrom.RemoveCardInList(whereFrom._inDeckCards, card);
                card.transform.SetParent(area);
                card.OpenCard();
            }
        }



        private void ReplacePlayers(ActionsWithCards player, ActionsWithCards enemy)
        {
            foreach (Card card in player._inHandCards)
            {
                card.transform.SetParent(_playerArea);
            }
            foreach (Card card in player._inDeckCards)
            {
                card.transform.position = _playerDeck.transform.position;
                card.transform.SetParent(_playerDeck);
            }
            foreach (Card card in player._inBoardCards)
            {
                card.transform.SetParent(_playerBoard);
            }
            foreach (Card card in player._inDumpCards)
            {
                card.transform.position = _playerDump.transform.position;
                card.transform.SetParent(_playerDump);

            }


            foreach (Card card in enemy._inHandCards)
            {
                card.transform.SetParent(_enemyArea);
            }
            foreach (Card card in enemy._inDeckCards)
            {
                card.transform.position = _enemyDeck.transform.position;
                card.transform.SetParent(_enemyDeck);
            }
            foreach (Card card in enemy._inBoardCards)
            {
                card.transform.SetParent(_enemyBoard);
            }
            foreach (Card card in enemy._inDumpCards)
            {
                card.transform.position = _enemyDump.transform.position;
                card.transform.SetParent(_enemyDump);
            }

            _imageEnemyHero.sprite = enemy.GetSpriteHero();
            _imagePlayerHero.sprite = player.GetSpriteHero();

            _heroEnemy.playerType = enemy.GetComponent<Player>().PlayerType;
            _heroPlayer.playerType = player.GetComponent<Player>().PlayerType;

            player.GetComponent<PlayerActions>().SetEnemyObject(_heroEnemy.gameObject);
            enemy.GetComponent<PlayerActions>().SetEnemyObject(_heroPlayer.gameObject);

        }


        private void ClearAllEndStep(ActionsWithCards player, ActionsWithCards enemy)
        {
            foreach (Card card in player._inHandCards)
            {
                card.transform.SetParent(_canvas);
            }
            foreach (Card card in player._inDeckCards)
            {
                card.transform.SetParent(_canvas);
            }
            foreach (Card card in player._inBoardCards)
            {
                card.transform.SetParent(_canvas);
            }
            foreach (Card card in player._inDumpCards)
            {
                card.transform.SetParent(_playerDump);
            }


            foreach (Card card in enemy._inHandCards)
            {
                card.transform.SetParent(_canvas);
            }
            foreach (Card card in enemy._inDeckCards)
            {
                card.transform.SetParent(_canvas);
            }
            foreach (Card card in enemy._inBoardCards)
            {
                card.transform.SetParent(_canvas);
            }
            foreach (Card card in enemy._inDumpCards)
            {
                card.transform.SetParent(_enemyDump);
            }

        }


        public Card ApplyEffect(Card card, Player player) 
        {
            if (Game.CurrentPlayer == player)
            {
                Twinner._instance.ApplyEffect(card.GetComponent<RectTransform>());
            }
            _applay.SetupEffect(card.GetEffect(), card);
                return card;
        }

        

        public Card CardInDump(Card card, Player player) 
        {
            if (Game.CurrentPlayer == player) 
            {
                Twinner._instance.CardMove(card.GetComponent<RectTransform>(), _playerDump.GetComponent<RectTransform>());
            }
            else 
            {
                Twinner._instance.CardMove(card.GetComponent<RectTransform>(), _enemyDump.GetComponent<RectTransform>());
            }
            return card;
        }

     

        private void UpdateDeckCount(int count, Text text)
        {
            text.text = count.ToString();
        }


       

        private void GenerateDump(Card last, Transform parent)
        {
            foreach (Transform child in parent)
            {
                if (!child.GetComponent<Text>())
                    child.gameObject.SetActive(false);
            }

            if (last)
            {
                last.transform.SetParent(parent);
                last.transform.position = parent.position;
                last.gameObject.SetActive(true);
            }
        }

        private void GenerateCasing(int count, Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < count; i++)
            {
                Instantiate(_casing, parent);
            }
        }



        private void GenerateCards(List<ModelCard> models, Transform patent, ActionsWithCards player)
        {
            RemoveAllChild(patent);
            int i = 0;
            foreach (var model in models)
            {

                var card = Instantiate(_prefabCard);
                card.name = player.name + " " + i;
                card.transform.position = patent.transform.position;
                card.transform.SetParent(patent);
                card.Setup(player.GetPlayer(), model, _popap);
                card.SetupLineRenderer(_lineRenderer);
                card.TogelIsCanDrag(true);
                player.AddCardInList(player._inDeckCards, card);
                i++;
                //card.gameObject.SetActive(true);
            }
            player.CardDeckMix();

        }





        private void RemoveAllChild(Transform parent)
        {
            foreach (Transform child in parent)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
