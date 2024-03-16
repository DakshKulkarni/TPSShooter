using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachines : MonoBehaviour
{
    public EnemyBaseState activeState;
    public GameObject bulletPrefab;
    public AudioSource audioSource;
    public void Initialise()
    {
        ChangeState(new PatrolState());
    }
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState!=null)
        {
            activeState.Perform();
        }
    }
    public void ChangeState(EnemyBaseState newState)
    {
        if(activeState!=null)
        {
            activeState.Exit();
        }
        activeState=newState;
        if(activeState!=null)
        {
            activeState.stateMachine=this;
            activeState.enemy=GetComponent<Enemy>();
            if(newState is AttackState)
            {
                ((AttackState)newState).audioSource=audioSource;
            }
            activeState.Enter();
        }
    }
}
