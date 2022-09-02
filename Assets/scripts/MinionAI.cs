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
    private Entity target;

    public State state;
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
                    if (target.transform.position.x > transform.position.x) Look(1);
                    else Look(-1);
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
                        animator.SetBool("moving", false);
                        if (isRanged)
                        {

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
                        Move(moveDir.normalized);
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
