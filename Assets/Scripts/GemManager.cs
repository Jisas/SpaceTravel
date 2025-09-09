using UnityEngine;

public class GemManager : MonoBehaviour
{
    [Header("Gem")]
    public GameObject gemEffect;
    public MeshRenderer gemMesh;
    public Collider gemCol;

    private TimerAndCoin timerAndCoin;

    private void Start()
    {
        timerAndCoin= FindAnyObjectByType<TimerAndCoin>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gemMesh.enabled = false;
            gemCol.enabled = false;
            gemEffect.SetActive(true);
            timerAndCoin.CurrentGems += 1;
        }
    }
}
