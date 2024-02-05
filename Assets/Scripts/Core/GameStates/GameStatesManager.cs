using System;
using System.Collections.Generic;
using System.Linq;
using Core.DISystem;
using Core.GameStates.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameStates
{
    public class GameStatesManager : MonoSingleton<GameStatesManager>
    {
        [SerializeField] private GameStarts _gameStarts;
        [SerializeField] private GameRuns _gameRuns;
        
        private HashSet<GameState> _gameStates;

        private Queue<GameState> _pendingStates;
        
        private GameState _currentState;
        
        private IDependencyContainer _dependencyContainer;

        private Coroutine _currentInitProcess;
        private void Awake()
        {
            _dependencyContainer = new DependencyContainer();
            
            _gameStates = new HashSet<GameState>
            {
                Instantiate(_gameStarts, this.transform),
                Instantiate(_gameRuns, this.transform),
            };
            
            _pendingStates = new Queue<GameState>();

            foreach (var gameState in _gameStates)
            {
                _pendingStates.Enqueue(gameState);
            }

            var nextState = _pendingStates.Peek();
            _currentState = nextState;
            Debug.Log("Current state: " + _currentState.GetType()); 

            _currentState.TriggerStateSwitch += SwitchState;

            DontDestroyOnLoad(this.gameObject);
            SceneManager.LoadScene("Main");

            _currentInitProcess = StartCoroutine(_currentState.Initialize(_dependencyContainer));
        }

        private void SwitchState()
        {
            _currentState.Stop();
            _currentState.TriggerStateSwitch -= SwitchState;
            
            _pendingStates.Dequeue();
            var nextState = _pendingStates.Peek();
            Debug.Log("Switch state to " + nextState.GetType()); 


            _currentState = nextState;

            Debug.Log("Current state: " + _currentState.GetType()); 

            _currentState.TriggerStateSwitch += SwitchState;

            StopCoroutine(_currentInitProcess);
            _currentInitProcess = StartCoroutine(_currentState.Initialize(_dependencyContainer));
        }
    
        private void Update()
        {
            
            _currentState.TickState();
        }
        
    }
}