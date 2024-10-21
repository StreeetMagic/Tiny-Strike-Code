using DG.Tweening;
using UnityEngine;

namespace Core.Grenades
{
    public class GrenadeDetonationRadius : MonoBehaviour
    {
        [SerializeField] private RectTransform _detonationRadius;
        private float _localRadius;
        private GrenadeConfig _config;

        public void Init(GrenadeConfig config)
        {
            _config = config;
            float radius = config.DetonationRadius;
            _localRadius = radius * 2;

            transform.localScale = new Vector3(_localRadius, _localRadius, _localRadius);
        }

        private void Start()
        {
            StartDetonationDuration(_localRadius, _config.DetonationTime);
        }

        private void StartDetonationDuration(float targetScale, float duration)
        {
            _detonationRadius.DOScale(new Vector3(targetScale, targetScale, targetScale), duration)
                .SetEase(Ease.InOutQuad);
        }
    }
}