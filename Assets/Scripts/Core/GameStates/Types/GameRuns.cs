using System;
using System.Collections;
using Core.DISystem;
using Input;

namespace Core.GameStates.Types
{
    public class GameRuns : GameState
    {
        private IDependencyContainer _dependencyContainer;
        private IDependency _playerInput;

        public override Action TriggerStateSwitch { get; set; }
        public override bool IsInitialized { get; set; }

        public override IEnumerator Initialize(IDependencyContainer container)
        {
            _dependencyContainer = container;
            
            _playerInput ??= _dependencyContainer.GetDependency<PlayerInput>();
            
            IsInitialized = true;

            yield break;
        }

        public override void TickState()
        {
            _dependencyContainer.Tick(_playerInput);
        }

        public override void Stop()
        {}
    }
}