using UnityEngine;

namespace InfiRun
{
    public class GameController : MonoBehaviour
    {
        public CameraController cameraController;
        public PlayerController playerController;
        public WorldController worldController;
        public TMPro.TextMeshProUGUI scoreLabel;
        public GameObject ball;

        public bool IsPlaying;
        public int Score { get; private set; }

        // Not the best way of doing it but whatever lol
        public static GameController GetCurrentController()
        {
            return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        }

        public void Start()
        {
            IsPlaying = true;
        }

        void Update()
        {
            if (!IsPlaying) return;

            Score++;
            scoreLabel.text = Score.ToString();
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
        }
    }
}