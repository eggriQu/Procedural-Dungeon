using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ChaseState : State
{
    public ChaseState(EnemyAI controller) : base(controller)
    {

    }

    public override void Handle()
    {
        //controller.rb.linearVelocity = controller.followDirection.normalized * 6;
        controller.rb.linearVelocity = new Vector3(controller.followDirection.x, 0, controller.followDirection.z).normalized * 6;
    }
}
