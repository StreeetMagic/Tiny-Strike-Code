namespace Core.Characters.Enemies
{
  public class EnemyAnimatorProvider
  {
    private EnemyAnimatorController _instance;

    public EnemyAnimatorController Instance
    {
      get
      {
        if (!_instance)
           throw new System.NullReferenceException(); 
        
        return _instance;
      }
      
      set => _instance = value;
    }
  }
}