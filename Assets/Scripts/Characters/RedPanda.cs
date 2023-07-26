using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PineyPiney.Manage
{
    public class RedPanda : NPC
    {

        float target = 7f;
        public float speed = 2f;

        // Start is called before the first frame update
        override protected void Start()
        {
            base.Start();
            body.velocity = new Vector3(speed, 0, 0);
        }

        // Update is called once per frame
        override protected void Update()
        {
            base.Update();

            if (transform.position.x > 7) target = -7;
            else if (transform.position.x < -7) target = 7;

            if (transform.position.x > target) body.velocity = new Vector3(-speed, 0, 0);
            else body.velocity = new Vector3(speed, 0, 0);
        }
    }
}