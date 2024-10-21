using Meta.Currencies;
using SaveLoadServices;
using Utilities;

namespace Meta
{
  public abstract class Quest
  {
    protected readonly CurrencyStorage CurrencyStorage;
    protected readonly ISaveLoadService SaveLoadService;

    protected Quest(CurrencyStorage currencyStorage, ISaveLoadService saveLoadService)
    {
      CurrencyStorage = currencyStorage;
      SaveLoadService = saveLoadService;
    }
    
    public ReactiveProperty<QuestState> State { get; protected set; }
  }
}