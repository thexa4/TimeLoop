using System;
using System.Collections.Generic;

namespace LoopLib
{
    public class Universe
    {
        public int TickRate { get; private set; }
        public int TotalGameSeconds { get; private set; }
        public Snapshot[] Frames { get; private set; }
        public int LagCompensation { get; private set; }
        
        public Universe(int tickRate, int totalGameSeconds, int lagCompensationFrames, IEnumerable<EntityType> entityTypes)
        {
            TickRate = tickRate;
            TotalGameSeconds = totalGameSeconds;
            LagCompensation = lagCompensationFrames;

            var typeList = new List<EntityType>(entityTypes);
            for (int i = 0; i < typeList.Count; i++)
                typeList[i].Initialize(i);
            
            int frameCount = tickRate * totalGameSeconds;
            Frames = new Snapshot[frameCount];

            Frames[0] = new Snapshot(0, typeList, null);
            for (int i = 1; i < frameCount; i++)
                Frames[i] = new Snapshot(i, typeList, Frames[Math.Max(0, i - LagCompensation)]);
        }
    }
}