using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rigid;
    private CollisionHandler collision;

       
    [SerializeField]
    private float thrustPower;
    [SerializeField]
    private float rotionPower;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collision = GetComponent<CollisionHandler>();
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
            if (!collision.particles[(int)ParticleName.MainThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.MainThrust].Play();
                collision.particles[(int)ParticleName.MainThrust - 1].Play();
            }

            float thrust = thrustPower * Time.deltaTime;
            // AddRelativeForce : 오브젝트의 로컬 좌표를 기준으로 힘을 부여 (회전이 된 상태일때 오브젝트의 머리부분으로 힘이 들어감)
            rigid.AddRelativeForce(Vector3.up * thrust);
            AudioManager.instance.PlayThrustSfx();
        }
        else
        {
            if (collision.particles[(int)ParticleName.MainThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.MainThrust].Stop();
                collision.particles[(int)ParticleName.MainThrust - 1].Stop();
            }
            if (AudioManager.instance.sfxPlayers[(int)Sfx.Thrust].isPlaying)
            {
                AudioManager.instance.sfxPlayers[(int)Sfx.Thrust].Stop();
            }
        }
    }

    private void ProcessRotaion()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!collision.particles[(int)ParticleName.LeftThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.LeftThrust].Play();
            }

            if (collision.particles[(int)ParticleName.RightThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.RightThrust].Stop();
            }
            RotationThrust(rotionPower);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!collision.particles[(int)ParticleName.RightThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.RightThrust].Play();
            }
            if (collision.particles[(int)ParticleName.LeftThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.LeftThrust].Stop();
            }

            RotationThrust(-rotionPower);
        }
        else
        {
            if (collision.particles[(int)ParticleName.RightThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.RightThrust].Stop();
            }
            else if (collision.particles[(int)ParticleName.LeftThrust].isPlaying)
            {
                collision.particles[(int)ParticleName.LeftThrust].Stop();
            }
        }
        
    }

    public void RotationThrust(float rotationDir)
    {
        rigid.freezeRotation = true; // 수동 제어를 할 수 있도록 회전을 고정
        transform.Rotate(Vector3.forward  * Time.deltaTime * rotationDir);
        rigid.freezeRotation = false; // 물리 시스템이 적용 되도록 회전 고정을 해제
    }
}
