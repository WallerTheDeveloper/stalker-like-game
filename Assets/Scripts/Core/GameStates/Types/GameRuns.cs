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
        protected override IDependencyProvider DependencyProvider { get; set; }

        public override void Initialize(IDependencyContainer container)
        {
            _dependencyContainer = container;
            DependencyProvider = (IDependencyProvider) container;
            
            _playerInput ??= DependencyProvider.GetDependency<PlayerInput>();
        }

        public override void TickState()
        {
            _dependencyContainer.Tick(_playerInput);
        }

        public override void Stop()
        {}
    }
}