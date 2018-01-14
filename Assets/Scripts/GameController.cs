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
