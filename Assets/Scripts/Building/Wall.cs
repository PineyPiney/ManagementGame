using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PineyPiney.Util;

namespace PineyPiney.Manage
{
    public class Wall : BuildingItem
    {

        // Start is called before the first frame update
        override protected void Start()
        {
            base.Start();
            TryGetComponent(out Renderer r);
            r.material.mainTexture = Resources.Load<Texture2D>($"Textures/Buildings/Walls/{transform.parent.gameObject.name}/wooden");
            r.material.color = Color.white;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetParent(GameObject o)
        {

        }

        public void SetSize(Vector2 size)
        {
            TryGetComponent(out MeshFilter meshFilter);
            var scale = 1f / Builder.INSTANCE.snapAccuracy;
            meshFilter.mesh = Tiling.CreateTileMesh(size, new(scale, scale));
        }
    }
}