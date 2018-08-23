using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private float speed = 0.5f;
    private TouchController touchController;

    void Start()
    {
        touchController = GetComponentInChildren<TouchController>();
    }

    private void MoveToDoor(Door door)
    {
        StartCoroutine(CoMoveToDoor(door));
    }

    private IEnumerator CoMoveToDoor(Door door)
    {
        touchController.enabled = false;

        Vector3 startPoint = transform.position;
        Vector3 goalPoint;
        Vector3 midPoint;

        Vector3 startAngle = NormalizedAngle(touchController.transform.rotation.eulerAngles);
        Vector3 goalAngle;

        if (Vector3.Distance(transform.position, door.FStep.position) > Vector3.Distance(transform.position, door.BStep.position))
        {
            goalPoint = door.FStep.position;
            goalAngle = NormalizedAngle(door.FStep.rotation.eulerAngles);
            midPoint = door.BStep.position;
            door.BOpen();
        }
        else
        {
            goalPoint = door.BStep.position;
            goalAngle = NormalizedAngle(door.BStep.rotation.eulerAngles);
            midPoint = door.FStep.position;
            door.FOpen();
        }

        if (goalAngle.y - startAngle.y > 180)
            goalAngle.y -= 360;
        else if (goalAngle.y - startAngle.y < -180)
            goalAngle.y += 360;
        startAngle.z = 0f;

        float startTime = 0f;

        yield return new WaitForSeconds(0.5f);
        float prevFieldOfView = Camera.main.fieldOfView;

        while (startTime < 1f)
        {
            startTime += (Time.deltaTime * speed);

            // 이동
            transform.position = BezierCurve(startTime, startPoint, midPoint, goalPoint);
            // 카메라 방향
            touchController.transform.rotation = Quaternion.Euler(BezierCurve(startTime, startAngle, goalAngle));
            // 카메라 범위
            Camera.main.fieldOfView = Mathf.Lerp(prevFieldOfView, touchController.fieldOfViewMax, startTime);

            yield return new WaitForFixedUpdate();
        }
        door.Close();

        touchController.enabled = true;
    }

    Vector3 BezierCurve(float t, Vector3 p0, Vector3 p1)
    {
        return ((1 - t) * p0) + ((t) * p1);
    }

    Vector3 BezierCurve(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Vector3 pa = BezierCurve(t, p0, p1);
        Vector3 pb = BezierCurve(t, p1, p2);
        return BezierCurve(t, pa, pb);
    }

    Vector3 NormalizedAngle(Vector3 angle)
    {
        if (angle.x < -180)
            angle.x += 360;
        else if (angle.x > 180)
            angle.x -= 360;
        if (angle.y < -180)
            angle.y += 360;
        else if (angle.y > 180)
            angle.y -= 360;

        return angle;
    }

    public void Touched(Door door)
    {
        if (door.State == DoorState.Open)
        {
            MoveToDoor(door);
        }
        else if (door.State == DoorState.Locked)
        {
            door.LockedCall();
        }
        else if (door.State == DoorState.Secret)
        {
            door.SecretCall(door);
        }
        else if (door.State == DoorState.DirLock)
        {
            door.DirLockCall(door);
        }
    }
}
