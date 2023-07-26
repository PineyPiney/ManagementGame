using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using PineyPiney.Util;

namespace PineyPiney.Manage
{

    public class BuildingOutline : BuildingItem
    {

        bool placedStart = false;
        Vector2 startPoint;
        Vector2 endPoint;

        public NPC owner;
        float acc;

        // Start is called before the first frame update
        new virtual protected void Start()
        {
            acc = Builder.INSTANCE.snapAccuracy * 2;
        }

        // Update is called once per frame
        void Update()
        {
            var pos = GetMouse();
            endPoint = pos;

            if (placedStart)
            {
                transform.position = (startPoint + endPoint) / 2;
                transform.localScale = (Vector3)(endPoint - startPoint).Abs() + new Vector3(2 * acc, 2 * acc, 1f);
            }
            else
            {
                transform.position = endPoint;
            }
        }

        public Vector2 GetMouse()
        {
            return (Vector2)GetPlacementPosition(2 * acc, new(acc, acc)).Item1 - new Vector2(acc, acc);
        }

        public void SetStart()
        {
            startPoint = GetMouse();
            placedStart = true;
        }
    }
}