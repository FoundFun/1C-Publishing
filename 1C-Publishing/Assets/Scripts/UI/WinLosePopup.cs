using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class WinLosePopup : MonoBehaviour
    {
        [SerializeField] private GameObject _winBord;
        [SerializeField] private GameObject _loseBord;
        [SerializeField] private Button _restartButton;
        
        public bool IsLose { get; private set; }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestart);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestart);
        }

        public void OpenLose()
        {
            gameObject.SetActive(true);
            
            _winBord.SetActive(false);
            _loseBord.SetActive(true);
            
            IsLose = true;
        }

        public void OpenWin()
        {
            gameObject.SetActive(true);
            
            _loseBord.SetActive(false);
            _winBord.SetActive(true);

            IsLose = false;
        }

        private void OnRestart()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }
}