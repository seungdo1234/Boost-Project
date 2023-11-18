using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;


public enum ParticleName { ShutDown, Goal, MainThrust = 3 , RightThrust , LeftThrust }
public enum InteractionName { ShutDown, Goal }
public class CollisionHandler : MonoBehaviour
{
    public ParticleSystem[] particles;
    private Movement move;
    private bool isInteracion;
    [SerializeField]
    private float levelLoadDelay;

    private void Awake()
    {
        move = GetComponent<Movement>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isInteracion){ return; }


        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartCoroutine(InteractiveRocket(InteractionName.Goal));
                break;
            case "Fuel":
                Debug.Log("Fuel");
                break;
            default:
                StartCoroutine(InteractiveRocket(InteractionName.ShutDown));
                break;

        }
    }

    private IEnumerator InteractiveRocket(InteractionName interaction) // ������ ��ȣ�ۿ� 1.�ı�, 2.���� ...
    {
        particles[(int)ParticleName.MainThrust].Stop();
        particles[(int)ParticleName.MainThrust - 1].Stop();
        particles[(int)ParticleName.LeftThrust].Stop();
        particles[(int)ParticleName.RightThrust].Stop();

        isInteracion = true;
        move.enabled = false;
        AudioManager.instance.sfxPlayers[(int)Sfx.Thrust].Stop();

        InteractionEffect(interaction);

        yield return new WaitForSeconds(levelLoadDelay);

        particles[(int)interaction].Stop();
        isInteracion = false;
        move.enabled = true;
        
        LoadLevel(interaction);

    }
    private void InteractionEffect(InteractionName interaction)
    {
        switch (interaction)
        {
            case InteractionName.ShutDown:
                particles[(int)ParticleName.ShutDown].Play();
                AudioManager.instance.PlayerSfx(Sfx.ShutDown);
                break;
            case InteractionName.Goal:
                particles[(int)ParticleName.Goal].Play();
                AudioManager.instance.PlayerSfx(Sfx.Goal);
                break;
        }
    }
    private void LoadLevel(InteractionName interaction) // ���� �ε� ���� �Լ�
    {

        switch (interaction)
        {
            case InteractionName.ShutDown:
                ReloadLevel();
                break;
            case InteractionName.Goal:
                LoadNextLevel();
                break;
        }
    }
    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // ���� �� ��ȣ�� �츮�� �����ϴ� ���� ������ ���̶��
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    private void ReloadLevel()
    {
        //  SceneManager.GetActiveScene().buildIndex : ���� ����ǰ��ִ� �� ��ȣ ��ȯ 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
