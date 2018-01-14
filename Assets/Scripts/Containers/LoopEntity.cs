using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Containers;

namespace Containers
{
    public class LoopEntity : MonoBehaviour
    {
        public EntityType Type;
        public ClientInput Client;

        protected LoopLib.EntityId id;

        public void Start()
        {
            id = Type.LoopType.CreateNew(Client.ClientId);
        }

        public void Update()
        {
            var view = Client
        }
    }
}
