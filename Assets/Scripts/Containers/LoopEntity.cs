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

        protected int Id { get; private set; }

        public virtual void Start()
        {
            Id = Client.GetNextId(Type.LoopType);
            gameObject.SetActive(false);
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
