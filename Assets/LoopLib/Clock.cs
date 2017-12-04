using System;
using System.Collections.Generic;

namespace LoopLib
{
    public class Clock
    {
        public readonly List<EntityId> Members;

        public Clock()
        {
            Members = new List<EntityId>();
        }
    }
}