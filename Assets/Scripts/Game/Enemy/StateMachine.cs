using System;
using System.Collections;

using UnityEngine;

namespace PaperTank.Game.Enemy
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private StateBehaviour[] _states;

        private StateBehaviour _current;

        public virtual void Start()
        {
            StartCoroutine(RunStates());
        }

        private IEnumerator RunStates()
        {
            _current = _states[0];

            yield return StartCoroutine(_current.Execute());

            while (true)
            {
                Type nextState = _current.NextState ?? _states[0].GetType();

                foreach (var state in _states)
                {
                    if (state.GetType() != nextState) continue;

                    _current = state;
                    break;
                }

                yield return StartCoroutine(_current.Execute());
            }
        }
    }
}
