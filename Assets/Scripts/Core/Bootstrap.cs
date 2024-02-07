using Sirenix.Utilities;
using UnityEngine;

namespace Core
{
    public class Bootstrap : MonoSingleton<Bootstrap>
    {
        [SerializeField] private GameObject _core;
        private void Awake()
        {
            var coreObject = Instantiate(_core, Vector3.zero, Quaternion.identity);

            ISystem[] gameSystems = coreObject.GetComponentsInChildren<ISystem>();

            gameSystems.ForEach(system => system.Run());
            
            DontDestroyOnLoad(coreObject);
        }
    }
}