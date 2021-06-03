using System;
using System.Collections;
using UnityEngine;

namespace PaperTank.Game.Enemy
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateBehaviour[] _states;

        public virtual void Start()
        {
            StartCoroutine(RunStates());
        }

        private IEnumerator RunStates()
        {
            var current = _states[0];

            yield return StartCoroutine(current.Execute());

            while (true)
            {
                Type nextState = current.nextState ?? _states[0].GetType();

                foreach (var state in _states)
                {
                    if (state.GetType() != nextState) continue;

                    current = state;
                    break;
                }

                yield return StartCoroutine(current.Execute());
            }
        }
    }
}
