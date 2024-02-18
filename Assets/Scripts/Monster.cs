using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    NavMeshAgent agent;

    public int lookAngle = 80;

    public GameObject patrolPointsObj;

    [SerializeField] private Vector3[] positions;

    public bool canSeePlayer;
    public bool isChasingPlayer;
    bool isTryingLoss;

    Player player;

    Coroutine coroutineReference;

    private void Start() {
        player = FindAnyObjectByType<Player>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();

        positions = new Vector3[patrolPointsObj.transform.childCount];
        int index = 0;
        foreach(Transform t in patrolPointsObj.GetComponentsInChildren<Transform>()) {
            if(t == patrolPointsObj.transform) continue;
            positions[index++] = t.position;
        }

        agent.SetDestination(positions[Random.Range(0, positions.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == agent.destination && !isChasingPlayer) { // Patrolling
            SetRandomDestination();
            return;
        }

        if (CheckForPlayer()) {
            isChasingPlayer = true;
            if (isTryingLoss) {
                StopCoroutine(coroutineReference);
                isTryingLoss = false;
            }
        }
        else {
            if (isChasingPlayer && !isTryingLoss) {
                coroutineReference = StartCoroutine(TryLosePlayer());
            };
        }

        if (isChasingPlayer) agent.destination = player.transform.position;
    }

    public bool CheckForPlayer() {
        Vector3 direction = player.transform.position - (transform.position + Vector3.up / 2);
        if (Physics.Raycast(transform.position + Vector3.up / 2, direction, out var hit, 30, ~LayerMask.GetMask("Monster"))) {
            Debug.DrawLine(transform.position + Vector3.up / 2, hit.point, UnityEngine.Color.white);
            return hit.collider.gameObject == player.gameObject && Vector3.Angle(transform.forward, direction) <= lookAngle || Vector3.Distance(transform.position, player.transform.position) < 5;
        }
        return false;
    }

    public void SetDestination(GameObject position) {
        agent.SetDestination(positions[System.Array.IndexOf(positions, position.transform.position)]);
    }

    public void SetRandomDestination() {
        agent.SetDestination(positions[Random.Range(0, positions.Length)]);
    }

    IEnumerator TryLosePlayer() {
        isTryingLoss = true;
        yield return new WaitForSeconds(4);
        if(!CheckForPlayer() && isTryingLoss) {
            SetRandomDestination();
            isChasingPlayer = false;
        }
        isTryingLoss = false;
    }
}
