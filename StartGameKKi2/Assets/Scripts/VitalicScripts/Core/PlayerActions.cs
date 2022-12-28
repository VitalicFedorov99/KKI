using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardGame.Cards.Base;
using CardGame.Cards.Model;
using UnityEngine.UI;
namespace CardGame.Core
{
    public class PlayerActions : MonoBehaviour
    {

        private Player _player;
        private ActionsWithCards _actionsWithCards;
        private int counter = 0;
        private GameObject _enemyObj;
        public void StartMove()
        {
            _actionsWithCards.AddCardsToHand();
            _actionsWithCards._inBoardCards.ForEach(card => card.GetCardUnit().SetCanAct());

            if (Game.MoveNumber > 20)
            {
                _player.TakeDamage(1);
            }

            int addForceCount = Game.MoveNumber / 2 + Game.MoveNumber % 2;
            if (addForceCount > 6)
                addForceCount = 6;
            _player.Force += Game.MoveNumber / 2 + Game.MoveNumber % 2;
            if (_player.Force > 10)
                _player.Force = 10;


            // GoToDefendPhase(Game.Enemy);
        }
        public void GoToDefendPhase()
        {
            _player.CurrentPhase = Phase.Defend;

            _actionsWithCards._inHandCards.ForEach(card =>
            {
                if (card.GetTypeCard() == TypeCard.Effect)
                    card.IsCanDrag = true;
                else card.IsCanDrag = false;
            });

            _actionsWithCards._inBoardCards.ForEach(card => card.GetCardUnit().SetCanAct());
        }

        public void GoToPlayCardPhase()
        {
            _player.CurrentPhase = Phase.Play;
            /*_actionsWithCards._inBoardCards.ForEach(card =>
            {
                card.IsCanDrag = false;
                if (card.GetUnit().Status != Status.FirstTurn && card.GetUnit().Status != Status.Defender)
                    card.GetCardUnit().SetCannotAct();
            });
            _actionsWithCards._inHandCards.ForEach(card => card.IsCanDrag = true);
            _actionsWithCards.GetGeneratorGameBoard().Generate(Game.CurrentPlayer.GetActionsWithCards(), Game.Enemy.GetActionsWithCards());
            _actionsWithCards.DisableCards();*/
        }

        public void GoToAttackPhase()
        {
            _player.CurrentPhase = Phase.Attack;
            _actionsWithCards._inBoardCards.ForEach(card =>
            {
                card.IsCanDrag = false;
            }
            );
            _actionsWithCards._inHandCards.ForEach(card => card.IsCanDrag = false);
            /*_actionsWithCards._inBoardCards.ForEach(card =>
              {
                  card.IsCanDrag = true;
                  if (card.GetUnit().Status != Status.FirstTurn && card.GetUnit().Status != Status.Defender)
                      card.GetCardUnit().SetCannotAct();
              });
            _actionsWithCards._inHandCards.ForEach(card => card.IsCanDrag = false);*/
        }

        public void GoToFight()
        {
            _actionsWithCards._inBoardCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            _actionsWithCards._inDumpCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            Game.CurrentPlayer.GetActionsWithCards()._inBoardCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            Game.CurrentPlayer.GetActionsWithCards()._inDumpCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            counter = 0;
            StartCoroutine(Fight());
        }

        public void SetEnemyObject(GameObject objEnemy) 
        {
            _enemyObj = objEnemy;
        }


        IEnumerator Fight()
        {
            Unit unit = _actionsWithCards._inBoardCards[counter].GetUnit();
            if (unit.Status == Status.Attacker)
            {
                if (unit.GetEnemy() != null)
                {
                    Twinner._instance.Attack(unit.GetComponent<RectTransform>(), unit.GetEnemy().GetComponent<RectTransform>());
                    Debug.LogError("Пошел бой");
                    yield return new WaitForSeconds(2f);
                    unit.TakeDamage(unit.GetEnemy().Damage);
                    unit.GetEnemy().TakeDamage(unit.Damage);
                }
                else 
                {
                    Twinner._instance.AttackInHero(unit.GetComponent<RectTransform>(), _enemyObj.GetComponent<RectTransform>());
                    Debug.LogError("Пошел бой");
                    yield return new WaitForSeconds(2f);

                }

                _actionsWithCards._inBoardCards[counter].GetComponent<CardUnit>().Attack();
            }
            counter++;
            if(counter < _actionsWithCards._inBoardCards.Count) 
            {
                StartCoroutine(Fight());
            }
            else 
            {
                _actionsWithCards._inBoardCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = false);
                _actionsWithCards._inDumpCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = false);
                Game.CurrentPlayer.GetActionsWithCards()._inBoardCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = false);
                Game.CurrentPlayer.GetActionsWithCards()._inDumpCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = false);
            }

        }
        private void Awake()
        {
            _actionsWithCards = GetComponent<ActionsWithCards>();
            _player = GetComponent<Player>();
        }
    }
}