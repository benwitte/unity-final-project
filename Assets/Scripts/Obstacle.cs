using UnityEngine;

namespace InfiRun
{
    public class Obstacle : MonoBehaviour
    {
        GameController gameController;

        // Start is called before the first frame update
        void Start()
        {
            gameController = GameController.GetCurrentController();
        }

        // Update is called once per frame
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Player") return;
            gameController.Penalize();
            Destroy(this.gameObject);
        }
    }
}