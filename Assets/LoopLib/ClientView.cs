using System;
using System.Collections;

namespace LoopLib
{
    public class ClientView
    {
        int clientId;

        Snapshot prevNow;
        Snapshot nextNow;

        Snapshot prevComp;
        Snapshot nextComp;

        float frac;
        
        public ClientView(Universe universe, float gameSeconds, int clientId)
        {
            this.clientId = clientId;

            int frameId = (int)Math.Floor(gameSeconds * universe.TickRate);
            frac = (float)((gameSeconds * universe.TickRate) % 1.0);
            prevNow = universe.Frames[frameId];
            nextNow = universe.Frames[frameId + 1];

            int compFrameId = Math.Max(0, frameId - universe.LagCompensation);
            prevComp = universe.Frames[compFrameId];
            nextComp = universe.Frames[compFrameId + 1];
        }

        public int Count(EntityType type)
        {
            return prevNow.EntityLists[type.TypeId].Length;
        }

        public T? Get<T>(EntityType<T> type, int id) where T : struct, IEntityState
        {
            Snapshot interpFrom;
            Snapshot interpTo;
            if (type.Owners[id] == clientId)
            {
                interpFrom = prevNow;
                interpTo = nextNow;
            } else {
                interpFrom = prevComp;
                interpTo = nextComp;
            }
            var fromList = (EntityList<T>)interpFrom.EntityLists[type.TypeId];
            if (!fromList[id].HasValue)
                return null;

            var toList = (EntityList<T>)interpTo.EntityLists[type.TypeId];
            if (!toList[id].HasValue)
                return fromList[id];
            
            return type.Interpolate(fromList[id].Value, toList[id].Value, frac);
        }
    }
}