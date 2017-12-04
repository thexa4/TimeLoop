using System.Collections;
using System.Collections.Generic;

namespace LoopLib
{
    public struct EntityId
    {
        public readonly EntityType Type;
        public readonly int Id;

        public EntityId(EntityType type, int id)
        {
            Type = type;
            Id = id;
        }

    }

}