using UnityEngine;

namespace IronMountain.StandardAnimations
{
    public class StandardAnimationGroup : MonoBehaviour
    {
        public enum ConfigurationAction
        {
            None,
            ExitImmediate,
            Exit,
            EnterImmediate,
            Enter
        };

        [SerializeField] private ConfigurationAction onAwake = ConfigurationAction.ExitImmediate;
        [SerializeField] private ConfigurationAction onStart = ConfigurationAction.Enter;
        [SerializeField] private StandardAnimation[] standardAnimations;

        public StandardAnimation[] StandardAnimations => standardAnimations;

        private void Awake() => Execute(onAwake);
        private void Start() => Execute(onStart);

        public void ExitImmediate()
        {
            foreach (StandardAnimation standardAnimation in standardAnimations)
            {
                if (standardAnimation) standardAnimation.ExitImmediate(); 
            }
        }

        public void Exit()
        {
            foreach (StandardAnimation standardAnimation in standardAnimations)
            {
                if (standardAnimation) standardAnimation.Exit(); 
            }
        }

        public void EnterImmediate()
        {
            foreach (StandardAnimation standardAnimation in standardAnimations)
            {
                if (standardAnimation) standardAnimation.EnterImmediate(); 
            }
        }

        public void Enter()
        {
            foreach (StandardAnimation standardAnimation in standardAnimations)
            {
                if (standardAnimation) standardAnimation.Enter(); 
            }
        }

        private void Execute(ConfigurationAction action)
        {
            switch (action)
            {
                case ConfigurationAction.ExitImmediate:
                    ExitImmediate();
                    break;
                case ConfigurationAction.Exit:
                    Exit();
                    break;
                case ConfigurationAction.EnterImmediate:
                    EnterImmediate();
                    break;
                case ConfigurationAction.Enter:
                    Enter();
                    break;
            }
        }
    }
}
