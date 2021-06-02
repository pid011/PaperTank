using System.Collections.Generic;
using System.Linq;

using PaperTank.Game.Enemy.Tank;
using PaperTank.Game.Player;
using PaperTank.Game.UI;
using PaperTank.Util;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperTank
{
    public sealed class StageManager : Singleton<StageManager>
    {
        [SerializeField] private GameObject _stageEndPanelPrefab;

        private PlayerController _player;
        private List<EnemyTank> _enemies;
        private Canvas _canvas;

        public static Scene currentScene { get; private set; }

        public static bool isStage { get; private set; }
        public static bool isGameEnd { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneLoaded += OnStageLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnStageLoaded;
        }

        private void Update()
        {
            if (!isStage) return;

            if (!isGameEnd && _enemies != null && !_enemies.Exists(e => e != null))
            {
                Debug.Log("Stage Clear!");
                StageClear();
            }
        }

        public static void GameOver()
        {
            if (!isStage || isGameEnd) return;

            CreateStageEndPanel("Game Over");
            isGameEnd = true;
        }

        public static void StageClear()
        {
            if (!isStage || isGameEnd) return;

            CreateStageEndPanel("Stage Clear!");
            isGameEnd = true;
        }

        private void OnStageLoaded(Scene scene, LoadSceneMode sceneMode)
        {
            isGameEnd = false;
            currentScene = scene;
            Debug.Log($"Loaded scene: {currentScene.name}");
            _player = null;
            _canvas = null;
            _enemies?.Clear();
            _enemies = null;

            var playerObject = GameObject.FindGameObjectWithTag("Player");

            // 씬에 플레이어 오브젝트가 없으면 스테이지 씬이 아님
            if (playerObject == null)
            {
                isStage = false;
                return;
            }

            _player = playerObject.GetComponent<PlayerController>();
            _canvas = GameObject.FindGameObjectWithTag("GameCanvas").GetComponent<Canvas>();

            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            _enemies = new List<EnemyTank>();
            if (enemies.Length > 0)
            {
                _enemies.AddRange(enemies.Select(o => o.GetComponent<EnemyTank>()));
            }

            isStage = true;
        }

        private static void CreateStageEndPanel(string title)
        {
            var endPanel = Instantiate(instance._stageEndPanelPrefab, instance._canvas.transform)
                .GetComponent<StageEndInteraction>();

            endPanel.SetTitle(title);
            endPanel.OpenPanel();
        }
    }
}
