using System;
using System.Collections;

using UnityEngine;

namespace PaperTank.Game.Enemy
{
    public abstract class StateBehaviour : MonoBehaviour
    {
        private enum State
        {
            Enter, Execute, Exit
        }

        public bool Done { get; private set; }
        public Type NextState { get; protected set; }

        private Coroutine _currentCoroutine;
        private State _currentState;

        protected abstract IEnumerator OnEnter();
        protected abstract IEnumerator OnExecute();
        protected abstract IEnumerator OnExit();

        public IEnumerator Execute()
        {
            Done = false;

            _currentState = State.Enter;
            _currentCoroutine = StartCoroutine(OnEnter());
            yield return _currentCoroutine;

            _currentState = State.Execute;
            _currentCoroutine = StartCoroutine(OnExecute());
            yield return _currentCoroutine;

            _currentState = State.Exit;
            _currentCoroutine = StartCoroutine(OnExit());
            yield return _currentCoroutine;

            Done = true;
        }

        public IEnumerator StopState()
        {
            switch (_currentState)
            {
                // Enter나 Execute가 실행중일 경우
                case State.Enter:
                case State.Execute:
                    // 해당 코루틴과 Execute 모두 중지 후 OnExit 직접 실행
                    // Done 값을 직접 true로 변경
                    StopAllCoroutines();
                    yield return StartCoroutine(OnExit());
                    Done = true;
                    break;

                // Exit일 경우
                case State.Exit:
                    // 그대로 Done이 true로 변경 될 때까지 기다림
                    yield return new WaitUntil(() => Done);
                    break;
            }

            Debug.Log($"{GetType()}: Stopped");
        }
    }
}
