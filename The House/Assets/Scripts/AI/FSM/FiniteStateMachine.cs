using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//define namespace FSM
namespace FSM
{
    //Finite state machine class
    public class FiniteStateMachine<T>
    {
        //currentState
        public State<T> currentState { get; private set; }
        //potential previous state

        //owner of state
        public T owner;

        //initialize 
        public FiniteStateMachine(T o)
        {
            currentState = null;
            owner = o;
        }

        //change state
        public void ChangeState(State<T> newState)
        {
            //if there is a current state
            if (currentState != null)
                //exit the current state
                currentState.ExitState(owner);
            //make current state the new state
            currentState = newState;
            //enter into the new current state
            currentState.EnterState(owner);

        }

        //update
        public void Update()
        {
            currentState.UpdateState(owner);
        }
    }
}

//state functions
public abstract class State<T> 
{
    public string stateName { get; protected set; }

    public abstract void EnterState(T owner);
    public abstract void UpdateState(T owner);
    public abstract void ExitState(T owner);

}

