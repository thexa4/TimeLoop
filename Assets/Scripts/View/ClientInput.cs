using UnityEngine;
using Containers;

public class ClientInput: MonoBehaviour {

    public ClientView ClientView;

    public TankEntityComponent TankEntityType;
    public ShellEntityComponent ShellEnityType;

    public LoopLib.EntityId? _currentTank;
    public LoopLib.EntityId? _currentShell;

    public void Update()
    {
        bool newTank = false;
        if (!_currentTank.HasValue && Input.GetKey(ClientView.ClientId == 0 ? KeyCode.Joystick1Button7 : KeyCode.Joystick2Button7))
        {
            _currentTank = TankEntityType.LoopType.CreateNew(ClientView.ClientId);
            _currentShell = ShellEnityType.LoopType.CreateNew(ClientView.ClientId);
            newTank = true;
        }

        if (_currentTank.HasValue)
        {
            var inputEvent = new LoopLib.ClientEvent
            {
                MoveHoriz = Input.GetAxis("MoveTank" + ClientView.ClientId) * 0.001f,
                TurretMove = Input.GetAxis("MoveTurret" + ClientView.ClientId) * 3f,
                TurretTigger = Input.GetKey(ClientView.ClientId == 0 ? KeyCode.Joystick1Button5 : KeyCode.Joystick2Button5),
                DrivingEntityId = _currentTank.Value.Id,
                NewTankId = _currentTank,
                NewShellId = _currentShell,
                GameTime = ClientView.Wave.GameTime,
                NewTankTigger = newTank,
                RealTime = 0,
            };
            ClientView.Wave.LoopWave.AddClientEvent(inputEvent);
        }
    }

}
