using UnityEngine;

namespace Leaf._helpers
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        [Header(("Time Parameters"))]
        [SerializeField]
        private float _uiDelay = 0.125f;
        public float UiDelay { get => _uiDelay; private set => _uiDelay = value; }

        [Header(("Time Parameters"))]
        [SerializeField]
        private float _popUpTextAnimationDuration = 0.125f;
        public float PopUpTextAnimationDuration
        {
            get => _popUpTextAnimationDuration;
            private set => _popUpTextAnimationDuration = value;
        }

        public float PopUpTextAnimationDelay
        {
            get => _popUpTextAnimationDuration * Random.Range(3, 5);
            private set => _popUpTextAnimationDuration = value;
        }

        [SerializeField]
        private float _bulletArriveTime = 0.125f;
        public float BulletArriveTime { get => _bulletArriveTime; private set => _bulletArriveTime = value; }

        [SerializeField]
        private float _damagableDestroyDelay = 1f;
        public float DamagableDestroyDelay
        {
            get => _damagableDestroyDelay;
            private set => _damagableDestroyDelay = value;
        }

        [SerializeField]
        private float _damagableScaleChangeTime = 1f;
        public float DamagableScaleChangeTime
        {
            get => _damagableScaleChangeTime;
            private set => _damagableScaleChangeTime = value;
        }

        [SerializeField]
        private float _menuAnimationDuration = 0.15f;
        public float MenuAnimationDuration
        {
            get => _menuAnimationDuration;
            private set => _menuAnimationDuration = value;
        }

        [SerializeField]
        private float _menuAnimationDelay = 0.05f;
        public float MenuAnimationDelay
        {
            get => _menuAnimationDelay;
            private set => _menuAnimationDelay = value;
        }

        [SerializeField]
        private float _hitMaterialChangeInterval = 0.15f;
        public float HitMaterialChangeInterval
        {
            get => _hitMaterialChangeInterval;
            private set => _hitMaterialChangeInterval = value;
        }

        [SerializeField]
        private float _startDropDownDuration = 1f;
        public float StartDropDownDuration
        {
            get => _startDropDownDuration;
            private set => _startDropDownDuration = value;
        }

        [SerializeField]
        private float _startDropDownDelay = 0.25f;
        public float StartDropDownDelay
        {
            get => _startDropDownDelay;
            private set => _startDropDownDelay = value;
        }

        [SerializeField]
        private float _startScaleUpDuration = 1f;
        public float StartScaleUpDuration
        {
            get => _startScaleUpDuration;
            private set => _startScaleUpDuration = value;
        }

        [SerializeField]
        private float _startScaleUpDelay = 0.25f;
        public float StartScaleUpDelay
        {
            get => _startScaleUpDelay;
            private set => _startScaleUpDelay = value;
        }
        public void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }
    }
}