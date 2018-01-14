using UnityEngine;
using System.Collections;
using LoopLib;
using System;
using System.Collections.Generic;

public struct TankData : IEntityState
{
    public float X { get; set; }
    public float Y { get; set; }
    public float TurretAngle { get; set; }
    public float XSpeed { get; set; }
    public EntityId ShellId { get; set; }
    public int TankColorId { get; set; }
}

public class TankEntityComponent : Containers.EntityType
{
    public override EntityType CreateType()
    {
        return new TankEntity(MaxEntities);
    }
}

public class TankEntity : LoopLib.EntityType<TankData>
{
    public TankEntity(int maxEntities) : base("TankEntity", maxEntities)
    {
    }

    public override void UpdateEntity(TankData entity, Action<IExecutable> addMutation, DeterministicRandom random, int owner, LaggedView view, out TankData? newVal)
    {
        TankData newData = entity;
        newData.X += newData.XSpeed;
        newData.XSpeed *= 0.7f;
        newVal = newData;
    }

    public MutationEvent<TankData, float> MutateSpeed(int id, float speed)
    {
        return new MutationEvent<TankData, float>
        {
            EntityType = this,
            EntityId = id,
            Mutator = SpeedMutator,
            Args = speed,
        };
    }

    private static void SpeedMutator(ref TankData? victim, float data, List<IExecutable> mutations)
    {
        if (!victim.HasValue)
        {
            return;
        }
        var newData = victim.Value;
        newData.XSpeed += data;
        newData.XSpeed = Mathf.Clamp(newData.XSpeed, -1f, 1f);
        victim = newData;
    }

    public override void HandleClientEvent(ClientEvent e, Action<IExecutable> addMutation, TankData? entityData)
    {
        var mutationEvent = new MutationEvent<TankData, float>
        {
            EntityType = this,
            EntityId = e.DrivingEntityId,
            Mutator = (ref TankData? victim, float data, List<IExecutable> mutations) =>
            {
                if (!victim.HasValue)
                {
                    return;
                }
                var newData = victim.Value;
                newData.XSpeed += data;
                newData.XSpeed = Mathf.Clamp(newData.XSpeed, -1f, 1f);
                victim = newData;
            },
            Args = e.MoveHoriz,
        };
        if (e.MoveHoriz != 0f)
        {
            addMutation(mutationEvent);
        }
        var mutationEventTurret = new MutationEvent<TankData, float>
        {
            EntityType = this,
            EntityId = e.DrivingEntityId,
            Mutator = (ref TankData? victim, float data, List<IExecutable> mutations) =>
            {
                if (!victim.HasValue)
                {
                    return;
                }
                var newData = victim.Value;
                newData.TurretAngle += data;
                newData.TurretAngle = Mathf.Clamp(newData.TurretAngle, 15f, 165f);
                victim = newData;
            },
            Args = e.TurretMove,
        };
        if (e.TurretMove != 0f)
        {
            addMutation(mutationEventTurret);
        }

        if (e.TurretTigger && entityData.HasValue)
        {
            var mutationEventShell = new MutationEvent<ShellData, float>
            {
                EntityType = (EntityType<ShellData>)entityData.Value.ShellId.Type,
                EntityId = entityData.Value.ShellId.Id,
                Mutator = (ref ShellData? victim, float data, List<IExecutable> mutations) =>
                {
                    if (victim.HasValue)
                    {
                        return; // we still have active shell.
                    }
                    victim = new ShellData
                    {
                        X = entityData.Value.X,
                        Y = entityData.Value.Y,
                        XSpeed = Mathf.Cos(data / 180 * Mathf.PI) * 0.1f,
                        YSpeed = Mathf.Sin(data / 180 * Mathf.PI) * 0.1f,
                    };
                },
                Args = entityData.Value.TurretAngle,
            };
            addMutation(mutationEventShell);
        }

        if (e.NewTankId.HasValue && e.NewShellId.HasValue && e.NewTankTigger)
        {
            var mutationEventNewTank = new MutationEvent<TankData, TankData>
            {
                EntityType = this,
                EntityId = e.NewTankId.Value.Id,
                Mutator = (ref TankData? victim, TankData data, List<IExecutable> mutations) => {
                    victim = data;
                },
                Args = new TankData { X = 0, ShellId = e.NewShellId.Value },
            };
            addMutation(mutationEventNewTank);
        }
    }

    public MutationEvent<TankData, int> MutateDestory(int id)
    {
        return new MutationEvent<TankData, int>
        {
            EntityType = this,
            EntityId = id,
            Mutator = DestoryMutator,
            Args = 0,
        };
    }

    private static void DestoryMutator(ref TankData? victim, int data, List<IExecutable> mutations)
    {
        victim = null;
    }


    public override TankData Interpolate(TankData second, TankData first, float amount)
    {
        TankData result = second;

        result.X = first.X * amount + second.X * (1 - amount);
        result.Y = first.Y * amount + second.Y * (1 - amount);
        result.TurretAngle = first.TurretAngle * amount + second.TurretAngle * (1 - amount);

        return result;
    }
}

