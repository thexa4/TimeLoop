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
        public readonly EntityType<T> EntityType;
		public readonly int EntityId;
        public readonly EntityMutator<T, D> Mutator;
        public readonly D Args;

        public void Execute(Snapshot destination)
        {
            var list = (EntityList<T>)destination.EntityLists[EntityType.TypeId];
            list.Mutate<D>(EntityId, Mutator, Args, destination.Mutations);
        }
    }
}