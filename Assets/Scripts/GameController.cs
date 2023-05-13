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

        [Header("Audio Sources")]
        public AudioSource bgmSource;
        public AudioSource ballAudioSource;
        public AudioSource sfxAudioSource;

        [Header("SFX Clips")]
        public AudioClip hitClip;
        public AudioClip jumpClip;
        public AudioClip crashClip;

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
            ballAudioSource.Play();
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
                ball.GetComponent<Animator>().speed *= 1.1f;
            }
        }

        System.Collections.IEnumerator AnimateBallRolling()
        {
            const float lerpDuration = 0.5f;
            float timeElapsed = 0;

            var originalPos = ball.transform.localPosition;

            while (timeElapsed < lerpDuration)
            {
                ball.transform.localPosition = originalPos + Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 2), timeElapsed / lerpDuration);

                timeElapsed += Time.deltaTime;
                yield return null;
            }
            ball.transform.localPosition = originalPos + Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 2), 1);

            if (ball.transform.position.z + 2 >= player.transform.position.z)
            {
                PlaySFX(SfxKind.Crash);
                EndGame();
            }

            yield return null;
        }

        public void Penalize()
        {
            StartCoroutine(AnimateBallRolling());
        }

        public void Lose()
        {
            EndGame(true);
        }

        public void EndGame(bool forceLoss = false)
        {
            IsPlaying = false;
            bgmSource.Stop();
            ballAudioSource.Stop();

            gameUICanvas.enabled = false;
            gameOverCanvas.enabled = true;

            bool won = !forceLoss && Score >= (100 * 1000);
            winLoseLabel.text = won ? "You win!" : "You lose!";
            finalScoreLabel.text = Score.ToString();
        }

        public enum SfxKind
        {
            WallHit, Jump, Crash
        }

        public void PlaySFX(SfxKind kind)
        {
            switch (kind)
            {
                case SfxKind.WallHit: sfxAudioSource.PlayOneShot(hitClip); break;
                case SfxKind.Jump: sfxAudioSource.PlayOneShot(jumpClip); break;
                case SfxKind.Crash: sfxAudioSource.PlayOneShot(crashClip); break;
            }
        }
    }
}