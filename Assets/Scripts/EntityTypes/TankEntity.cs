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
}

public class TankEntity : LoopLib.EntityType<TankData>
{
    public TankEntity() : base("TankEntity", 2)
    {
    }

    public override void UpdateEntity(TankData entity, DeterministicRandom random, int owner, LaggedView view, out TankData? newVal)
    {
        TankData newData = entity;
        newData.X += newData.XSpeed;
        newData.XSpeed *= 0.95f;
        newVal = newData;
    }

    public override void HandleClientEvent(ClientEvent e, Action<IExecutable> addMutation, TankData entityData)
    {
        var mutationEvent = new MutationEvent<TankData, float>
        {
            EntityType = this,
            EntityId = e.DrivingEntityId,
            Mutator = (ref TankData? victim, float data, List<IExecutable> mutations) => {
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
        addMutation(mutationEvent);
    }
}
