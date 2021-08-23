using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Game.Play.Ball;
using Game.Play.Player;
using Game.StateMachine;

namespace Game.Manager
{
    public class GameManager : MonoBehaviour
    {
        //test
        #region declarations
        [SerializeField]
        private int maxPoints;
        public static int MaxPoints => Instance.maxPoints;

        public HighScoreManager highScore;

        [SerializeField]
        private BallBase _ballBase;
        [SerializeField]
        private List<PlayerBase> _Players;

        [Header("interfaces")]
        [SerializeField]
        private GameObject _menuInterface;

        [SerializeField]
        private GameObject _endGameInterface;
        public TextMeshProUGUI nameTextBox;
        [SerializeField]
        private GameObject _highScoreInterface;
        

        private PlayerBase _winner = null;
        #endregion

        #region static
        public static GameManager Instance;
        private void Awake()
        {
            if (Instance) Destroy(this);
            Instance = this;
        }
        #endregion

        #region State Commands
        public void StartGame(int side)
        {
            _winner = null;
            _ballBase.ResetBallPosition(side);
            foreach (PlayerBase pb in _Players)
            {
                pb.Initialize();
            }
        }

        public void EndGame()
        {
            _ballBase.ChangeCanMove(false);
            nameTextBox.text = "O jogador: "+_winner.playerName;
            foreach (PlayerBase pb in _Players)
            {
                pb.ChangeCanMove(false);
            }
            SaveGame();
            StartCoroutine(WaitToReturnMenu(3f));
        }
        IEnumerator WaitToReturnMenu(float time)
        {
            yield return new WaitForSeconds(time);
            StateMachineBase.OpenMenu();
        }

        public void ResetGame()
        {
            _ballBase.ChangeCanMove(false);
            foreach (PlayerBase pb in _Players)
            {
                pb.ResetPoints();
                pb.ChangeCanMove(false);
            }
        }

        public void SetWinner(PlayerBase player)
        {
            _winner = player;
        }
        #endregion

        #region save game

        // A melhor forma de fazer esse tipo de save é com serialização
        // porém no módulo foi explicado com PlayerPrefs
        public void SaveGame()
        {
            if (_winner == null || _winner.playerName.Equals("")) return;
            int qty = PlayerPrefs.GetInt("qty", 0);

            for (int i = 0; i < qty; i++)
            {
                if(PlayerPrefs.GetString("n"+i) == _winner.playerName)
                {
                    int point = PlayerPrefs.GetInt("p"+i) + 1;
                    PlayerPrefs.SetInt("p"+i, point);
                    return;
                }
            }
            
            PlayerPrefs.SetString("n" + qty, _winner.playerName);
            PlayerPrefs.SetInt("p" + qty, 1);
            qty++;
            PlayerPrefs.SetInt("qty", qty);
        }

        public void ResetSave()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
        }
        #endregion

        #region highscore
        public void CalculateHighScore()
        {
            highScore.Calcule();
        }

        public void ExitHighScore()
        {
            highScore.DestroyPrefabList();
        }
        #endregion

        #region toggle interface

        public void ActiveMenuInterface(bool b)
        {
            _menuInterface.SetActive(b);
        }
        public void ActiveEndGameInterface(bool b)
        {
            _endGameInterface.SetActive(b);
        }

        public void ActiveHighScoreInterface(bool b)
        {
            _highScoreInterface.SetActive(b);
        }

        public void QuitGame() => Application.Quit();

        #endregion
    }
}
