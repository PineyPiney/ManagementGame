using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace PineyPiney.Util
{
    public static class Extensions
    {

        public static float RoundToNearest(this float d, float step)
        {
            float rem = d % step;
            float ret = d - rem;
            if (rem >= step / 2) ret += step;
            else if (rem <= -step / 2) ret -= step;
            return ret;
        }

        public static int Sign(this float f)
        {
            return f == 0 ? 0 : f > 0 ? 1 : -1;
        }

        public static Vector2 Abs(this Vector2 v)
        {
            return new(Mathf.Abs(v.x), Mathf.Abs(v.y));
        }

        public static T GetOrDefault<T>(this Array a, int index, T def = default)
        {
            if (a.Length > index) return (T)a.GetValue(index);
            else return def;
        }

        public static void ForEachIndexed<T>(this List<T> list, Action<T, int> action)
        {
            int i = 0;
            foreach (T item in list) action(item, i++);
        }

        public static T GetOrDefault<T>(this List<T> list, int index, T def = default)
        {
            return list.Count() > index ? list[index] : def;
        }

        public static Vector2 Reciprocal(this Vector2 v, Vector2 ret = new())
        {
            ret.x = v.x == 0f ? 0f : 1f / v.x;
            ret.y = v.y == 0f ? 0f : 1f / v.y;
            return ret;
        }

        public static Vector3 Reciprocal(this Vector3 v, Vector3 ret = new())
        {
            ret.x = v.x == 0f ? 0f : 1f / v.x;
            ret.y = v.y == 0f ? 0f : 1f / v.y;
            ret.z = v.z == 0f ? 0f : 1f / v.z;
            return ret;
        }

        public static void Reciprocate(this Vector3 v)
        {
            Reciprocal(v, v);
        }

        public static Vector3 AddDimension(this Vector2 x)
        {
            return new Vector3(x.x, x.y, 0f);
        }

        public static Vector3[] AddDimension(this Vector2[] a)
        {
            return a.Select(x => new Vector3(x.x, x.y, 0f)).ToArray();
        }

        public static void Deconstruct(this Vector2 v, out float x, out float y)
        {
            x = v.x; y = v.y;
        }
    }
}
