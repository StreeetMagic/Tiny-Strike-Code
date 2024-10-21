using UnityEngine;

namespace Core.Characters.Companions
{
  [SelectionBase]
  public class Companion : MonoBehaviour
  {
    [field: SerializeField] public CompanionInstaller Installer { get; private set; }
  }
}