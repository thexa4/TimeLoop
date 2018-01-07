using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class GameController : MonoBehaviour {



    private Universe _universe;
    public Universe Universe
    {
        get
        {
            if(_universe == null)
            {
                _universe = new Universe(20, 60, 6, EnitityTypes);
            }
            return _universe;
        }
    }

    private EntityType[] _enitityTypes;
    public EntityType[] EnitityTypes
    {
        get
        {
            if (_enitityTypes == null)
            {
                _enitityTypes = new EntityType[] {
                    TankEnityType
                };
            }
            return _enitityTypes;
        }
    }

    public TankEntity TankEnityType = new TankEntity();
    
    public void Start()
    {
        var tank1 = TankEnityType.CreateNew(0);
        var tank2 = TankEnityType.CreateNew(1);

        var mutationEvent1 = new MutationEvent<TankData, TankData>
        {
            EntityType = TankEnityType,
            EntityId = tank1.Id,
            Mutator = (ref TankData? victim, TankData data, List<IExecutable> mutations) => { victim = data; },
            Args = new TankData { },
        };

        var mutationEvent2 = new MutationEvent<TankData, TankData>
        {
            EntityType = TankEnityType,
            EntityId = tank2.Id,
            Mutator = (ref TankData? victim, TankData data, List<IExecutable> mutations) => { victim = data; },
            Args = new TankData { X = 3 },
        };

        Universe.Frames[0].Mutations.Add(mutationEvent1);
        Universe.Frames[0].Mutations.Add(mutationEvent2);

        for (int i = 1; i < Universe.Frames.Length; i++)
        {
            Universe.Frames[i].InitFrom(Universe.Frames[i - 1]);
        }
    }
}
