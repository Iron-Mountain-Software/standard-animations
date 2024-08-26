using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IronMountain.StandardAnimations
{
    [Serializable]
    public struct StandardAnimationGroupTest<T> where T : MonoBehaviour
    {
        public event Action OnInstanceChanged;
        
        [SerializeField] private T prefab;

        private T _instance;

        public T Instance
        {
            get => _instance;
            private set
            {
                if (_instance == value) return;
                _instance = value;
                AnimationGroup = _instance 
                    ? _instance.GetComponentInChildren<StandardAnimationGroup>() 
                    : null;
                OnInstanceChanged?.Invoke();
            }
        }

        public StandardAnimationGroup AnimationGroup { get; private set; }

        public T Spawn(Transform parent)
        {
            if (Instance || !prefab) return Instance;
            Instance = Object.Instantiate(prefab, parent);
            ExitImmediate();
            Enter();
            return Instance;
        }

        public void Enter()
        {
            if (AnimationGroup) AnimationGroup.Enter();
        }
        
        public void EnterImmediate()
        {
            if (AnimationGroup) AnimationGroup.EnterImmediate();
        }

        public void Exit()
        {
            if (AnimationGroup) AnimationGroup.Exit();
        }
        
        public void ExitImmediate()
        {
            if (AnimationGroup) AnimationGroup.ExitImmediate();
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