using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Game.Manager;
using Game.StateMachine;

namespace Game.Play.Player
{
    public class PlayerBase : MonoBehaviour
    {
        #region declarations
        public float speed = 3f;
        [SerializeField]
        private int _points = 0;

        [HideInInspector]
        public Action AddPointCallBack;

        [Header("Input keys")]
        public KeyCode inputUp = KeyCode.UpArrow;
        public KeyCode inputDown = KeyCode.DownArrow;

        [Header("interface")]
        public TextMeshProUGUI text;

        [HideInInspector]
        public string playerName = "";

        private bool _canMove = false;
        private Rigidbody2D _myRB;
        #endregion

        #region initialize
        private void Awake()
        {
            _myRB = GetComponent<Rigidbody2D>();
            AddPointCallBack = AddPoint;
            ResetPoints();
        }

        public void Initialize()
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
            ChangeCanMove(true);
        }
        #endregion

        #region Controller
        public void ResetPoints()
        {
            _points = 0;
            UpdateInterface();
        }

        public void ChangeCanMove(bool b)
        {
            _canMove = b;
        }

        public void AddPoint()
        {
            _points++;
            CheckPoints();
            UpdateInterface();
        }

        public void CheckPoints()
        {
            if(_points >= GameManager.MaxPoints)
            {
                GameManager.Instance.SetWinner(this);
                StateMachineBase.EndGame();
            }
        }

        private void UpdateInterface()
        {
            text.text = _points.ToString();
        }
        #endregion

        #region movement input
        void Update()
        {
            if (!_canMove) return;
            if (Input.GetKey(inputUp))
            {
                _myRB.MovePosition(transform.position + transform.up * speed * Time.deltaTime);
            }
            else if (Input.GetKey(inputDown))
            {
                _myRB.MovePosition(transform.position + transform.up * -speed * Time.deltaTime);
            }
        }
        #endregion

        #region look
        public void ChangeColor(Color color)
        {
            GetComponent<SpriteRenderer>().material.color = color;
        }
        #endregion
    }
}

