using UnityEngine;
using UnityEngine.SceneManagement;

public class LifecycleManager : MonoBehaviour
{
    private static LifecycleManager lifecycle = null;

    public static LifecycleManager Lifecycle
    {
        get
        {
            if (lifecycle == null)
            {
                GameObject gObj = new GameObject();
                gObj.name = "Lifecycle";
                lifecycle = gObj.AddComponent<LifecycleManager>();
                DontDestroyOnLoad(gObj);
            }
            return lifecycle;
        }
    }

    private void Awake()
    {
        if (lifecycle == null)
        {
            lifecycle = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadFirstScene();
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }

    /* TODOD: Um.... Application lifecycle managament (event bus etc) goes here! */
}