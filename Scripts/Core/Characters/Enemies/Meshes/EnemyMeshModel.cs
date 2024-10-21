using System.Collections.Generic;
using UnityEngine;

namespace Core.Characters.Enemies
{
  public class EnemyMeshModel : MonoBehaviour
  {
    public List<SkinnedMeshRenderer> Meshes;
    
    [Tooltip("Материал, на который нужно поменять")]
    public Material NewMaterial;

    [Tooltip("Промежуточный материал")] 
    public Material TransitionMaterial;

    [Tooltip("Время, сколько будет первый материал")]
    public float DurationFirstMaterial;

    [Tooltip("Время перехода ко второму материалу")]
    public float TransitionDuration;

    [Tooltip("Время, сколько будет второй материал")]
    public float DurationSecondMaterial;
  }
}