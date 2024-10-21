using System.Collections.Generic;
using Core.Characters.FiniteStateMachines;
using UnityEngine.AI;

namespace Core.Characters.Companions.States
{
  public class CompanionBootstrapState : State
  {
    private readonly NavMeshAgent _navMeshAgent;
    
    public CompanionBootstrapState(List<Transition> transitions, NavMeshAgent navMeshAgent) : base(transitions)
    {
      _navMeshAgent = navMeshAgent;
    }

    public override void Enter()
    {
      _navMeshAgent.enabled = true;
    }

    protected override void OnTick()
    {
    }

    public override void Exit()
    {
    }
  }
}