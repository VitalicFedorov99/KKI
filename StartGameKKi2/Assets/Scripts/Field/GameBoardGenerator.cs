using System.Collections.Generic;
using Cards.Base;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Field
{
    public class GameBoardGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _casing;
        [SerializeField] private Transform _playerArea, _playerBoard, _playerDeck, _playerDump;
        [SerializeField] private Transform _enemyArea, _enemyBoard, _enemyDeck, _enemyDump;
        [SerializeField] private Text _playerDeckCountText, _enemyDeckCountText;

        public UnityEvent<int, int> playerValuesGenerated;
        public UnityEvent<int, int> enemyValuesGenerated;

        public void Generate(Player current, Player enemy)
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

        private void UpdateDeckCount(int count, Text text)
        {
            text.text = count.ToString();
        }

        private void GenerateDeck(int count, Transform parent)
        {
            foreach (Transform child in parent)
            {
                if(!child.GetComponent<Text>())
                    Destroy(child.gameObject);
            }
            
            if (count > 0)
            {
                Instantiate(_casing, parent).transform.position = parent.position;
            }
        }
        
        private void GenerateDump(Card last, Transform parent)
        {
            foreach (Transform child in parent)
            {
                if(!child.GetComponent<Text>())
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
            
        
        private void GenerateCards(List<UnitCard> cards, Transform patent)
        {
            RemoveAllChild(patent);

            foreach (var card in cards)
            {
                card.transform.SetParent(patent);
                card.gameObject.SetActive(true);
            }
        }
        
        private void GenerateCards(List<Card> cards, Transform patent)
        {
            RemoveAllChild(patent);

            foreach (var card in cards)
            {
                card.transform.SetParent(patent);
                card.gameObject.SetActive(true);
            }
        }

        private void RemoveAllChild(Transform parent)
        {
            foreach(Transform child in parent) {
                child.gameObject.SetActive(false);
            }
        }
    }
}
