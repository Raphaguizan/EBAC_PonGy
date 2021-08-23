using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Manager;

namespace Game.StateMachine
{
    public class StateBase
    {
        public virtual void OnStateEnter(object o = null)
        {
            //Debug.Log("Entrou no estado");
        }
        public virtual void OnStateStay()
        {
            //Debug.Log("Está no estado");
        }
        public virtual void OnStateExit()
        {
            //Debug.Log("Saiu no estado");
        }
    }
    public class StatePlaying : StateBase
    {
        public override void OnStateEnter(object o)
        {
            //Debug.Log("Entrou no estado PLAYING");
            GameManager.Instance.StartGame((int)o);
        }

        public override void OnStateStay()
        {
            //Debug.Log("Está no estado PLAYING");
        }
        public override void OnStateExit()
        {
            //Debug.Log("Saiu no estado PLAYING");
        }
    }

    public class StateMenu : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
           // Debug.Log("Entrou no estado MENU");
            GameManager.Instance.ResetGame();
            GameManager.Instance.ActiveMenuInterface(true);
        }
        public override void OnStateStay()
        {
            //Debug.Log("Está no estado MENU");
        }
        public override void OnStateExit()
        {
            //Debug.Log("Saiu no estado MENU");
            CustomizationManager.Configure();
            GameManager.Instance.ActiveMenuInterface(false);
        }
    }

    public class StateEndGame : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            //Debug.Log("Entrou no estado END");
            GameManager.Instance.ActiveEndGameInterface(true);
            GameManager.Instance.EndGame();
            
        }
        public override void OnStateStay()
        {
            //Debug.Log("Está no estado END");
        }
        public override void OnStateExit()
        {
            //Debug.Log("Saiu no estado END");
            GameManager.Instance.ActiveEndGameInterface(false);
        }
    }

    public class StateHighScore : StateBase
    {
        public override void OnStateEnter(object o = null)
        {
            //Debug.Log("Entrou no estado HIGH");
            GameManager.Instance.ActiveHighScoreInterface(true);
            GameManager.Instance.CalculateHighScore();
        }
        public override void OnStateStay()
        {
            //Debug.Log("Está no estado HIGH");
        }
        public override void OnStateExit()
        {
            //Debug.Log("Saiu no estado HIGH");
            GameManager.Instance.ExitHighScore();
            GameManager.Instance.ActiveHighScoreInterface(false);
        }
    }
}

