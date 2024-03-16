using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachines stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastpos;

    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }
    public Vector3 Lastpos { get => lastpos; set => lastpos = value; }
    [SerializeField] private string currentState;
    public Path1 path;

    [Header("Sight Values")]
    public float sightDistance = 10f;
    public float fieldOfView = 90f;
    public float eyeHeight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10)] public float fireRate;
    public float sphereCastRadius = 1f;

    void Start()
    {
        stateMachine = GetComponent<StateMachines>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player != null)
        {
            CanSeePlayer();
        }
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position - (Vector3.up * eyeHeight);
        float angleToPlayer = Vector3.Angle(directionToPlayer, transform.forward);
        //Debug.Log("nested1");
        if (angleToPlayer <= fieldOfView)
        {
            //Debug.Log("nested2");
            RaycastHit hit;
            if (Physics.Raycast(transform.position + (Vector3.up * eyeHeight), directionToPlayer, out hit, sightDistance))
            {
                //Debug.Log("nested3");
                if (hit.collider.CompareTag("Player"))
                {
                    //Debug.Log("CanSeePlayer");
                    Debug.DrawLine(transform.position + (Vector3.up * eyeHeight), player.transform.position, Color.green);
                    return true;
                }
            }
        }

        Debug.DrawLine(transform.position + (Vector3.up * eyeHeight), player.transform.position, Color.red);
        return false;
    }
}
