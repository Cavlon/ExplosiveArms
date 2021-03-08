using System.Collections;
using System.Collections.Generic;

public abstract class State
{
    protected Enemy enemy;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(Enemy enemy)
    {
        this.enemy = enemy;
    }
}
