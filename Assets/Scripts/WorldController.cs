using System.Collections.Generic;
using UnityEngine;

namespace InfiRun
{
    public class WorldController : MonoBehaviour
    {
        public Vector3 moveSpeed;

        public void Start()
        {
            moveSpeed = Vector3.back * 10;
        }

        public void Update()
        {
            transform.Translate(moveSpeed * Time.deltaTime);
        }
    }
}