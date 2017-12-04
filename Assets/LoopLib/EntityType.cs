﻿using System.Collections;
using System.Collections.Generic;
using System;

namespace LoopLib
{
    public delegate void EntityUpdate<T>(T entity, DeterministicRandom random, out T? newVal) where T : struct, IEntityState;

    public abstract class EntityType
    {
        public int TypeId { get; private set; }

        public EntityType()
        {
            TypeId = -1;
        }

        public abstract EntityList CreateEntityList(Snapshot current, Snapshot laggedSnapshot);

        public void Initialize(int id)
        {
            if (TypeId != -1)
                throw new InvalidOperationException("Already initialized");

            TypeId = id;
        }
    }

    /// <summary>
    /// Defines an entity. Controls how it should be updated and how many there can be
    /// </summary>
    public abstract class EntityType<T> : EntityType where T: struct, IEntityState
    {
        public int MaxEntities { get; private set; }
        public string Name { get; private set; }
        public int BaseSeed { get; private set; }
        public int NextEntityId { get; private set; }
        public readonly int[] Owners;

        public EntityType(string name, int maxEntities)
        {
            MaxEntities = maxEntities;
            Name = name;
            BaseSeed = name.GetHashCode();
            Owners = new int[maxEntities];
        }

        public sealed override EntityList CreateEntityList(Snapshot current, Snapshot laggedSnapshot)
        {
            LaggedView view = new LaggedView(current, laggedSnapshot);
            return new EntityList<T>(this, view);
        }

        public virtual void UpdateEntity(T entity, DeterministicRandom random, int owner, LaggedView view, out T? newVal)
        {
            newVal = entity;
        }

        public virtual void HandleClientEvent(ClientEvent e, ClientView view, EntityType type) {}

        public virtual T Interpolate(T first, T second, float amount)
        {
            T result = first;

            result.X = first.X * amount + second.X * (1 - amount);
            result.Y = first.Y * amount + second.Y * (1 - amount);

            return result;
        }

        public EntityId CreateNew(Snapshot snapshot, int clientId)
        {
            if (NextEntityId > MaxEntities)
                throw new InvalidOperationException("Too many entities created of type: " + Name);

            Owners[NextEntityId] = clientId;
            return new EntityId(this, NextEntityId++);
        }
    }
}