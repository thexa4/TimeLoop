using System.Collections;
using System.Collections.Generic;

namespace LoopLib
{
    public struct ClientEvent
    {
        public int DrivingTypeId;
        public int DrivingEntityId;
        public float GameTime;
        public float RealTime;

        public float MoveHoriz;
        public float TurretMove;
        public bool TurretTigger;
        public EntityId? NewTankId;
        public EntityId? NewShellId;
        public bool NewTankTigger;
    }
}