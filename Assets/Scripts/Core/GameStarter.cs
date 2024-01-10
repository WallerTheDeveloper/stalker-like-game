using System;
using System.Collections.Generic;
using Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    public class GameStarter : SerializedMonoBehaviour
    {
        private static GameStarter _instance;

        public static GameStarter Instance => _instance;

        [field: SerializeField] private List<ISystem> _systemList;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            foreach (var system in _systemList)
            {
                system.Register();
            }
        }

        private void OnDestroy()
        {
            foreach (var system in _systemList)
            {
                system.Deregister();
            }
        }
    }
}