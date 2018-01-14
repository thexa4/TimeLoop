using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Containers;

namespace Containers
{
    class TimeLine : MonoBehaviour
    {
        public int TickRate;
        public int TotalGameSeconds;
        public int LagCompensationFrames;
        public EntityType[] EntityTypes;

        private LoopLib.Universe _universe;
        public LoopLib.Universe Universe
        {
            get
            {
                if (_universe == null)
                {

                    var types = EntityTypes.Select((e) => e.LoopType);

                    _universe = new LoopLib.Universe(TickRate, TotalGameSeconds, LagCompensationFrames, types);
                }
                return _universe;
            }
        }
    }
}
