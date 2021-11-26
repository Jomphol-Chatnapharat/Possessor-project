using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float sightRange, attackRange;
    public bool canSeePlayer, canAttackPlayer;

    public float timeBetweenAttack;
    bool alreadyAttacked;

    public float speed;
    public float attackDmg;

    private Transform target;

    public LayerMask DetectionLayerMask;

    public GameObject Target;

    [field: SerializeField]
    public bool PlayerDetected { get; private set; }
    public Vector2 DirectionToTarget => target.transform.position - detectorOrigin.position;

    [Header("OverlapBox parameters")]
    [SerializeField]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f;

    private void Start()
    {
        //StartCoroutine(DetectionCoroutine());

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //private IEnumerator DetectionCoroutine()
    //{
    //    yield return new WaitForSeconds(detectionDelay);
    //    PerformDetection();
    //    StartCoroutine(DetectionCoroutine());
    //}

    //void PerformDetection()
    //{
    //    Collider2D collider = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, DetectionLayerMask);

    //    if(collider != null)
    //    {
    //        Target = collider.gameObject;
    //    }
    //    else
    //    {
    //        Target = null;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        canSeePlayer = Physics2D.OverlapBox((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, DetectionLayerMask);
        canAttackPlayer = Physics2D.OverlapCircle(transform.position, attackRange, DetectionLayerMask);

        if (!canSeePlayer && !canAttackPlayer) Patroling();
        if (canSeePlayer && !canAttackPlayer) Chasing();
        if (canSeePlayer && canAttackPlayer) Attacking();
    }

    private void Patroling()
    {

    }

    private void Chasing()
    {
        //Debug.Log("isChasing");
        if (Vector2.Distance(transform.position, target.position) > attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            var dir = target.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Attacking()
    {

        if (!alreadyAttacked)
        {
            GameObject player = GameObject.Find("Player");

            player.GetComponent<PlayerStats>().currentHp -= attackDmg;


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttack);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        if (PlayerDetected)
            Gizmos.color = Color.red;
        Gizmos.DrawCube((Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize);

    }
}
