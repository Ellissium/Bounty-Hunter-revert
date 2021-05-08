using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public StateMachine state;
    public GroundedState grounding;
    public ShootingState shooting;

    [SerializeField] private FirePointPosition firePoint;
    [SerializeField] private GameObject bullet;
    public Rigidbody2D rbody;
    public Animator animator;
    

    Vector2 lastInputVector = Vector2.up;
    private bool isDiagonal = false;
    private bool noDelayStarted = false;
    private float delay = 0.05f;

    public Animator CharacterAnimator { get { return animator; } }
    public Rigidbody2D Rbody { get { return rbody; } }

    public Text timer;
    public bool record = false;
    public long seconds;


    public void Move(Vector2 inputVector, float speed)
    {
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * speed;
        rbody.velocity = new Vector2(movement.x, movement.y);
        //rbody.MovePosition(rbody.position + movement * Time.fixedDeltaTime);
        if (inputVector.magnitude > 0.1f)
        {
            animator.Play("Run");
            if (inputVector.x != 0 && inputVector.y != 0)
            {
                isDiagonal = true;
                lastInputVector = new Vector2(inputVector.x, inputVector.y);
                animator.SetFloat("Horizontal", inputVector.x);
                animator.SetFloat("Vertical", inputVector.y);
                animator.SetFloat("Magnitude", inputVector.magnitude);
            }
            else
            {
                if (isDiagonal && !noDelayStarted)
                {
                    StartCoroutine(NoMoreDiagonal());
                    noDelayStarted = true;
                }
                else
                {
                    lastInputVector = new Vector2(inputVector.x, inputVector.y);
                    animator.SetFloat("Horizontal", inputVector.x);
                    animator.SetFloat("Vertical", inputVector.y);
                    animator.SetFloat("Magnitude", inputVector.magnitude);
                }
            }
        }
        else
        {
            animator.Play("Idle");
            animator.SetFloat("Horizontal", lastInputVector.x);
            animator.SetFloat("Vertical", lastInputVector.y);
            animator.SetFloat("Magnitude", 0);
        }
    }

    public void CreateBullet()
    {
        CharacterStats.instance.CharacterShooted();
        firePoint.SetCurrentPosition(lastInputVector);
        Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Bullet>().SetBulletDirection(lastInputVector);
        AudioManager.instance.Play("PlayerShot");
    }

    public void ReceiveDamage(int damageAmount)
    {
        CharacterStats.instance.HealthPoint = CharacterStats.instance.HealthPoint - damageAmount;
        CharacterStats.instance.onHeatlhChanged();
    }

    public void OnDestroyAnimation()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    private IEnumerator NoMoreDiagonal()
    {
        yield return new WaitForSeconds(delay);
        isDiagonal = false;
        noDelayStarted = false;
    }

    private void Start()
    {
        state = new StateMachine();
        grounding = new GroundedState(gameObject, state);
        shooting = new ShootingState(gameObject, state);
        state.Initialize(grounding);
        seconds = 0;
        record = true;
        StartCoroutine(AddValueEachSecond());
    }
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private bool isPlaying = false;
    private void Update()
    {
        if (state.CurrentState != null)
        {
            state.CurrentState.HandleInput();

            state.CurrentState.LogicUpdate();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            if (!isPlaying)
            {
                isPlaying = true;
                AudioManager.instance.Play("Walk");
            } 
        } else
        {
            if (isPlaying)
            {
                AudioManager.instance.Stop("Walk");
                isPlaying = false;
            }
        }
    }
    private IEnumerator AddValueEachSecond()
    {
        while (record)
        {
            yield return new WaitForSeconds(1);
            seconds++;
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            timer.text = t.ToString(@"mm\:ss");
        }
    }
    private void FixedUpdate()
    {
        if (state.CurrentState != null)
        {
            state.CurrentState.PhysicsUpdate();
        }
    }
}
