using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PineyPiney.Util
{
    public static class Tiling
    {
        public static Mesh CreateTileMesh(Vector2 size, Vector2 scale)
        {
            Mesh mesh = new Mesh
            {
                name = "Tiled"
            };
            var (x, y) = Vector2.one / 2; // size / 2;
            mesh.vertices = new Vector3[] { new(-x, -y, 0), new(-x, y, 0), new(x, y, 0), new(x, -y, 0f) }; // mesh.uv.Select(x => x.AddDimension()).ToArray();


            mesh.uv = new[] { Vector2.zero, new(0f, size.y * scale.y), size * scale, new(size.x * scale.x, 0f) };
            mesh.triangles = new int[] { 0, 1, 2, 2, 3, 0 };
            
            return mesh;
        }
    }
}
