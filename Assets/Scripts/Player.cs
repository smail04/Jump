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
        if (CheckGround(collision))
        {
            SetState(PlayerState.Idle);
            return;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CheckGround(collision))
        {
            SetState(PlayerState.Idle);
            return;
        }
        SetState(PlayerState.Flying);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (CheckGround(collision))
        {
            SetState(PlayerState.Idle);
            return;
        }
    }

    private bool CheckGround(Collision collision)
    {
        Platform platform = collision.gameObject.GetComponent<Platform>();
        if (platform && platform.Stable)
            foreach (ContactPoint point in collision.contacts)
            {
                if (Vector3.Dot(point.normal, Vector3.up) > 0.9f)
                {
                    return true;
                }
            }
        return false;
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
        SetState(PlayerState.Idle);
    }

    public void Kick()
    {
        Vector3 direction = (transform.position - rope.endPosition).normalized;
        Kick(direction, Vector3.Distance(transform.position, rope.endPosition) * kickForceMultiplier);
    }

    public void Die()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowTrajectory()
    {
        trajectory.SimulatePath(gameObject, (transform.position - rope.endPosition).normalized
                                            * Vector3.Distance(transform.position, rope.endPosition)
                                            * kickForceMultiplier);
        SetState(PlayerState.Charge); 
    }

    
}
