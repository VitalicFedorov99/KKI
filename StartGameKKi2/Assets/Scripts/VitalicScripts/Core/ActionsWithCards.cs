using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Random = System.Random;

using UnityEngine;

using CardGame.Field;
using CardGame.Cards.Model;
using CardGame.Cards.Base;
namespace CardGame.Core
{
    public class ActionsWithCards : MonoBehaviour
    {

        [SerializeField] private List<ModelCard> allModelCards;
        [SerializeField] private Sprite _hero;
        public List<ModelCard> AllModelCards => allModelCards;

        public List<Card> _inDeckCards = new List<Card>();
        public List<Card> _inBoardCards = new List<Card>();
        public List<Card> _inHandCards = new List<Card>();
        public List<Card> _inDumpCards = new List<Card>();


        private GeneratorGameBoard _generatorGameBoard;
        private Player _player;
        public void Setup(GeneratorGameBoard gameBoard)
        {
            _generatorGameBoard = gameBoard;
        }


        public void CardsCanDrag(bool flag) 
        {
            _inHandCards.ForEach(t => t.IsCanDrag = flag);
        }

        public void CardsCanAct() 
        {
            _inBoardCards.ForEach(t => t.GetUnit().SetStatus(Status.CanAct));
        }


     

        public void MoveCard(Card card, Transform position) 
        {
            card.transform.parent = position;
        }

        
     





        public void MoveCardToDumpFromHand(Card card)
        {
            if (_inHandCards.Contains(card))
            {
                _inHandCards.Remove(card);
                _inDumpCards.Add(card);
                card.gameObject.SetActive(false);
            }
        }

        public void MoveCardToDumpFromFoard(Card card) 
        {
            if (_inBoardCards.Contains(card))
            {
                _inBoardCards.Remove(card);
                _inDumpCards.Add(card);
                card.GetCardUnit().ResetCard();
                //card.gameObject.SetActive(false);
            }
        }


        public void MoveCardToBoardFromHand(Card card)
        {
            if (_inHandCards.Contains(card) && card.GetUnit().Price <= _player.Force)
            {
                card.GetCardUnit().OnGoInBoard(true);
                _inHandCards.Remove(card);
                _inBoardCards.Add(card);
                _player.SpendForce(card.GetUnit().Price);
            }

            DisableCards();

            _generatorGameBoard.Generate(Game.CurrentPlayer.GetActionsWithCards(), Game.Enemy.GetActionsWithCards());
        }

        public void MoveCardsFromBoardToDump(params Card[] cards)
        {
            foreach (var card in cards)
            {
                if (_inBoardCards.Contains(card))
                {
                    _inBoardCards.Remove(card);
                    _inDumpCards.Add(card);
                    card.GetCardUnit().ResetCard();
                    card.gameObject.SetActive(false);
                }
            }
            _generatorGameBoard.Generate(Game.CurrentPlayer.GetActionsWithCards(), Game.Enemy.GetActionsWithCards());
        }

       

        


        public void MoveCardsFromDeckToBoard(int count)
        {
            List<Card> unitCards = _inDeckCards.OfType<Card>().Take(count).ToList();
            unitCards.ForEach(card =>
            {
                _inDeckCards.Remove(card);
                _inBoardCards.Add(card);
                card.GetCardUnit().OnGoInBoard(false);
            });
            _generatorGameBoard.Generate(Game.CurrentPlayer.GetActionsWithCards(), Game.Enemy.GetActionsWithCards());
        }

        public void MoveCardsCardFromDumpToDeck(params Card[] cards)
        {
            foreach (var card in cards)
            {
                _inDumpCards.Remove(card);
                _inDeckCards.Add(card);
            }

            //_inDeckCards = GetRandomDeck(_inDeckCards);
        }

        public void AddCardsToHand()
        {
            int cardsToFullHand = 5 - _inHandCards.Count;
            if (_inDeckCards.Count < cardsToFullHand)
                MoveCardsCardFromDumpToDeck(new List<Card>(_inDumpCards).ToArray());

            _inDeckCards.Take(cardsToFullHand).ToList().ForEach(card =>
            {
                _inDeckCards.Remove(card);
                _inHandCards.Add(card);
            });
        }

        public void AddCardInList(List<Card> cards, Card card)
        {
            cards.Add(card);
        }
        public void RemoveCardInList(List<Card> cards, Card card)
        {
            cards.Remove(card);
        }

       

        public void CardDeckMix() 
        {

            // var lstResult = _inDeckCards.OrderBy(x => Guid.NewGuid().ToString()).ToList();
           
            List<Card> listMix = new List<Card>();
            int count = _inDeckCards.Count;
            
            for(int i = 0; i < count; i++) 
            {
                int rand = Random.Range(0, _inDeckCards.Count);
                var card = _inDeckCards[rand];
                _inDeckCards.Remove(card);
                listMix.Add(card);
            }
            _inDeckCards.Clear();
            foreach(var card in listMix) 
            {
                _inDeckCards.Add(card);
            }
        }


     

        public GeneratorGameBoard GetGeneratorGameBoard() 
        {
            return _generatorGameBoard;
        }

       /* public  List<Card> GetRandomDeck(List<Card> deck)
        {
           // Random random = new Random();
           // return deck.OrderBy(card => random.Next()).ToList();
        }
       */
        public void BlockControl()
        {
            _inHandCards.ForEach(card => card.IsCanDrag = false);
        }


        public void DisableCards()
        {
            _inHandCards.ForEach(card =>
            {
                if (card.GetUnit().Price > _player.Force)
                    card.IsCanDrag = false;
            });
        }
 

        public Player GetPlayer() 
        {
            return _player;
        }

        public Sprite GetSpriteHero() 
        {
            return _hero; 
        }

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

    }
}
