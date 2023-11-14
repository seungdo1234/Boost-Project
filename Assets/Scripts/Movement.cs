using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField]
    private float thrustPower;
    [SerializeField]
    private float rotionPower;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotaion();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrust = thrustPower * Time.deltaTime;
            // AddRelativeForce : 오브젝트의 로컬 좌표를 기준으로 힘을 부여 (회전이 된 상태일때 오브젝트의 머리부분으로 힘이 들어감)
            rigid.AddRelativeForce(Vector3.up * thrust);
        }   
    }

    private void ProcessRotaion()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            RotationThrust(rotionPower);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotationThrust(-rotionPower);
        }
    }

    public void RotationThrust(float rotationDir)
    {
        rigid.freezeRotation = true; // 수동 제어를 할 수 있도록 회전을 고정
        transform.Rotate(Vector3.forward  * Time.deltaTime * rotationDir);
        rigid.freezeRotation = false; // 물리 시스템이 적용 되도록 회전 고정을 해제
    }
}
