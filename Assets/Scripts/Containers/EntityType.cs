using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Containers
{
    public class EntityType : MonoBehaviour
    {
        public int MaxEntities;

        private LoopLib.EntityType _loopType;
        public LoopLib.EntityType LoopType
        {
            get
            {
                if (_loopType == null)
                    _loopType = CreateType();
                return _loopType;
            }
        }

        public virtual LoopLib.EntityType CreateType()
        {
            return null;
        }
    }
}
