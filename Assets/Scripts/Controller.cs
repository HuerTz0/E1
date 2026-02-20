using UnityEngine;
using Pathfinding;

public class Controller : MonoBehaviour
{
    [SerializeField] Transform goal;
    AILerp ai;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ai = GetComponent<AILerp>();
    }

    // Update is called once per frame
    void Update()
    {
        ai.destination = goal.position;
    }
}
