using System;
using System.Collections.Generic;

namespace LoopLib
{
    public class Snapshot
    {
        public int FrameIndex { get; private set; }
        public readonly EntityList[] EntityLists;
        public readonly List<IExecutable> Mutations;
        public readonly Dictionary<int, ClientEvent> ClientEvents;

        private List<EntityType> entityTypes;

        public Snapshot(int frameIndex, List<EntityType> entityTypes, Snapshot laggedSnapshot)
        {
            FrameIndex = frameIndex;
            EntityLists = new EntityList[entityTypes.Count];
            Mutations = new List<IExecutable>();
            ClientEvents = new Dictionary<int, ClientEvent>();
            this.entityTypes = entityTypes;

            foreach (var type in entityTypes)
            {
                EntityLists[type.TypeId] = type.CreateEntityList(this, laggedSnapshot ?? this);
            }
        }

        public void InitFrom(Snapshot prev)
        {
            Mutations.Clear();

            int types = EntityLists.Length;
            for (int i = 0; i < types; i++)
                EntityLists[i].InitFrom(prev, Mutations);

            foreach(var e in ClientEvents.Values)
            {
                var entityType = entityTypes[e.DrivingTypeId];
                var data = EntityLists[e.DrivingTypeId].GetRaw(e.DrivingEntityId);
                if (data != null)
                    entityType.HandleClientEvent(e, Mutations.Add, data);
            }

            int mutations = prev.Mutations.Count;
            for (int i = 0; i < mutations; i++)
                prev.Mutations[i].Execute(this);
        }

        public T? Get<T>(EntityType<T> type, int id) where T : struct, IEntityState
        {
            var list = (EntityList<T>)EntityLists[type.TypeId];
            return list[id];
        }
    }
}