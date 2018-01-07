using System.Collections;
using System.Collections.Generic;

namespace LoopLib
{
    public interface IExecutable
    {
        void Execute(Snapshot destination);
    }

    public delegate void EntityMutator<T, D>(ref T? victim, D data, List<IExecutable> mutations) where T : struct, IEntityState;

    public struct MutationEvent<T, D> : IExecutable where T : struct, IEntityState
    {
        public EntityType<T> EntityType;
		public int EntityId;
        public EntityMutator<T, D> Mutator;
        public D Args;

        public void Execute(Snapshot destination)
        {
            var list = (EntityList<T>)destination.EntityLists[EntityType.TypeId];
            list.Mutate<D>(EntityId, Mutator, Args, destination.Mutations);
        }
    }
}