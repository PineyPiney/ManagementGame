using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PineyPiney.Manage
{
    public class Grid : MonoBehaviour
    {

        new Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            camera = GetComponentInParent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            float height = camera.orthographicSize * 2;
            transform.localScale = new Vector3(height * camera.aspect, height);
        }
    }
}