using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyShootingState : State
{
    public Transform player;

    public int layerMask = 1 << 8;

    public float pursuitDistance = 1.5f;
    public float shootingDistance = 0.5f;

    public Vector2 followPoint;
    public Vector2 xCathetus;
    public Vector2 yCathetus;

    public bool followCompleted = false;
    public bool isCoroutineExist = false;
    public bool wasShot;

    public EnemyShootingState(GameObject entity, StateMachine stateMachine) : base(entity, stateMachine) { }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
   /*     AnimatorStateInfo info = character.CharacterAnimator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= 0.6f && !wasShot && info.IsName("Shoot"))
        {
            wasShot = true;
            character.CreateBullet();
        }
        if (character.CharacterAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            character.state.ChangeState(character.grounding);
        }*/
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        rayCast();
    }

    public virtual void ChangePointPosition()
    {
       
    }

    public IEnumerator ChangeFollow()
    {
        if (isCoroutineExist) yield break;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        ChangePointPosition();
        isCoroutineExist = false;
    }

    public virtual void rayCast()
    {
        
    }

    public virtual void ChooseAxis()
    {
        
    }

    public virtual void ChooseAxisFromOne()
    {
        
    }

    public virtual void MoveToAxisX()
    {
        Shoot();
    }

    public virtual void MoveToAxisY()
    {
        Shoot();
    }

    public virtual void Shoot()
    {

    }

    public virtual void FollowOnEachAxis()
    {
       
    }
}
