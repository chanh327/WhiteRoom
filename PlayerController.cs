using UnityEngine;
using System.Collections;

delegate IEnumerator RotateAndMove(Vector3 v);
public class PlayerController: MonoBehaviour
{
    private GameObject player;
    private bool hasKey;
    private bool moving;
    private bool rotating;
    private Vector3 direction;
    private Quaternion lookRotation;
    private Vector3 destination;
    private RotateAndMove rotateAndMove;

    private float movingSpeed = 1.5f;
    private float rotationSpeed = 40f;
    public Door tempDoor;


    public PlayerController()
    {
        hasKey = false;
        moving = false;
        rotating = false;
    }

	void Start()
	{
        player = gameObject;
        destination = gameObject.transform.position;
        StartCoroutine(CoMoveToDoor(tempDoor));
	}

    void FixedUpdate()
	{
        if(moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * movingSpeed);
        }

        if(rotating)
        {
            direction = (destination - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
		
	}

    IEnumerator CoMoveTo(Vector3 point)
    {
        moving = true;
        destination = point;
        while(Vector3.Distance(transform.position,destination) > 0.1f)
        {
            yield return new WaitForFixedUpdate();
        }
        Debug.Log(transform.position);
        moving = false;
    }
    IEnumerator CoRotateTo(Vector3 point)
    {
        rotating =true;
        destination = point;
        yield return new WaitForFixedUpdate();
        while(Quaternion.Angle(transform.rotation, lookRotation) != 0)
        {
            yield return new WaitForFixedUpdate();
        }
        rotating = false;
    }
    public IEnumerator CoMoveToDoor(Door door) 
    {
        Vector3 height = new Vector3(0,transform.position.y,0);
        Vector3 intermediatePoint = door.DoorTransform.position + height;
        Vector3 goalPoint =((Vector3.Distance(transform.position, door.FStep.position) > Vector3.Distance(transform.position, door.BStep.position)) ? door.FStep.position : door.BStep.position);

        door.Open();
        yield return new WaitForSeconds(1f);

        StartCoroutine(CoRotateTo(intermediatePoint));
        yield return StartCoroutine(CoMoveTo(intermediatePoint));

        StartCoroutine(CoRotateTo(goalPoint));
        yield return StartCoroutine(CoMoveTo(goalPoint));
    }

    public bool HasKey
    {
        get { return hasKey; }
        set
        {
            if (hasKey != value)
                hasKey = value;
        }
    }
}
