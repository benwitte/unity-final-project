using UnityEngine;

namespace InfiRun
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public float trailDistance = 5.0f;
        public float heightOffset = 3.0f;

        void Update()
        {
            var followPos = target.position - (target.forward * trailDistance);
            followPos.y += heightOffset;

            transform.position = followPos;
            transform.LookAt(target);
        }
    }
}