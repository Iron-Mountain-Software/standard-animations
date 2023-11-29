# State Machines
Base classes to create custom state machines.

## Package Mirrors:
[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODg3LnBuZw==/original/npRUfq.png'>](https://github.com/Iron-Mountain-Software/state-machines.git)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODkyLnBuZw==/original/Fq0ORM.png'>](https://www.npmjs.com/package/com.iron-mountain.state-machines)[<img src='https://img.itch.zone/aW1nLzEzNzQ2ODk4LnBuZw==/original/Rv4m96.png'>](https://iron-mountain.itch.io/state-machines)
## Key Scripts & Components:
1. public interface **IState**
   * Methods: 
      * public abstract Boolean ***CanTransitionTo***(IState state)
      * public abstract void ***Enter***()
      * public abstract void ***Exit***()
1. public interface **IStateMachine`1**
   * Properties: 
      * public T ***PreviousState***  { get; set; }
      * public T ***CurrentState***  { get; set; }
   * Methods: 
      * public abstract Boolean ***RequestState***(T requestedState)
      * public abstract void ***RequestPreviousState***()
      * public abstract void ***ChangeToState***(T newState)
1. public abstract class **ScriptedState** : ScriptableObject
   * Methods: 
      * public virtual Boolean ***CanTransitionTo***(IState state)
      * public abstract void ***Enter***()
      * public abstract void ***Exit***()
1. public abstract class **State** : Object
   * Methods: 
      * public virtual Boolean ***CanTransitionTo***(IState state)
      * public abstract void ***Enter***()
      * public abstract void ***Exit***()
1. public class **StateMachine`1** : Object
   * Actions: 
      * public event Action ***OnStateChange*** 
   * Properties: 
      * public T ***PreviousState***  { get; set; }
      * public T ***CurrentState***  { get; set; }
      * public Type ***PreviousStateType***  { get; set; }
      * public Type ***CurrentStateType***  { get; set; }
      * public float ***TimeOfLastStateChange***  { get; }
      * public float ***SecondsSinceLastStateChange***  { get; }
      * public float ***UnscaledTimeOfLastStateChange***  { get; }
      * public float ***UnscaledSecondsSinceLastStateChange***  { get; }
   * Methods: 
      * public virtual Boolean ***RequestState***(T requestedState)
      * public virtual void ***RequestPreviousState***()
      * public virtual void ***ChangeToState***(T newState)
      * public StateMachine`1 ***Initialize***(T initialState)
1. public abstract class **StateRequestButton** : MonoBehaviour
   * Methods: 
      * public abstract void ***OnClick***()
