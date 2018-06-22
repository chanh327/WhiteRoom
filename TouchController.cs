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
    float fieldOfViewMin;
    float fieldOfViewMax;

    bool touchCheck;
    bool rotateCheck;
    Vector3 touchPoint0;
    Vector3 touchPoint1;

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

        angleRotateSpeed = 0.4f;
        perspectiveZoomSpeed = 0.04f;
        fieldOfViewMin = 25f;
        fieldOfViewMax = 50f;

        touchCheck = false;
        rotateCheck = false;
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
                    player.MoveToDoor(hit.transform.parent.gameObject.transform.parent.GetComponent<Door>());

                    hit.transform.parent.gameObject.transform.parent.GetComponent<SecretController>().OpenSecret();
                }
                else if(hit.transform.CompareTag("Item"))
                {
                    Debug.Log("Item Touched");
                    hit.transform.GetComponent<Button>().Touched();
                }
                // 버튼 태그 추가
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

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(angle), angleRotateSpeed);
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
                            player.MoveToDoor(hit.transform.parent.gameObject.transform.parent.GetComponent<Door>());
                            hit.transform.parent.gameObject.transform.parent.GetComponent<SecretController>().OpenSecret();
                        }

                        // 버튼 태그 추가
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
    }
}
