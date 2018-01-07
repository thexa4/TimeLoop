using System;
using System.Collections;
using System.Collections.Generic;

namespace LoopLib
{
    public abstract class EntityList
    {
        public abstract void InitFrom(Snapshot prev);
        public abstract int Length { get; }

        public abstract IEntityState GetRaw(int pos);

    }

    public class EntityList<T> : EntityList where T : struct, IEntityState
    {
        public EntityType<T> EntityType { get; private set; }
        private T?[] Entities { get; set; }
        DeterministicRandom random = new DeterministicRandom();
        readonly LaggedView laggedView;

        public T? this[int pos]
        {
            get
            {
                return Entities[pos];
            }
            set
            {
                Entities[pos] = value;
            }
        }
        public override IEntityState GetRaw(int pos)
        {
            return this[pos]
;        }
        public override int Length { get { return Entities.Length; } }

        public EntityList(EntityType<T> type, LaggedView laggedView)
        {
            EntityType = type;
            Entities = new T?[type.MaxEntities];
            this.laggedView = laggedView;
        }

        public override void InitFrom(Snapshot prev)
        {
            var prevList = (EntityList<T>)prev.EntityLists[EntityType.TypeId];
            int len = Entities.Length;

            long baseSeed = EntityType.BaseSeed ^ (prev.FrameIndex * 1299827).GetHashCode();

            for (int i = 0; i < len; i++)
            {
                if (prevList.Entities[i] == null)
                    continue;

                random.State = (UInt32)(baseSeed ^ (i * 29387).GetHashCode());
                int owner = EntityType.Owners[i];
                laggedView.ClientID = owner;
                EntityType.UpdateEntity(prevList.Entities[i].Value, random, owner, laggedView, out Entities[i]);
            }
        }

        public void Mutate<D>(int id, EntityMutator<T, D> mutator, D data, List<IExecutable> mutations)
        {
            mutator(ref Entities[id], data, mutations);
        }
    }
}