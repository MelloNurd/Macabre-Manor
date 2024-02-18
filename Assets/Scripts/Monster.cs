using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject patrolPointsObj;

    [SerializeField] private Vector3[] positions;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        positions = new Vector3[patrolPointsObj.transform.childCount];
        index = 0;
        foreach(Transform t in patrolPointsObj.GetComponentsInChildren<Transform>()) {
            if(t == patrolPointsObj.transform) continue;
            positions[index++] = t.position;
        }

        agent.SetDestination(positions[Random.Range(0, positions.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == agent.destination) {
            agent.SetDestination(positions[Random.Range(0, positions.Length)]);
        }
    }

    public void SetDestination(GameObject position) {
        agent.SetDestination(positions[System.Array.IndexOf(positions, position.transform.position)]);
    }
}
