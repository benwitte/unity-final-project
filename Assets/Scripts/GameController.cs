using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiRun
{
    public class GameController : MonoBehaviour
    {
        public CameraController cameraController;
        public PlayerController playerController;
        public GameObject ball;

        [Header("GameBegin UI")]
        public Canvas gameBeginCanvas;

        [Header("UI")]
        public Canvas gameUICanvas;
        public TMPro.TextMeshProUGUI scoreLabel;

        [Header("GameOver UI")]
        public Canvas gameOverCanvas;
        public TMPro.TextMeshProUGUI winLoseLabel;
        public TMPro.TextMeshProUGUI finalScoreLabel;

        public bool IsPlaying;
        public int Score { get; private set; }

        // Not the best way of doing it but whatever lol
        public static GameController GetCurrentController()
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        public void Start()
        {
            IsPlaying = false;
        }

        public void StartGame()
        {
            gameBeginCanvas.enabled = false;
            IsPlaying = true;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }

        void Update()
        {
            if (!IsPlaying) return;

            Score++;
            scoreLabel.text = Score.ToString();

            if (Score % 1500 == 0 && Score < 25000)
            {
                playerController.worldMoveSpeed *= 1.1f;
                playerController.moveSpeed *= 1.1f;
            }
        }

        public void Penalize()
        {
            ball.transform.Translate(new Vector3(0, 0, 2));
            if (ball.transform.position.z >= playerController.transform.position.z)
                Lose();
        }

        public void Lose()
        {
            IsPlaying = false;
            gameOverCanvas.enabled = true;
        }
    }
}