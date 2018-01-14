using UnityEngine;
using Containers;

public class ClientInput: MonoBehaviour {

    public ClientView ClientView;

    public void Update()
    {
        var inputEvent = new LoopLib.ClientEvent
        {
            MoveHoriz = Input.GetAxis("MoveTank" + ClientView.ClientId) * 0.1f,
            TurretMove = Input.GetAxis("MoveTurret" + ClientView.ClientId) * 3f,
            TurretTigger = Input.GetKey(ClientView.ClientId == 0 ? KeyCode.Joystick1Button5 : KeyCode.Joystick2Button5),
            DrivingEntityId = ClientView.ClientId,
            GameTime = ClientView.Wave.GameTime,
            RealTime = 0,
        };
        ClientView.Wave.LoopWave.AddClientEvent(inputEvent);
    }

}
