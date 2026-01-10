using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ChaseState : State
{
    public ChaseState(EnemyAI controller) : base(controller)
    {

    }

    public override void Handle()
    {
        controller.rb.linearVelocity = controller.followDirection.normalized * 6;
    }
}
