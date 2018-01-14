using System.Collections.Generic;
using UnityEngine;

namespace Containers
{
    public class ClientView : MonoBehaviour {

        public Wave Wave;
        public int ClientId;

        public LoopLib.ClientState Client { get; private set; }
        private Dictionary<LoopLib.EntityType, int> nextIds = new Dictionary<LoopLib.EntityType, int>();

        public int GetNextId(LoopLib.EntityType type)
        {
            if (!nextIds.ContainsKey(type))
                nextIds.Add(type, 0);
            return nextIds[type]++;
        }

        public void Update()
        {
            Client = new LoopLib.ClientState(Wave.LoopWave.Universe, Wave.LoopWave.GameTime, ClientId);
        }
    }
}
