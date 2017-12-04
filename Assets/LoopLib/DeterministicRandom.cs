using System;

namespace LoopLib
{
    public struct DeterministicRandom
    {
        public UInt32 State { get; set; }

        public double NextDouble()
        {
            var newState = State;
            newState ^= newState << 13;
            newState ^= newState >> 17;
            newState ^= newState << 5;
            State = newState;

            double result = ((double)State) / ((double)UInt32.MaxValue);
            return result;
        }

        public int Next()
        {
            return (int)Math.Floor(NextDouble() * int.MaxValue);
        }

        public int Next(int max)
        {
            return (int)Math.Floor(NextDouble() * max);
        }

        public int Next(int min, int max)
        {
            return (int)Math.Floor(NextDouble() * (max - min)) + min;
        }

        public float Sample()
        {
            return (float)NextDouble();
        }
    }
}