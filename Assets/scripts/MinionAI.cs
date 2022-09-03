using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    idle,
    chase,
    attack
}

public class MinionAI : Entity
{

    [Header("Combat Agent")]

    public State state;
    public Transform prayer;

    Vector2 moveDir;

    private void Update()
    {
        switch (state)
        {
            case State.idle:
                target = getClosestEntity();
                if(target != null)
                {
                    state = State.chase;
                    break;
                }
                Stop();
                animator.SetBool("moving", false);
                break;

            case State.chase:
                if (!canMove) break;

                if(target == null)
                {
                    state = State.idle;
                    break;
                }

                if(!CheckInRange(target.transform.position , attackRange))
                {
                    moveDir = target.transform.position - transform.position;
                    animator.SetBool("moving", true);
                    Move(moveDir.normalized);
                    Look(target.transform.position.x - transform.position.x);
                }
                else
                {
                    state = State.attack;
                }
                break;

            case State.attack:
                if (!canAttack) break;

                if (target == null)
                {
                    state = State.idle;
                    break;
                }

                if (CheckInRange(target.transform.position, attackRange) )
                {
                    if(attackCounter < Time.time)
                    {
                        Stop();
                        animator.SetBool("moving", false);
                        if (isRanged)
                        {
                            CreateProjectile(target);
                        }
                        else
                        {
                            Attack(target);
                        }

                        Debug.Log("attacked");
                        attackCounter = attackRate + Time.time;

                        moveDir = getRandomDir().normalized;
                        Look(moveDir.x);
                    }

                    if(attackCounter > Time.time)
                    {
                        animator.SetBool("moving", true);
                        if (CheckObstacle(moveDir)) moveDir = getRandomDir().normalized;
                        Move(moveDir.normalized);
                        Look(moveDir.x);

                    }
                }
                else
                {
                    state = State.chase;
                }


                break;
        }       
    }
}
