using System;
using UnityEngine;

namespace IronMountain.StandardAnimations
{
    public abstract class StandardAnimation : MonoBehaviour
    {
        public float seconds;
        
        public float Seconds => seconds;

        // Parameterless for UnityEvents
        public abstract void Enter();
        public abstract void Enter(Action onComplete);
        public abstract void Enter(float animationSeconds, Action onComplete = null);
        
        // Parameterless for UnityEvents
        public abstract void EnterImmediate();
        public abstract void EnterImmediate(Action onComplete);
        
        // Parameterless for UnityEvents
        public abstract void Exit();
        public abstract void Exit(Action onComplete);
        public abstract void Exit(float animationSeconds, Action onComplete = null);

        // Parameterless for UnityEvents
        public abstract void ExitImmediate();
        public abstract void ExitImmediate(Action onComplete);
    }
}
