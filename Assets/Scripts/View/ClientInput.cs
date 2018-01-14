using UnityEngine;
using Containers;

public class ClientInput: MonoBehaviour {

    public ClientView ClientView;

    public TankEntityComponent TankEntityType;
    public ShellEntityComponent ShellEnityType;

    public LoopLib.EntityId? _currentTank;
    public LoopLib.EntityId? _currentShell;
    
    private float _spawnCoolDown = -100f;
    private float _lastFrame = -1;

    public void Update()
    {
        if (ClientView.Wave.LoopWave.FrameNumber <= _lastFrame) return;
        _lastFrame = ClientView.Wave.LoopWave.FrameNumber;

        if (_currentTank.HasValue && ClientView.Client.Get((TankEntity)_currentTank.Value.Type, _currentTank.Value.Id) == null && Time.time > _spawnCoolDown)
        {
            _currentTank = null;
            _currentShell = null;
        }

        bool newTank = false;
        if (!_currentTank.HasValue && Input.GetKey(ClientView.ClientId == 0 ? KeyCode.Joystick1Button7 : KeyCode.Joystick2Button7) && Time.time > _spawnCoolDown)
        {
            _currentTank = TankEntityType.LoopType.CreateNew(ClientView.ClientId);
            _currentShell = ShellEnityType.LoopType.CreateNew(ClientView.ClientId);
            newTank = true;
            _spawnCoolDown = Time.time + 3;
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
