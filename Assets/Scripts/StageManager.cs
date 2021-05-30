using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperTank
{
    public sealed class StageManager : Singleton<StageManager>
    {
        public static bool IsStage { get; private set; }
        public static Scene CurrentScene { get; private set; }

        private PlayerController _player;
        private readonly List<EnemyTank> _enemies = new List<EnemyTank>();

        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneLoaded += OnStageLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnStageLoaded;
        }

        private void OnStageLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            CurrentScene = scene;
            Debug.Log($"Loaded scene: {CurrentScene.name}");
            _player = null;
            _enemies.Clear();

            var playerObject = GameObject.FindGameObjectWithTag("Player");

            // 씬에 플레이어 오브젝트가 없으면 스테이지 씬이 아님
            if (playerObject == null)
            {
                IsStage = false;
                return;
            }

            _player = playerObject.GetComponent<PlayerController>();

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 0)
            {
                _enemies.AddRange(enemies.Select(o => o.GetComponent<EnemyTank>()));
            }

            IsStage = true;
        }
    }
}
