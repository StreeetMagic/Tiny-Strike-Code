using UnityEngine;

namespace LevelDesign
{
  public class DisableMeshRendererOnAwake : MonoBehaviour
  {
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
      _meshRenderer = GetComponent<MeshRenderer>();
      _meshRenderer.enabled = false;
    }
  }
}