using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfiRun
{
    public class GameController : MonoBehaviour
    {
        public CameraController cameraController;
        public PlayerController playerController;
        public GameObject player;
        public GameObject ball;
        public AudioSource bgmSource;

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
            gameBeginCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            gameUICanvas.enabled = false;
        }

        public void StartGame()
        {
            gameBeginCanvas.enabled = false;
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

            gameUICanvas.enabled = true;
            IsPlaying = true;
            bgmSource.Play();
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
            if (ball.transform.position.z + 2 >= player.transform.position.z)
                EndGame();
        }

        public void Lose()
        {
            EndGame(true);
        }

        public void EndGame(bool forceLoss = false)
        {
            IsPlaying = false;
            bgmSource.Stop();

            gameUICanvas.enabled = false;
            gameOverCanvas.enabled = true;

            bool won = !forceLoss && Score >= (100 * 1000);
            winLoseLabel.text = won ? "You win!" : "You lose!";
            finalScoreLabel.text = Score.ToString();
        }
    }
}