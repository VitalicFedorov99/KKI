using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CardGame.Core;
using CardGame.Cards.Base;
using CardGame.Cards.Model;
using CardGame.Ui;
namespace CardGame.Field
{
    public class GeneratorGameBoard : MonoBehaviour
    {
        [SerializeField] private GameObject _casing;
        [SerializeField] private Transform _playerArea, _playerBoard, _playerDeck, _playerDump;
        [SerializeField] private Transform _enemyArea, _enemyBoard, _enemyDeck, _enemyDump;
        [SerializeField] private Text _playerDeckCountText, _enemyDeckCountText;
        [SerializeField] private Image _imagePlayerHero, _imageEnemyHero;
        [SerializeField] private Transform _canvas;
        [SerializeField] private GameObject _heroPlayer, _heroEnemy;
        [SerializeField] private UpdaterPopap _popap;
        [SerializeField] private UiLineRender _lineRenderer;
        [SerializeField] private Card _prefabCard;
        public UnityEvent<int, int> playerValuesGenerated;
        public UnityEvent<int, int> enemyValuesGenerated;

        /*   public void Generate(Player current, Player enemy)
           {
               GenerateCards(current.InHandCards, _playerArea);
               GenerateCards(current.InBoardCards, _playerBoard);
               GenerateCards(enemy.InBoardCards, _enemyBoard);
               GenerateCasing(enemy.InHandCards.Count, _enemyArea);
               GenerateDeck(current.InDeckCardsCount, _playerDeck);
               GenerateDeck(enemy.InDeckCardsCount, _enemyDeck);
               GenerateDump(current.LastInDumpCard, _playerDump);
               GenerateDump(enemy.LastInDumpCard, _enemyDump);
               UpdateDeckCount(current.InDeckCardsCount, _playerDeckCountText);
               UpdateDeckCount(enemy.InDeckCardsCount, _enemyDeckCountText);

           }
        */
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
            UpdateDeckCount(current._inDeckCards.Count, _playerDeckCountText);

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

            player.GetComponent<PlayerActions>().SetEnemyObject(_heroEnemy);
            enemy.GetComponent<PlayerActions>().SetEnemyObject(_heroPlayer);

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


        

        public Card CardInDump(Card card, Player player) 
        {
            //Card newCard = Instantiate(card);
            if (Game.CurrentPlayer == player) 
            {
                Debug.Log("Отбой");
                //card.gameObject.SetActive(false);
                //card.transform.SetParent(_canvas);
                //var source = card.GetComponent<RectTransform>();
                //Vector2 shiftDirection = source.anchorMin;
                //source.anchoredPosition = shiftDirection * source.rect.size;
                // Debug.Log("x: "+ card.GetComponent<RectTransform>().anchoredPosition.x + " y:" + card.GetComponent<RectTransform>().anchoredPosition.y);
                //card.transform.position = _playerDump.transform.position;
                //newCard.transform.position= _playerDump.transform.position;
                //newCard.transform.SetParent(_playerDump);

                Twinner._instance.CardMove(card.GetComponent<RectTransform>(), _playerDump.GetComponent<RectTransform>());
                
                Debug.Log("x: " + card.transform.position.x + " y:" + card.transform.position.y);
              //  card.transform.SetParent(_playerDump);
              //  card.gameObject.SetActive(false);
                //Destroy(card.gameObject);
            }
            else 
            {
                Twinner._instance.CardMove(card.GetComponent<RectTransform>(), _enemyDump.GetComponent<RectTransform>());
                //card.transform.SetParent(_canvas);
                //card.GetComponent<RectTransform>().anchoredPosition =  Vector2;
                //card.transform.position = _enemyDump.transform.localPosition;
                //card.transform.SetParent(_enemyDump);
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

            foreach (var model in models)
            {

                var card = Instantiate(_prefabCard);
                card.transform.position = patent.transform.position;
                card.transform.SetParent(patent);
                card.Setup(player.GetPlayer(), model, _popap);
                card.SetupLineRenderer(_lineRenderer);
                card.TogelIsCanDrag(true);
                player.AddCardInList(player._inDeckCards, card);
                //card.gameObject.SetActive(true);
            }
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
