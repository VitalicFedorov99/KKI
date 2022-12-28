using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class NextMovePanel : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private Button _okButton;

        private void Awake()
        {
            _game.moveEnded.AddListener(() =>
            {
                gameObject.SetActive(true);
            });
            _okButton.onClick.AddListener(MoveNext);
        }

        private void MoveNext()
        {
            _game.NextMove();
            gameObject.SetActive(false);
        }
    }
}
