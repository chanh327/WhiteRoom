/// Sourced from
/// Pinch to Zoom: https://unity3d.com/kr/learn/tutorials/topics/mobile-touch/pinch-zoom
/// Rotate Camera: https://answers.unity.com/questions/805630/

using UnityEngine;

public class TouchController : MonoBehaviour
{
    private Camera mainCamera;
    private float eyeline;

    Vector3 firstPoint;
    Vector3 secondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    float perspectiveZoomSpeed; // The rate of change of the field of view in perspective mode.
    float fieldOfViewMin;
    float fieldOfViewMax;

    Quaternion rot;

    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);

        perspectiveZoomSpeed = 0.1f;
        fieldOfViewMin = 30f;
        fieldOfViewMax = 50f;
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.point);
            }

            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * 2, -Input.GetAxis("Mouse X") * 3, 0));
            Camera.main.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

            Debug.Log(this.transform.rotation.eulerAngles);
        }
#elif UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Rotate();
        }
        else if (Input.touchCount == 2)
        {
            PinchZoom();
        }
        this.transform.rotation = rot;
#endif
    }

    private void Touch(GameObject o)
    {

    }

    private void Rotate()
    {
        Touch touch = Input.GetTouch(0);

        // 화면 회전
        if (touch.phase == TouchPhase.Began)
        {
            firstPoint = touch.position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
        }
        if (touch.phase == TouchPhase.Moved)
        {
            secondPoint = touch.position;
            xAngle = xAngleTemp + (secondPoint.x - firstPoint.x) * 180 / Screen.width;
            yAngle = yAngleTemp + (secondPoint.y - firstPoint.y) * 90 / Screen.height;

            // 화면 제한
            float angleLimit = (fieldOfViewMax - Camera.main.fieldOfView) * 0.5f;
            yAngle = Mathf.Clamp(yAngle, -angleLimit, angleLimit);

            rot = Quaternion.Euler(yAngle, -xAngle, 0f);

            //this.transform.rotation = Quaternion.Euler(yAngle, -xAngle, 0f);
        }
    }

    private void PinchZoom()
    {
        // Store both touches.
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        // Find the position in the previous frame of each touch.
        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        // Find the magnitude of the vector (the distance) between the touches in each frame.
        float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

        // Find the difference in the distances between each frame.
        float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

        // Change the field of view based on the change in distance between the touches.
        Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

        // Clamp the field of view to make sure it's between 0 and 180.
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, fieldOfViewMin, fieldOfViewMax);

        // 화면 제한
        Vector3 angle = this.transform.rotation.eulerAngles;
        if (180f < angle.x)
        {
            angle.x -= 360f;
        }
        if (180 < angle.y)
        {
            angle.y -= 360f;
        }

        float angleLimit = (fieldOfViewMax - Camera.main.fieldOfView) * 0.5f;
        angle.x = Mathf.Clamp(angle.x, -angleLimit, angleLimit);

        rot = Quaternion.Euler(angle);
        //this.transform.rotation = Quaternion.Euler(angle);
    }

    public void SetEyeline()
    {

    }
}
