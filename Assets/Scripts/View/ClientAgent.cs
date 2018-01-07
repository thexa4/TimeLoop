using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class ClientAgent: MonoBehaviour {

    public GameController GameController;

    public int ClientId;

    public float GameTime;

    public TankView[] Tanks;

    private Wave wave;

    public void Start()
    {
        wave = new Wave();
        wave.Universe = GameController.Universe;
        wave.SetGameTime(GameTime);
    }

    public void Update()
    {
        Universe universe = GameController.Universe;

        var view = new ClientView(universe, GameTime, ClientId);

        wave.SetGameTime(GameTime);
        
        foreach (TankView tank in Tanks)
        {
            tank.OnUpdate(view);
        }

        GameTime += 0.001f;
        
        var inputEvent = new ClientEvent
        {
            MoveHoriz = Input.GetAxis("MoveTank" + ClientId),
            DrivingTypeId = GameController.TankEnityType.TypeId,
            DrivingEntityId = ClientId,
            GameTime = GameTime,
            RealTime = 0,
        };
        wave.AddClientEvent(inputEvent);
    }

}
