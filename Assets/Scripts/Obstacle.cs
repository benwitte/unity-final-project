using UnityEngine;

namespace InfiRun
{
    public class Obstacle : MonoBehaviour
    {
        GameController gameController;

        void Start()
        {
            gameController = GameController.GetCurrentController();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Player") return;
            gameController.PlaySFX(GameController.SfxKind.WallHit);
            gameController.Penalize();
            Destroy(this.gameObject);
        }
    }
}