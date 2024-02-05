using System;
using System.Collections;
using System.Collections.Generic;
using Core.DISystem;
using Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameStates.Types
{
    public class GameStarts : GameState
    {
        [SerializeField] private PlayerMovementController _playerMovementController;

        private IDependencyContainer _dependencyContainer;
        public override Action TriggerStateSwitch { get; set; }
        public override bool IsInitialized { get; set; }

        public override IEnumerator Initialize(IDependencyContainer container)
        {
            _dependencyContainer = container;
            
            // TODO: Implement game scene manager
            while (SceneManager.GetActiveScene().name != "Main")
            {
                yield return null;
            }
            
            PlayerMovementController playerMovementController = Instantiate(_playerMovementController);
            RegisterDependenciesFor(playerMovementController);

            IsInitialized = true;
            
            TriggerStateSwitch?.Invoke();
        }

        public override void TickState()
        {}

        public override void Stop()
        {
            IsInitialized = false;
        }
        
        private void RegisterDependenciesFor(PlayerMovementController playerMovementController)
        {
            var playerMovementDependencies = new List<IDependency>
            {
                new PlayerInput(),
            };

            _dependencyContainer.Register<PlayerMovementController>(playerMovementController, playerMovementDependencies);
        }
    }
}