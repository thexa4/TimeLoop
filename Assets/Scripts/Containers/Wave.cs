using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Containers
{
    public class Wave : MonoBehaviour
    {
        public TimeLine TimeLine;
        public float GameTime;

        private LoopLib.Wave _loopWave;
        public LoopLib.Wave LoopWave
        {
            get
            {
                if (_loopWave == null)
                    _loopWave = new LoopLib.Wave()
                    {
                        Universe = TimeLine.Universe,
                        GameTime = GameTime,
                    };
                return _loopWave;
            }
        }

        public void Update()
        {
            LoopWave.GameTime += Time.deltaTime;
        }

    }
}
