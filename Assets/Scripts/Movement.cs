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
            Debug.Log("스페이스 바 누름");
        }   
    }

    private void ProcessRotaion()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("왼쪽 누름");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("오른쪽 누름");
        }
    }
}
