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

    Player player;

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
        if (transform.position == agent.destination) SetRandomDestination();
        Debug.Log(CheckForPlayer());
    }

    public bool CheckForPlayer() {
        Vector3 direction = player.transform.position - (transform.position + Vector3.up / 2);
        if (Physics.Raycast(transform.position + Vector3.up / 2, direction, out var hit, 30, ~LayerMask.GetMask("Monster"))) {
            Debug.DrawLine(transform.position + Vector3.up / 2, hit.point, UnityEngine.Color.white);
            return hit.collider.gameObject == player.gameObject && Vector3.Angle(transform.forward, direction) <= lookAngle;
        }
        return false;
    }

    public void SetDestination(GameObject position) {
        agent.SetDestination(positions[System.Array.IndexOf(positions, position.transform.position)]);
    }

    public void SetRandomDestination() {
        agent.SetDestination(positions[Random.Range(0, positions.Length)]);
    }
}
