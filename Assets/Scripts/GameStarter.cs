using System.Collections;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject[] objectsToActive;
    private LensDistortionManager lensDistortion;

    void Start()
    {
        lensDistortion = GetComponent<LensDistortionManager>();
        StartCoroutine(OnAll());
    }

    IEnumerator OnAll()
    {
        lensDistortion.ResetEvents();
        LensDistortionManager.OnEffectFinish += () => StartCoroutine(ActiveObjects());
        yield return new WaitForEndOfFrame();
        StartCoroutine(lensDistortion.StartExitEffect());
    }

    private IEnumerator ActiveObjects()
    {
        for (int i = 0; i < objectsToActive.Length; i++)
        {
            objectsToActive[i].SetActive(true);
            yield return new WaitForEndOfFrame();
        }
    }
}
