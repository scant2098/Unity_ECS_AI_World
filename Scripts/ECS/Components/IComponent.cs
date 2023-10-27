using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using ZFramework;

namespace JH_ECS
{
    public interface IComponent
    {
        [JsonIgnore]
        public IComponent Default { get; }
    }

}
