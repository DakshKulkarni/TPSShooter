using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyBaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    public float waitBeforeSearchTime = 7f;
    public float bulletSpeed = 40f;
    private float shotTimer;
    public AudioSource audioSource;

    public override void Enter()
    {
        losePlayerTimer = 0;
    }

    public override void Exit()
    {
        moveTimer = 0;
        losePlayerTimer = 0;
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(4, 10))
            {
                enemy.Agent.SetDestination(enemy.transform.position + Random.insideUnitSphere * 6);
                moveTimer = 0;
            }
            enemy.Lastpos = enemy.Player.transform.position;
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > waitBeforeSearchTime)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        if (stateMachine.bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not assigned in the StateMachines.");
            return;
        }

        Transform gunBarrel = enemy.gunBarrel;
        if (gunBarrel == null)
        {
            Debug.LogError("Gun barrel transform is not assigned to the enemy.");
            return;
        }
        audioSource.Play();

        GameObject bullet = GameObject.Instantiate(stateMachine.bulletPrefab, gunBarrel.position, enemy.transform.rotation);
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-2f, 2f), Vector3.up) * shootDirection * bulletSpeed;
        shotTimer = 0;
    }
}
