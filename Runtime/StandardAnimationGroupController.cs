using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IronMountain.StandardAnimations
{
    [Serializable]
    public struct StandardAnimationGroupController
    {
        public event Action OnInstanceChanged;
        
        [SerializeField] private StandardAnimationGroup prefab;

        private StandardAnimationGroup _instance;

        public StandardAnimationGroup Instance
        {
            get => _instance;
            private set
            {
                if (_instance == value) return;
                _instance = value;
                OnInstanceChanged?.Invoke();
            }
        }

        public StandardAnimationGroup Spawn(Transform parent)
        {
            if (Instance || !prefab) return Instance;
            Instance = Object.Instantiate(prefab, parent);
            ExitImmediate();
            Enter();
            return Instance;
        }

        public void Enter()
        {
            if (Instance) Instance.Enter();
        }
        
        public void EnterImmediate()
        {
            if (Instance) Instance.EnterImmediate();
        }

        public void Exit()
        {
            if (Instance) Instance.Exit();
        }
        
        public void ExitImmediate()
        {
            if (Instance) Instance.ExitImmediate();
        }

        public void Destroy(float delaySeconds)
        {
            if (!Instance) return;
            Exit();
            Object.Destroy(Instance.gameObject, delaySeconds);
            Instance = null;
        }
    }
}