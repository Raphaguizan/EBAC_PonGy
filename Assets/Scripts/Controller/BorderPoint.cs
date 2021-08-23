using UnityEngine;
using Game.Play.Player;
using Game.StateMachine;

namespace Game.Play.Border
{
    public class BorderPoint : MonoBehaviour
    {
        public PlayerBase player;
        public string ballTag = "Ball";

        public int side = 1;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(ballTag))
            {
                player.AddPointCallBack?.Invoke();
                StateMachineBase.StartGame(side);
            }
        }
    }

}
