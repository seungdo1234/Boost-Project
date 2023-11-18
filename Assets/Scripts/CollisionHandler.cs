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

    private IEnumerator InteractiveRocket(InteractionName interaction) // 로켓의 상호작용 1.파괴, 2.골인 ...
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
    private void LoadLevel(InteractionName interaction) // 레벨 로드 관련 함수
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

        // 다음 씬 번호가 우리가 빌드하는 씬의 마지막 씬이라면
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    private void ReloadLevel()
    {
        //  SceneManager.GetActiveScene().buildIndex : 현재 실행되고있는 씬 번호 반환 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
