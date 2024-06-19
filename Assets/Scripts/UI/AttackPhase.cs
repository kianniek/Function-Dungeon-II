using UnityEngine;
using UnityEngine.Events;

namespace KaijuUI
{
    public class AttackPhase : MonoBehaviour
    {
        private int _attackCount;
        public UnityEvent FirstAttack;
        public UnityEvent SecondAttack;
        public UnityEvent CounterResetAttack;

        /// <summary>
        /// Launches and event based on the attack amount and increases the counter
        /// </summary>
        public void LaunchAttack()
        {
            switch (_attackCount)
            {
                case 0:
                    FirstAttack.Invoke();
                    break;
                case 1:
                    SecondAttack.Invoke();
                    break;
            }
            _attackCount++;
        }

        /// <summary>
        /// Resets the counter to zero and launches an event
        /// </summary>
        public void ResetAttack()
        {
            _attackCount = 0;
            CounterResetAttack.Invoke();
        }
    }
}
