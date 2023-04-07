using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundObject : MonoBehaviour
{

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _distanceFromTarget = 30;

    public float rotationSpeed = 0.1f; 
    private float currentRotation = 0.0f; 

    void Update()
    {  
<<<<<<< HEAD
        if (Input.touchCount > 0 && Time.timeScale != 0) // 터치 입력이 있을 경우
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치 입력
=======
        if (Input.touchCount > 0) // 터치 입력이 있을 경우
        {
            Touch touch = Input.GetTouch(0); // 첫 번째 터치 입력
            if (touch.position.y <= 950) {
            //Debug.Log("Touch Position: " + touch.position);
>>>>>>> aad1621 (최종)
            if (touch.phase == TouchPhase.Moved) // 터치가 움직였을 때
            {
                float deltaRotation = touch.deltaPosition.x * rotationSpeed; // 회전할 각도 계산
                currentRotation += deltaRotation; // 현재 회전 각도 업데이트
                transform.rotation = Quaternion.Euler(55.0f , currentRotation, 0.0f); // y축 회전
            }
<<<<<<< HEAD
=======
            }
>>>>>>> aad1621 (최종)
        }   
        Vector3 position = _target.position - transform.forward * _distanceFromTarget;//타겟 거리
        position.y = 30f; //카메라 높이
        transform.position = position;
    }

}



// public class MoveAroundObject : MonoBehaviour
// {
//     [SerializeField]
//     private float _mouseSensitivity = 3.0f;
//     private float _rotationY;
//     private float _rotationX;

//     [SerializeField]
//     private Transform _target;

//     [SerializeField]
//     private float _distanceFromTarget = 30.0f;

//     private Vector3 _currentRotation;
//     private Vector3 _smoothVelocity = Vector3.zero;

//     [SerializeField]
//     private float _smoothTime = 0.2f;

//     [SerializeField]
//     private Vector2 _rotationXMinMax = new Vector2(-40, 40);

//     private Vector3 startPos, endPos, distance;
//     private float touchStart, touchFinish;
//     Vector3 throwForce;
//     [SerializeField, Range(0f, 1f)]
//     private float directionThreshold = .9f;

//     void Update()
//     {
//         //if (Input.mousePosition.y < 300.00) {
//         //if (Input.touchPosition.y < 300.00) {
//             // if (Input.GetMouseButtonDown(0) || Input.touchCount>0) {
//             if (Input.touchCount>0) {
//                 Touch touch = Input.GetTouch(0);
//                 if (touch.phase == TouchPhase.Began) {
//                     //Debug.Log("Touch: " + Input.GetTouch(0).phase);
//                     Debug.Log("Touch: " + Input.touchCount);
//                     touchStart = Time.time;
//                     //startPos = Input.mousePosition;
//                     startPos = Input.GetTouch(0).position;
//                 }  
//             }
//             //Debug.Log("Less than"+Input.mousePosition.y);
//             //if (Input.GetMouseButtonUp(0) || Input.touchCount>0 && Input.GetTouch(0).phase==TouchPhase.Ended) {
//             if (Input.touchCount>0) {
//                 Touch touch = Input.GetTouch(0);
//                 if (Input.GetTouch(0).phase==TouchPhase.Ended) {
//                     touchFinish = Time.time;
//                     //endPos = Input.mousePosition;
//                     endPos = Input.GetTouch(0).position;

//                     float totalTime = touchFinish - touchStart;
//                     distance = endPos - startPos;
//                     distance.z = distance.magnitude;
//                     throwForce = distance / (totalTime*50000);
//                     //transform.position = throwForce;
//                 }
//             }
//             float mouseX = throwForce.x * _mouseSensitivity;
//             float mouseY = throwForce.y * _mouseSensitivity;
//             // float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
//             // float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
//             //

//             _rotationY += mouseX;
//             _rotationX += mouseY;

//             // Apply clamping for x rotation 
//             _rotationX = Mathf.Clamp(_rotationX, _rotationXMinMax.x, _rotationXMinMax.y);

//             Vector3 nextRotation = new Vector3(55, _rotationY);

//             // Apply damping between rotation changes
//             _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, _smoothTime);
//             transform.localEulerAngles = _currentRotation;

//             // Substract forward vector of the GameObject to point its forward vector to the target
//             transform.position = _target.position - transform.forward * _distanceFromTarget;
//         //}
//     }

// }