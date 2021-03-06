/// Sourced from
/// Pinch to Zoom: https://unity3d.com/kr/learn/tutorials/topics/mobile-touch/pinch-zoom
/// Rotate Camera: https://answers.unity.com/questions/805630/

using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour
{
    private PlayerController player;

    Vector3 angle;

    float angleRotateSpeed;
    Vector3 firstPoint;
    Vector3 secondPoint;
    float xAngle;
    float yAngle;
    float xAngleTemp;
    float yAngleTemp;

    float perspectiveZoomSpeed; // The rate of change of the field of view in perspective mode.
    public float fieldOfViewMin;
    public float fieldOfViewMax;

    bool touchCheck;
    bool rotateCheck;
    Vector3 touchPoint0;
    Vector3 touchPoint1;

    public static int howManyTouch;
    private Vector3 angleTemp;

    void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }

    void Start()
    {
        angle = Vector3.zero;

        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);

        angleRotateSpeed = 0.5f;
        perspectiveZoomSpeed = 0.04f;
        fieldOfViewMin = 30f;
        fieldOfViewMax = 55f;
        Camera.main.fieldOfView = fieldOfViewMax;

        touchCheck = false;
        rotateCheck = false;

        howManyTouch = 0;
    }

    void LateUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Door"))
                {
                    player.Touched(hit.transform.parent.gameObject.transform.parent.GetComponent<Door>());
                }
                else if (hit.transform.CompareTag("Item"))
                {
                    //Debug.Log("Item Touched");
                    hit.transform.GetComponent<Button>().Touched();
                }
            }

            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * 2, -Input.GetAxis("Mouse X") * 3, 0));
            Camera.main.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            RaycastTouch();
            RotateAndPinchZoom();
        }
#endif
    }

    private void RotateAndPinchZoom()
    {
        Touch touchZero = Input.GetTouch(0);

        if (Input.touchCount == 2)
        {
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

            if (touchZero.phase == TouchPhase.Ended)
            {
                firstPoint = touchOne.position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            if (touchOne.phase == TouchPhase.Ended)
            {
                firstPoint = touchZero.position;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
        }

        // 화면 회전
        if (touchZero.phase == TouchPhase.Began)
        {
            firstPoint = touchZero.position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
        }
        if (Input.touchCount == 1 && touchZero.phase == TouchPhase.Moved)
        {
            secondPoint = touchZero.position;
            xAngle = xAngleTemp + (secondPoint.x - firstPoint.x) * 180 / Screen.width;
            yAngle = yAngleTemp + (secondPoint.y - firstPoint.y) * 90 / Screen.height;

            xAngle %= 360;
        }

        float angleLimit = (fieldOfViewMax - Camera.main.fieldOfView) * 0.5f;
        yAngle = Mathf.Clamp(yAngle, -angleLimit, angleLimit);
        angle = new Vector3(yAngle, -xAngle, 0f);

        angleTemp = angle;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(angle), angleRotateSpeed);

        // 더블 탭 줌
        if (Input.touchCount == 1 && touchZero.phase == TouchPhase.Began)
        {
            howManyTouch++;
            StartCoroutine(IntervalBetweenTouch());
        }
        if (howManyTouch >= 2)
        {
            StartCoroutine(DoubleTabZoom());
        }
    }

    private void RaycastTouch()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchCheck = true;
                rotateCheck = false;
                touchPoint0 = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                touchPoint1 = touch.position;
                if (Vector2.Distance(touchPoint0, touchPoint1) > Screen.height * 0.03f)
                {
                    rotateCheck = true;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                if (!rotateCheck && touchCheck)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.transform.CompareTag("Door"))
                        {
                            howManyTouch = 0;
                            player.Touched(hit.transform.parent.gameObject.transform.parent.GetComponent<Door>());
                        }
                        else if (hit.transform.CompareTag("Item"))
                        {
                            howManyTouch = 0;
                            hit.transform.GetComponent<Button>().Touched();
                        }
                    }
                    touchCheck = false;
                }
            }
        }
    }

    private void OnEnable()
    {
        firstPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        secondPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        Vector3 angleTemp = this.transform.rotation.eulerAngles;

        xAngle = -angleTemp.y;
        yAngle = angleTemp.x;

        if (Input.touchCount > 0)
        {
            firstPoint = Input.GetTouch(0).position;
            secondPoint = Input.GetTouch(0).position;
            xAngleTemp = xAngle;
            yAngleTemp = yAngle;
        }
    }

    private IEnumerator IntervalBetweenTouch()
    {
        yield return new WaitForSeconds(0.3f);

        howManyTouch = 0;
    }

    private IEnumerator DoubleTabZoom()
    {
        player.enabled = false;
        howManyTouch = 0;

        float startTime = 0f;
        float curfieldOfView = Camera.main.fieldOfView;

        while (startTime < 1f)
        //while (fieldOfViewMin <= Camera.main.fieldOfView && Camera.main.fieldOfView <= fieldOfViewMax)
        {
            startTime += Time.deltaTime * 3.5f;
            if ((fieldOfViewMin + fieldOfViewMax) / 2 < curfieldOfView)
                Camera.main.fieldOfView = Mathf.Lerp(curfieldOfView, fieldOfViewMin, startTime);
            else
                Camera.main.fieldOfView = Mathf.Lerp(curfieldOfView, fieldOfViewMax, startTime);

            float angleLimit = (fieldOfViewMax - Camera.main.fieldOfView) * 0.5f;
            yAngle = Mathf.Clamp(yAngle, -angleLimit, angleLimit);
            angle = new Vector3(yAngle, angleTemp.y, 0f);

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(angle), angleRotateSpeed);

            yield return new WaitForFixedUpdate();
        }

        if (Camera.main.fieldOfView < fieldOfViewMin)
            Camera.main.fieldOfView = fieldOfViewMin;
        else if (Camera.main.fieldOfView > fieldOfViewMax)
            Camera.main.fieldOfView = fieldOfViewMax;

        player.enabled = true;
    }
}
