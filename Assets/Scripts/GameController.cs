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
                    TankEnityType,
                    ShellEnityType,
                };
            }
            return _enitityTypes;
        }
    }

    private Wave[] _waves;
    public Wave[] Waves
    {
        get
        {
            if (_waves == null)
            {
                _waves = CreateWaves();
            }
            return _waves;
        }
    }

    public TankEntity TankEnityType;
    public ShellEntity ShellEnityType;

    public void Start()
    {

        TankEnityType = new TankEntity();
        ShellEnityType = new ShellEntity(TankEnityType);

        var tank1 = TankEnityType.CreateNew(0);
        var tank2 = TankEnityType.CreateNew(1);

        var shell1 = ShellEnityType.CreateNew(0);
        var shell2 = ShellEnityType.CreateNew(1);

        var mutationEvent1 = new MutationEvent<TankData, TankData>
        {
            EntityType = TankEnityType,
            EntityId = tank1.Id,
            Mutator = (ref TankData? victim, TankData data, List<IExecutable> mutations) => { victim = data; },
            Args = new TankData { X = -3, ShellId = shell1 },
        };

        var mutationEvent2 = new MutationEvent<TankData, TankData>
        {
            EntityType = TankEnityType,
            EntityId = tank2.Id,
            Mutator = (ref TankData? victim, TankData data, List<IExecutable> mutations) => { victim = data; },
            Args = new TankData { X = 3, ShellId = shell2},
        };

        Universe.Frames[0].Mutations.Add(mutationEvent1);
        Universe.Frames[0].Mutations.Add(mutationEvent2);

        for (int i = 1; i < Universe.Frames.Length; i++)
        {
            Universe.Frames[i].InitFrom(Universe.Frames[i - 1]);
        }
    }

    public void Update()
    {
        Waves[0].GameTime += Time.deltaTime;
    }

    private Wave[] CreateWaves()
    {
        return new Wave[] {
            new Wave() {
                Universe = Universe,
                GameTime = 0,
            },
        };
    }
}
