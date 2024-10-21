using AudioServices.Sounds;
using Core.Characters.Enemies;
using VisualEffects;

namespace ConfigProviders
{
  public class EnemyVisualsProvider
  {
    private readonly ArtConfigProvider _art;

    public EnemyVisualsProvider(ArtConfigProvider artConfigProvider)
    {
      _art = artConfigProvider;
    }

    public VisualEffectId MuzzleFlash(EnemyId id) => _art.EnemyVisuals[Effect(id)].MuzzleFlashId;
    public VisualEffectId Bullet(EnemyId id) => _art.EnemyVisuals[Effect(id)].Bullet;
    public VisualEffectId Impact(EnemyId id) => _art.EnemyVisuals[Effect(id)].Impact;
    public VisualEffectId Panic(EnemyId id) => _art.EnemyVisuals[Effect(id)].Panic;
    
    public SoundId AttackSound(EnemyId id) => _art.EnemyVisuals[Effect(id)].AttackSound;
    public SoundId ReloadSound(EnemyId id) => _art.EnemyVisuals[Effect(id)].ReloadSound;

    private EnemyVisualId Effect(EnemyId id) => _art.Enemies[id].VisualEffectsSetupId;
  }
}