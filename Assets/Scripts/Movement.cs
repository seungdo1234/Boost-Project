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
            // AddRelativeForce : ������Ʈ�� ���� ��ǥ�� �������� ���� �ο� (ȸ���� �� �����϶� ������Ʈ�� �Ӹ��κ����� ���� ��)
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
        rigid.freezeRotation = true; // ���� ��� �� �� �ֵ��� ȸ���� ����
        transform.Rotate(Vector3.forward  * Time.deltaTime * rotationDir);
        rigid.freezeRotation = false; // ���� �ý����� ���� �ǵ��� ȸ�� ������ ����
    }
}
