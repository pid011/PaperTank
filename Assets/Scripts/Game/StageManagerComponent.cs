using System.Collections.Generic;
using System.Linq;
using PaperTank.Game.Enemy.Tank;
using PaperTank.Game.Player;
using PaperTank.Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PaperTank.Game
{
    public sealed class StageManagerComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _stageEndPanelPrefab;

        private PlayerController _player;
        private List<EnemyTank> _enemies;
        private Canvas _canvas;

        public Scene currentScene { get; private set; }
        public bool isStage { get; set; }
        public bool isGameEnd { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnStageLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnStageLoaded;
        }

        private void Update()
        {
            if (!isStage) return;

            // stage clear
            if (isGameEnd || _enemies?.Exists(e => e != null) != false) return;

            Debug.Log("Stage Clear!");
            StageManager.StageClear();
        }

        public void CreateStageEndPanel(string title)
        {
            var endPanel = Instantiate(_stageEndPanelPrefab, _canvas.transform)
                .GetComponent<StageEndInteraction>();

            endPanel.SetTitle(title);
            endPanel.OpenPanel();
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
    }
}
