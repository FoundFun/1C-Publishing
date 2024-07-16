using System;
using TMPro;
using UnityEngine;
using VContainer;

namespace UI
{
    public class GameStatistic : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _enemyCountText;
        [SerializeField] private TextMeshProUGUI _playerHealthValueText;

        private WinLosePopup _winLosePopup;
        
        public bool IsGameOver { get; private set; }
        
        public event Action PlayerDamaged;

        [Inject]
        public void Construct(WinLosePopup winLosePopup)
        {
            _winLosePopup = winLosePopup;
        }

        private void Start()
        {
            IsGameOver = false;
        }

        public void UpdatePlayerHeath(int currentHealth)
        {
            if (IsGameOver)
            {
                return;
            }
            
            _playerHealthValueText.text = $"{currentHealth}";
            
            if (currentHealth <= 0)
            {
                IsGameOver = true;
                _winLosePopup.OpenLose();
            }
        }

        public void SetEnemyScore(int targetSpawnCount)
        {
            _enemyCountText.text = $"{targetSpawnCount}";
        }

        public void RemovePlayerScore()
        {
            PlayerDamaged?.Invoke();
        }
    }
}