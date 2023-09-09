using UnityEngine;

namespace IronMountain.StandardAnimations.RectTransforms
{
    [RequireComponent(typeof(Drawer))]
    [RequireComponent(typeof(AudioSource))]
    public class DrawerSFX : MonoBehaviour
    {
        [SerializeField] private AudioClip drawerOpenSFX;
        [SerializeField] private AudioClip drawerCloseSFX;
    
        [Header("Cache")]
        private Drawer _drawer;
        private AudioSource _audioSource;

        private void Awake()
        {
            _drawer = GetComponent<Drawer>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (_drawer) _drawer.OnMoving += OnDrawerMoving;
        }

        private void OnDisable()
        {
            if (_drawer) _drawer.OnMoving -= OnDrawerMoving;
        }

        private void OnDrawerMoving(float seconds)
        {
            if (!_drawer || !_audioSource) return;
            _audioSource.PlayOneShot(_drawer.PreviousTarget < _drawer.CurrentTarget 
                ? drawerOpenSFX 
                : drawerCloseSFX);
        }
    }
}
