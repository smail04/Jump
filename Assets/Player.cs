using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Idle,
    Charge,
    Flying
}

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float kickForceMultiplier = 3;
    public PlayerState state;
    [SerializeField] private Rope rope;
    [SerializeField] private TrajectorySimulation trajectory;
    private Rigidbody _rigidbody;

    private void Start()
    {
        Application.targetFrameRate = 60;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint point in collision.contacts)
        {
            if (Vector3.Dot(point.normal, Vector3.up) > 0.9f)
            {
                SetState(PlayerState.Idle);
                return;
            }
        }
    }

    void OnCollisionExit(Collision other)
    {
        foreach (ContactPoint point in other.contacts)
        {
            if (Vector3.Dot(point.normal, Vector3.up) > 0.9f)
            {
                SetState(PlayerState.Idle);
                return;
            }
        }
        SetState(PlayerState.Flying);
    }

    private void Update()
    {
        switch (state)
        {
            case PlayerState.Idle:
                {
                    break;
                }
            case PlayerState.Charge:
                {
                    break;
                }
            case PlayerState.Flying:
                {
                    break;
                }
            default:
                break;
        }
    }

    private void SetState(PlayerState newState)
    {
        state = newState;
        
        switch (state) 
        {
            case PlayerState.Idle:
                { 
                    break; 
                }
            case PlayerState.Charge:
                { 
                    break; 
                }
            case PlayerState.Flying:
                {
                    break;
                }
            default:
                break;
        }

    }

    public void Kick(Vector3 direction, float force = 1f)
    {
        _rigidbody.AddForce(direction * force, ForceMode.VelocityChange);
    }

    public void Kick()
    {
        Vector3 direction = (transform.position - rope.endPosition).normalized;
        Kick(direction, Vector3.Distance(transform.position, rope.endPosition) * kickForceMultiplier);
    }

    public void ShowTrajectory()
    {
        trajectory.SimulatePath(gameObject, (transform.position - rope.endPosition).normalized
                                            * Vector3.Distance(transform.position, rope.endPosition)
                                            * kickForceMultiplier);
        SetState(PlayerState.Charge); 
    }

    
}
