using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Play.Ball
{
    public class BallBase : MonoBehaviour
    {
        #region declarations 
        public Vector2 RandomSpeedX = new Vector2(2, 3);
        public Vector2 RandomSpeedY = new Vector2(2, 3);

        public string playerTag = "Player";

        [Header("Speed Modifier")]
        [Tooltip("Porcentagem de aceleração do eixo x a cada toque em player")]
        [Range(0, 100)]
        public float xModifier = 10f;
        [Tooltip("quantidade de modificação de velocidade no eixo y")]
        public float yModifier = 5f;

        private bool _canMove = false;
        private Vector3 _currentSpeed;
        #endregion

        #region controller
        void Update()
        {
            if (!_canMove) return;
            transform.Translate(_currentSpeed * Time.deltaTime);
        }

        public void ChangeCanMove(bool b)
        {
            _canMove = b;
        }

        public void ResetBallPosition(int side)
        {
            ChangeCanMove(false);
            transform.position = Vector3.zero;
            _currentSpeed = new Vector2(Random.Range(RandomSpeedX.x, RandomSpeedX.y), Random.Range(RandomSpeedY.x, RandomSpeedY.y));
            _currentSpeed.x *= side;

            StartCoroutine(WaitSecondsToBegin(2f));
        }
        IEnumerator WaitSecondsToBegin(float time)
        {
            yield return new WaitForSeconds(time);
            ChangeCanMove(true);
        }
        #endregion

        #region collision
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(playerTag))
            {
                _currentSpeed.x *= -(xModifier / 100 + 1);

                float playerCenterDistance = transform.position.y - collision.transform.position.y;
                _currentSpeed.y += playerCenterDistance * yModifier;
            }
            else
            {
                _currentSpeed.y *= -1;
            }
        }
        #endregion
    }
}

