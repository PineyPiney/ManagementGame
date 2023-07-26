using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PineyPiney.Util
{
    public static class Maths
    {
        public static float AbsMin(float a, params float[] b)
        {
            float c = a;
            foreach (float v in b)
            {
                if (MathF.Abs(v) < MathF.Abs(c)) c = v;
            }
            return c;
        }

        public static float RoundToNearest(float value, float step)
        {
            float rem = value % step;
            float ret = value - rem;
            if (rem >= step / 2) ret += step;
            if (value < 0) ret -= step;
            return ret;
        }
    }
}
