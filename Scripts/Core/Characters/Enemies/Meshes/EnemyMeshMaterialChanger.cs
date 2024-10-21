using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Core.Characters.Enemies
{
  public class EnemyMeshMaterialChanger : IInitializable, IDisposable
  {
    private readonly IHealth _health;
    private readonly List<Material> _originalMaterials = new();
    private readonly EnemyMeshModelSpawner _meshModelSpawner;

    private List<SkinnedMeshRenderer> _skinnedMeshRenderers;
    private Tween _tween;

    public EnemyMeshMaterialChanger(IHealth health, EnemyMeshModelSpawner meshModelSpawner)
    {
      _health = health;
      _meshModelSpawner = meshModelSpawner;
    }

    public EnemyMeshModel EnemyMeshModel => _meshModelSpawner.MeshModel;
    private Material NewMaterial => EnemyMeshModel.NewMaterial;
    private Material TransitionMaterial => EnemyMeshModel.TransitionMaterial;
    private float DurationFirstMaterial => EnemyMeshModel.DurationFirstMaterial;
    private float TransitionDuration => EnemyMeshModel.TransitionDuration;
    private float DurationSecondMaterial => EnemyMeshModel.DurationSecondMaterial;

    public void Initialize()
    {
      _health.Damaged += OnHealthChanged;

      if (!EnemyMeshModel)
        throw new ArgumentNullException(nameof(EnemyMeshModel));

      _skinnedMeshRenderers = EnemyMeshModel.Meshes;

      foreach (SkinnedMeshRenderer renderer in _skinnedMeshRenderers)
      {
        if (renderer != null)
          _originalMaterials.Add(renderer.material);
        else
          Debug.LogError("One of the SkinnedMeshRenderers is not set.");
      }
    }

    public void Dispose()
    {
      _health.Damaged -= OnHealthChanged;
    }

    private void OnHealthChanged(float obj)
    {
      if (EnemyMeshModel != null)
      {
        ChangeMaterial();
      }
    }

    private void ChangeMaterial()
    {
      if (NewMaterial == null || TransitionMaterial == null || _skinnedMeshRenderers.Count != _originalMaterials.Count)
        Debug.LogError("SkinnedMeshRenderers, new material, or transition material is not set, or original materials list count does not match renderers list count.");

      foreach (SkinnedMeshRenderer renderer in _skinnedMeshRenderers)
        renderer.material = NewMaterial;

      if (_tween != null && _tween.active)
        _tween.Kill();

      _tween = DOVirtual.DelayedCall(DurationFirstMaterial, () =>
      {
        foreach (SkinnedMeshRenderer renderer in _skinnedMeshRenderers)
        {
          Material originalMaterial = _originalMaterials[_skinnedMeshRenderers.IndexOf(renderer)];
          renderer.material.DOColor(TransitionMaterial.color, TransitionDuration).OnComplete(() =>
          {
            renderer.material = TransitionMaterial;

            DOVirtual.DelayedCall(DurationSecondMaterial, () => { renderer.material.DOColor(originalMaterial.color, TransitionDuration).OnComplete(() => { renderer.material = originalMaterial; }); });
          });
        }
      });
    }
  }
}