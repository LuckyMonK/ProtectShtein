using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;
[Serializable]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyState state;
    [SerializeField] private CharacterController cc;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform target;
    [SerializeField] private Slider HP;
    public float _hp;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private AnimationController animController;
    private Quaternion qTo;
    private enum EnemyState {
        Search,
        Atack
    }

    public enum EnemyType { 
        Knight,
        Sceleton,
        Orc
    }

    private Coroutine logic;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animController = GetComponent<AnimationController>();
        InitHP();
        target = FindObjectOfType<MainBuild>().transform;

        agent = GetComponent<NavMeshAgent>();
        logic = StartCoroutine(EnemyLogic());

    }

    private IEnumerator EnemyLogic() {
        agent.updatePosition = true;
        agent.SetDestination(target.position);
        while (state is EnemyState.Search) {
            yield return null;
        }
        agent.isStopped = true;

        while (state is EnemyState.Atack)
        {
            Atack();
            yield return new WaitForSeconds(UnityEngine.Random.Range(2.5f, 3.2f));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<MainBuild>()) {
            agent.isStopped = true;
            state = EnemyState.Atack;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.GetComponent<Player>()) {
        //    StopCoroutine(logic);
        //    StartCoroutine(TrollPanic());
        //}
    }

    private IEnumerator TrollPanic() {
        rb.velocity = Vector3.zero;
        animController.Panic();
        float timer = UnityEngine.Random.Range(1f, 3f);
        yield return new WaitForSeconds(timer);
        logic = StartCoroutine(EnemyLogic());
    }

    private void Atack() {
        animController.Atack();
    }

    public void DamageFromPlayer(int damage, List<Enemy> list) {
        _hp -= damage;
        UpdateHP(list);
    }

    public void DamageFromTower(float damage)
    {
        _hp -= damage;
        UpdateHP();
    }


    private void UpdateHP(List<Enemy> list) {
        if (_hp <= 0) {
            list.Remove(this);
            AtackBtn.Instantiate.UpdateState(list.Count);
            Death();
        }

        HP.value = _hp;
        animController.GetDamage();
    }

    private void UpdateHP()
    {
        if (_hp <= 0)
        {
            Death();
        }

        HP.value = _hp;
        animController.GetDamage();
    }

    private void InitHP() {
        HP.maxValue = _hp;
        HP.value = _hp;
    }

    private void Death() {
        StartCoroutine(DieWaiting());
    }

    private IEnumerator DieWaiting() {
        animController.Die();
        GetComponent<Collider>().enabled = false;
        while (!animController.anim.GetCurrentAnimatorStateInfo(0).IsName("Die")) {
            yield return null;
        }
        yield return new WaitForSeconds(animController.anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void TestMethod() {
        Debug.Log(1);
    }
}
