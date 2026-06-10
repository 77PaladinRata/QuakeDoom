using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Animator fade;
    ///* sin reinico eso creo
    [SerializeField]
    private UnityEvent onSceneStart;
    private void Start()
    {
        onSceneStart?.Invoke();
    }
    ///* un poco de pausa despues de perder
    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
    public void LoadSceneWitFade(string sceneName)
    {
        StartCoroutine(LoadSceneWithFadeCoroutine(sceneName));
    }
    private IEnumerator LoadSceneWithFadeCoroutine(string sceneName)
    {
        fade.Play("FadeOut");
        yield return new WaitForSeconds(fade. GetCurrentAnimatorStateInfo(0).length) ;
        LoadScene(sceneName);
    }
}
