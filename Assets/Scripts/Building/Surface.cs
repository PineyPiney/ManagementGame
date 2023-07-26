using System;
using UnityEngine;

using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class Surface: MonoBehaviour
    {
        // Relative position of surface
        public float y;
        public float left;
        public float right;
        public float z = -0.1f;

        public bool active = false;

        public float worldY { get { return transform.position.y + (transform.lossyScale.y * y); } }
        public float worldLeft { get { return transform.position.x + (transform.lossyScale.x * left); } }
        public float worldRight { get { return transform.position.x + (transform.lossyScale.x * right); } }

        private void Start()
        {
            var g = Instantiate(Prefabs.Get<GameObject>("Prefabs/UI/Line"));
            g.transform.SetParent(transform);
            g.transform.localPosition = new Vector3((left + right) / 2, y, 0);
            g.transform.localScale = new Vector3(right - left, 0.02f / g.transform.parent.lossyScale.y, 1f);

        }

        private void Update()
        {
            
        }

        public void SetValues(float left, float right, float y, bool active = false)
        {
            this.left = left;
            this.right = right;
            this.y = y;
            this.active = active;
        }

        public float GetDistance(PlaceOnSurface i)
        {
            var(dl, dr, dy) = GetOffsets(i);
            if (dl < 0 && dr > 0) return MathF.Abs(dy);

            return MathF.Sqrt(MathF.Pow(MathF.Min(MathF.Abs(dl), MathF.Abs(dr)), 2) + (dy * dy));
        }

        public Vector3 GetPlacement(PlaceOnSurface i)
        {
            var (dl, dr, dy) = GetOffsets(i);
            float x = dl < 0 && dr > 0 ? 0 : (Mathf.Abs(dl) < Mathf.Abs(dr) ? dl : dr);
            return new Vector3(x, dy, transform.position.z + z);
        }

        //
        //  Summary:
        //      Gets the vector from the mouse in world space to the placement position of i
        public (float, float, float) GetOffsets(PlaceOnSurface i)
        {
            Vector2 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float dl = worldLeft - (m.x + i.Left());
            float dr = worldRight - (m.x + i.Right());
            float dy = worldY - (m.y + i.Y());
            return (dl, dr, dy);
        }
    }
}
