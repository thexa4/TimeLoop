using UnityEngine;
using System.Collections;
using LoopLib;
using System;

public struct ShellData : IEntityState
{
    public float X { get; set; }
    public float Y { get; set; }
    public float XSpeed { get; set; }
    public float YSpeed { get; set; }
}

public class ShellEntityComponent : Containers.EntityType
{
    public TankEntityComponent TankEntity;

    public override EntityType CreateType()
    {
        return new ShellEntity(TankEntity.LoopType as TankEntity, MaxEntities);
    }
}

public class ShellEntity : EntityType<ShellData>
{
    private TankEntity tankEntityType;

    public ShellEntity(TankEntity tankEntityType, int maxEntities) : base("ShellEntity", maxEntities)
    {
        this.tankEntityType = tankEntityType;
    }

    public override void UpdateEntity(ShellData entity, Action<IExecutable> addMutation, DeterministicRandom random, int owner, LaggedView view, out ShellData? newVal)
    {
        ShellData newData = entity;
        newData.X += newData.XSpeed;
        newData.XSpeed *= 0.95f;
        newData.Y += newData.YSpeed;
        newData.YSpeed -= 0.05f;
        newVal = newData;
        if(newData.Y < 0)
        {
            newVal = null;
            for(int i = 0; i < tankEntityType.MaxEntities; i++)
            {
                var tank = view.Get(tankEntityType, i);
                if(tank.HasValue && Mathf.Abs(tank.Value.X - newData.X) < 1)
                {
                    addMutation(tankEntityType.MutateDestory(i));
                }
            }        
        }
    }
}

