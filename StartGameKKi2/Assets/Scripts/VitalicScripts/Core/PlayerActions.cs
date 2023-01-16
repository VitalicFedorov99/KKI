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
        [SerializeField] private int _step;
        private ActionsWithCards _actionsWithCards;
        private int counter = 0;
        [SerializeField] private GameObject _enemyObj;
        public void StartMove()
        {
            _actionsWithCards.AddCardsToHand();
            _actionsWithCards._inBoardCards.ForEach(card => card.GetCardUnit().SetCanAct());

            if (Game.MoveNumber > 20)
            {
                _player.TakeDamage(1);
            }
            _step++;

            _player.Force = 0;
            _player.AddForce(2 * _step);
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
        }

        public void GoToFight()
        {
            _actionsWithCards._inBoardCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            _actionsWithCards._inDumpCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            Game.CurrentPlayer.GetActionsWithCards()._inBoardCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            Game.CurrentPlayer.GetActionsWithCards()._inDumpCards.ForEach(t => t.GetComponent<LayoutElement>().ignoreLayout = true);
            counter = 0;
            if (_actionsWithCards._inBoardCards.Count > 0)
                StartCoroutine(Fight());
        }

        public void SetEnemyObject(GameObject objEnemy)
        {
            _enemyObj = objEnemy;
        }


        public int GetStep()
        {
            return _step;
        }

        IEnumerator Fight()
        {

            Unit unit = _actionsWithCards._inBoardCards[counter].GetUnit();
            if (unit.GetEnemyPlayer() == null)
            {
                unit.SetEnemyPlayer(Game.CurrentPlayer);
            }



            if (unit.Status == Status.Attacker)
            {
                if (unit.GetEnemy() != null)
                {
                    Twinner._instance.Attack(unit.GetComponent<RectTransform>(), unit.GetEnemy().GetComponent<RectTransform>());
                    yield return new WaitForSeconds(2f);
                    unit.TakeDamage(unit.GetEnemy().Damage);
                    unit.GetEnemy().TakeDamage(unit.Damage);
                }
                else
                {
                    _enemyObj = Game.Generator.GetHero(unit.GetEnemyPlayer().PlayerType);
                    unit.GetEnemyPlayer().TakeDamage(unit.Damage);
                    Twinner._instance.AttackInHero(unit.GetComponent<RectTransform>(), _enemyObj.GetComponent<RectTransform>());
                    yield return new WaitForSeconds(2f);

                }
                _actionsWithCards._inBoardCards[counter].ActionFight?.Invoke();
                _actionsWithCards._inBoardCards[counter].GetComponent<CardUnit>().Attack();
                if (unit.GetEnemy() != null)
                {
                    unit.GetEnemy().SetEnemy(null);
                }
                unit.SetEnemy(null);
            }

            counter++;
            if (counter < _actionsWithCards._inBoardCards.Count)
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