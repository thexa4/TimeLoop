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
        public ClientView Client;

        protected LoopLib.EntityId id;

        public virtual void Start()
        {
            id = Type.LoopType.CreateNew(Client.ClientId);
        }

        public void Update()
        {
            var view = Client.Client;
            OnUpdate(view);
        }

        public virtual void OnUpdate(LoopLib.ClientView view)
        {

        }
    }
}
