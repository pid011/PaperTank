using System.Collections.Generic;
using System.Linq;

using PaperTank.Game.Enemy.Tank;
using PaperTank.Game.Player;
using PaperTank.Util;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperTank
{
    public sealed class StageManager : Singleton<StageManager>
    {
        public static bool isStage { get; private set; }
        public static Scene currentScene { get; private set; }

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
            currentScene = scene;
            Debug.Log($"Loaded scene: {currentScene.name}");
            _player = null;
            _enemies.Clear();

            var playerObject = GameObject.FindGameObjectWithTag("Player");

            // 씬에 플레이어 오브젝트가 없으면 스테이지 씬이 아님
            if (playerObject == null)
            {
                isStage = false;
                return;
            }

            _player = playerObject.GetComponent<PlayerController>();

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 0)
            {
                _enemies.AddRange(enemies.Select(o => o.GetComponent<EnemyTank>()));
            }

            isStage = true;
        }
    }
}
