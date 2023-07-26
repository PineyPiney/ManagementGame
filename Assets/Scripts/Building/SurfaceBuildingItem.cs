using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using PineyPiney.Util;

namespace PineyPiney.Manage
{

    abstract public class SurfaceBuildingItem : BuildingItem, PlaceOnSurface
    {
        SpriteRenderer sr;

        private void Awake()
        {
            TryGetComponent(out sr);
        }

        public float Y()
        {
            return -sr.bounds.extents.y;
        }

        public float Left()
        {
            return -sr.bounds.extents.x;
        }

        public float Right()
        {
            return sr.bounds.extents.x;
        }

        override public (Vector3, Transform) GetPlacementPosition(float snapAccuracy, Vector2 offset)
        {
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            House house = House.GetNearest(mouse);
            GameObject side = Builder.INSTANCE.isInside ? house.inside : house.outside;

            Surface s = GetNearestOf(side.GetComponentsInChildren<Surface>().Where(s => s.active).ToArray());
            Vector3 r = (Vector3)mouse + s.GetPlacement(this);
            return (r, s.transform);
        }

        public Surface GetNearestOf(Surface[] surfaces)
        {
            var (nearest, distance) = (surfaces[0], float.MaxValue);
            foreach (Surface surface in surfaces)
            {
                float newDist = surface.GetDistance(this);
                if (newDist < distance)
                {
                    (nearest, distance) = (surface, newDist);
                }
            }
            return nearest;
        }
    }
}