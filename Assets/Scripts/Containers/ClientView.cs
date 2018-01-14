using UnityEngine;

namespace Containers
{
    public class ClientView : MonoBehaviour {

        public Wave Wave;
        public int ClientId;

        public LoopLib.ClientView Client { get; private set; }

        public void Update()
        {
            Client = new LoopLib.ClientView(Wave.LoopWave.Universe, Wave.LoopWave.GameTime, ClientId);
        }
    }
}
