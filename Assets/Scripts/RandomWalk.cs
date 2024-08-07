using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class RandomWalk : MonoBehaviour
{
    public float _Range = 25.0f;
    NavMeshAgent _Agent;

    void Start()
    {
        _Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // && AND
        // || OR

        //If AI is still moving
        if (_Agent.pathPending || !_Agent.isOnNavMesh || _Agent.remainingDistance > 0.1f)
        {
            //Exit function (update) here
            return;
        }

        //Choose a random point
        Vector3 randomPosition = _Range * Random.insideUnitCircle;
        randomPosition = new Vector3(randomPosition.x, 0, randomPosition.y);
        _Agent.destination = transform.position + randomPosition;
    }
}
