public abstract class State
{
    //protected EnemyController enemy;

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    /*public State(EnemyController enemy)
    {
        this.enemy = enemy;
    }*/
}
