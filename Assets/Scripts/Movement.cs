using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            Debug.Log("�����̽� �� ����");
        }   
    }

    private void ProcessRotaion()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("���� ����");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("������ ����");
        }
    }
}
