using System;
using System.Collections;
using Core.DISystem;
using UnityEngine;

namespace Core.GameStates
{
    public abstract class GameState : MonoBehaviour
    { 
        public abstract Action TriggerStateSwitch { get; set; }
        public abstract bool IsInitialized { get; set; }
        public abstract IEnumerator Initialize(IDependencyContainer container);
        public abstract void TickState();
        public abstract void Stop();
    }
}