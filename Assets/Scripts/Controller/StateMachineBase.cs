using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.StateMachine
{
    public enum States
    {
        MENU,
        PLAYING,
        END_GAME,
        HIGHSCORE
    }

    public class StateMachineBase : MonoBehaviour
    {

        #region declaration
       

        public Dictionary<States, StateBase> dictionaryState;

        private StateBase _currentState;
        private static StateMachineBase Instance;

        #endregion

        #region states controller
        private void Awake()
        {
            if (Instance) Destroy(this);
            Instance = this;

            dictionaryState = new Dictionary<States, StateBase>();
            dictionaryState.Add(States.MENU, new StateMenu());
            dictionaryState.Add(States.PLAYING, new StatePlaying());
            dictionaryState.Add(States.END_GAME, new StateEndGame());
            dictionaryState.Add(States.HIGHSCORE, new StateHighScore());

            OpenMenu();
        }

        private void SwitchState(States state, object o = null)
        {
            if (_currentState != null) _currentState.OnStateExit();
            _currentState = dictionaryState[state];
            _currentState.OnStateEnter(o);
        }

        private void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
        #endregion

        #region Change State
        public static void OpenMenu()
        {
            Instance.SwitchState(States.MENU);
        }

        public static void StartGame(int side = 1)
        {
            if (Instance._currentState.GetType() == new StateEndGame().GetType()) return;
            Instance.SwitchState(States.PLAYING, side);
        }

        public static void EndGame()
        {
            Instance.SwitchState(States.END_GAME);
        }

        public static void HighScoreOpen()
        {
            Instance.SwitchState(States.HIGHSCORE);
        }
        #endregion
    }

}
