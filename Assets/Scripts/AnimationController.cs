using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Gui;
using UnityEngine.AI;
public class AnimationController : MonoBehaviour
{
    [SerializeField] public Animator anim;

    [SerializeField] public AnimationState state;
    [SerializeField] private AnimationType animType;
    private Rigidbody rb;
    private NavMeshAgent cc;
    private LeanJoystick jstick;
    private enum AnimationType { 
        Player, Enemy
    }
    public enum AnimationState { 
        Idle, Walk, Atack, Iteract
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (animType is AnimationType.Player) {
            jstick = FindObjectOfType<LeanJoystick>();
        }
        if (animType is AnimationType.Enemy) {
            cc = GetComponent<NavMeshAgent>();
        }
    }

    
    void Update()
    {
        if (animType is AnimationType.Player)
        {
            //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            Vector3 move = new Vector3(jstick.ScaledValue.x, 0, jstick.ScaledValue.y);
            if (move.magnitude > 0 && state is AnimationState.Idle)
            {
                state = AnimationState.Walk;
                StartRunning();
            }
            else if (move.magnitude == 0 && state is AnimationState.Walk)
            {
                state = AnimationState.Idle;
                StartRunning();
            }

            
        }

        if (animType is AnimationType.Enemy) {
            if (cc.velocity.magnitude >= 0.01f && state is AnimationState.Idle) {
                state = AnimationState.Walk;
                StartRunning();
            } else if (cc.velocity.magnitude <= 0.1f && state is AnimationState.Walk)
            {
                state = AnimationState.Idle;
                StartRunning();
            }
        }

        anim.SetInteger("State", (int)state);
    }


   private void StartRunning()
    {
        anim.SetTrigger("ChangeState");
    }

    public void Atack() {
        RandomizeAtack();
        anim.SetTrigger("Atack");
    }

    private void RandomizeAtack() {
        anim.SetInteger("AtackType", Random.Range(1, 3));
    }

    public void Panic() {
        anim.SetInteger("EmotionType", 1);
        anim.SetTrigger("Emotion");
    }

    public void Die() {
        anim.SetTrigger("Die");
    }

    public void GetDamage() {
        RandomizeDamage();
        anim.SetTrigger("GetHit");
    }

    private void RandomizeDamage()
    {
        anim.SetInteger("GetHitType", Random.Range(0, 2));
    }
}
