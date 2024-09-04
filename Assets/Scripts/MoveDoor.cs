using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;



public class MoveDoor : MonoBehaviour
{
    public enum DoorStates
    {
        Opened,
        Closed,
        Opening,
        Closing,
        Locked,
    }

    public Vector3 openingDirection = new Vector3(0f, -2f, 0f);

    public float waitTime = 5f;
    private float _startOfWait = 0f;

    public float speed = 5f;
    public bool unlockDoor = false;
    public bool doorCycle = true;

    private Vector3 _closedPosition = Vector3.zero;
    private Vector3 _openPosition = Vector3.zero;

    public DoorStates state = DoorStates.Closed;

    void Start()
    {
        _closedPosition = transform.position;
        _openPosition = transform.position + openingDirection;
    }

    void Update()
    {
        switch (state)
        {
            case DoorStates.Closed:
                if (waitTime < Time.time - _startOfWait)
                {
                    state = DoorStates.Opening;
                }
                break;

            case DoorStates.Opened:
                if (doorCycle)
                {
                    if (waitTime < Time.time - _startOfWait)
                    {
                        state = DoorStates.Closing;
                    }
                }

                break;

            case DoorStates.Closing:
                transform.position = Vector3.MoveTowards(transform.position, _closedPosition, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _closedPosition) < 0.01f)
                {
                    state = DoorStates.Closed;
                    _startOfWait = Time.time;
                }
                break;

            case DoorStates.Opening:
                transform.position = Vector3.MoveTowards(transform.position, _openPosition, speed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _openPosition) < 0.01f)
                {
                    state = DoorStates.Opened;
                    _startOfWait = Time.time;
                }
                break;

            case DoorStates.Locked:
                if (unlockDoor)
                {
                    state = DoorStates.Opening;
                }
                break;
            default:
                break;
        }
    }
}
