using System.Collections;
using System.Collections.Generic;
using System;

namespace LoopLib
{
    public delegate void EntityUpdate<T>(T entity, DeterministicRandom random, out T? newVal) where T : struct, IEntityState;

    public abstract class EntityType
    {
        public int TypeId { get; private set; }
        public int MaxEntities { get; private set; }
        public int NextEntityId { get; private set; }
        public string Name { get; private set; }
        public readonly int[] Owners;

        public EntityType(string name, int maxEntities)
        {
            TypeId = -1;
            Name = name;
            Owners = new int[maxEntities];
            MaxEntities = maxEntities;
        }

        public abstract void HandleClientEvent(ClientEvent e, Action<IExecutable> addMutation, IEntityState entityData);

        public abstract EntityList CreateEntityList(Snapshot current, Snapshot laggedSnapshot);

        public void Initialize(int id)
        {
            if (TypeId != -1)
                throw new InvalidOperationException("Already initialized");

            TypeId = id;
        }

        public EntityId CreateNew(int clientId)
        {
            if (NextEntityId > MaxEntities)
                throw new InvalidOperationException("Too many entities created of type: " + Name);

            Owners[NextEntityId] = clientId;
            return new EntityId(this, NextEntityId++);
        }
    }

    /// <summary>
    /// Defines an entity. Controls how it should be updated and how many there can be
    /// </summary>
    public abstract class EntityType<T> : EntityType where T: struct, IEntityState
    {
        public int BaseSeed { get; private set; }

        public EntityType(string name, int maxEntities) : base(name, maxEntities)
        {
            BaseSeed = name.GetHashCode();
        }

        public sealed override void HandleClientEvent(ClientEvent e, Action<IExecutable> addMutation, IEntityState entityData)
        {
            HandleClientEvent(e, addMutation, (T?)entityData);
        }  
        public virtual void HandleClientEvent(ClientEvent e, Action<IExecutable> addMutation, T? entityData) { }

        public sealed override EntityList CreateEntityList(Snapshot current, Snapshot laggedSnapshot)
        {
            LaggedView view = new LaggedView(current, laggedSnapshot);
            return new EntityList<T>(this, view);
        }

        public virtual void UpdateEntity(T entity, Action<IExecutable> addMutation, DeterministicRandom random, int owner, LaggedView view, out T? newVal)
        {
            newVal = entity;
        }

        public virtual T Interpolate(T second, T first, float amount)
        {
            T result = second;

            result.X = first.X * amount + second.X * (1 - amount);
            result.Y = first.Y * amount + second.Y * (1 - amount);

            return result;
        }
    }
}