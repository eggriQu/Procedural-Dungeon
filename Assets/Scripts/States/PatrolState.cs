using System.Collections;
using UnityEngine;

public interface IState
{
    void Handle();
}

public abstract class State : IState
{
    protected EnemyAI controller;

    public State (EnemyAI controller)
    {
        this.controller = controller;
    }

    public abstract void Handle();
}

public class PatrolState : State
{
    private float xValue = Random.Range(-7, 7);
    private float zValue = Random.Range(-7, 7);

    public PatrolState(EnemyAI controller) : base(controller)
    {

    }

    public override void Handle()
    {
        controller.rb.linearVelocity = new Vector3(xValue, 0, zValue);
    }
}
