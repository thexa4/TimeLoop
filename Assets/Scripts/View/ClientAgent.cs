using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoopLib;

public class ClientAgent: MonoBehaviour {

    public Containers.Wave Wave;
    public int ClientId;

    public TankView[] Tanks;
    public ShellView[] Shells;

    private Wave wave;
    

    public void Update()
    {
        var view = new ClientView(Wave.LoopWave.Universe, wave.GameTime, ClientId);
        
        foreach (TankView tank in Tanks)
        {
            tank.OnUpdate(view);
        }

        foreach (ShellView shell in Shells)
        {
            shell.OnUpdate(view);
        }

        var inputEvent = new ClientEvent
        {
            MoveHoriz = Input.GetAxis("MoveTank" + ClientId) * 0.1f,
            TurretMove = Input.GetAxis("MoveTurret" + ClientId) * 3f,
            TurretTigger = Input.GetKey(ClientId == 0 ? KeyCode.Joystick1Button5 : KeyCode.Joystick2Button5),
            DrivingTypeId = GameController.TankEnityType.TypeId,
            DrivingEntityId = ClientId,
            GameTime = wave.GameTime,
            RealTime = 0,
        };
        wave.AddClientEvent(inputEvent);
    }

}
