using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private Text _playerNameText;
        [SerializeField] private Button _doneButton;

        private void Awake()
        {
            _doneButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
            _game.gameOver.AddListener(OnWin);
            gameObject.SetActive(false);
        }

        private void OnWin(Player winner)
        {
            gameObject.SetActive(true);
            _playerNameText.text = winner.name;
        }
    }
}
