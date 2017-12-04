using System.Collections;
using System.Collections.Generic;

namespace LoopLib
{
    public class LaggedView
    {
        public readonly Snapshot LaggedSnapshot;
        public readonly Snapshot NormalSnapshot;
        public int ClientID;
        
        public LaggedView(Snapshot laggedSnapshot, Snapshot normalSnapshot)
        {
            LaggedSnapshot = laggedSnapshot;
            NormalSnapshot = normalSnapshot;
        }

        public T? Get<T>(EntityType<T> type, int id) where T : struct, IEntityState
        {
            Snapshot snapshot;
            if (type.Owners[id] == ClientID)
                snapshot = NormalSnapshot;
            else
                snapshot = LaggedSnapshot;

            var list = (EntityList<T>)snapshot.EntityLists[type.TypeId];
            return list[id];
        }
    }

}