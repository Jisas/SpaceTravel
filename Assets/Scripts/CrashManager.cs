using UnityEngine;

public class CrashManager : MonoBehaviour
{
    private DeadManager deadManager;
    private GameObject explotion;
    private MeshRenderer mesh;
    private SphereCollider col;
    private int projectileCount = 0;
    private AudioSource myAudio;
    private TraumaInducer myTraumaInducer;

    void Start()
    {
        deadManager = GameObject.FindGameObjectWithTag("Respawn").GetComponent<DeadManager>();
        explotion = transform.GetChild(0).gameObject;
        mesh = gameObject.GetComponent<MeshRenderer>();
        col = gameObject.GetComponent<SphereCollider>();
        myAudio = gameObject.GetComponent<AudioSource>();
        myTraumaInducer = GetComponent<TraumaInducer>();
    }

    public void Crash()
    {
        myAudio.Play();
        Handheld.Vibrate();
        explotion.SetActive(true);
        StartCoroutine(myTraumaInducer.Start());

        mesh.enabled = false;
        col.enabled = false;
    }

    public void Dead()
    {
        myAudio.Play();
        explotion.SetActive(true);
        mesh.enabled = false;
        col.enabled = false;

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Destroy(Player);
        StartCoroutine(deadManager.Lose());
    }
    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player") || other.CompareTag("BlackHole") || other.CompareTag("Planet") || other.CompareTag("Laser"))
        {
            Crash();
        }

        if(other.CompareTag("Projectile"))
        {
            projectileCount++;
            other.transform.GetComponentInChildren<Collider>().enabled = false;
            other.gameObject.SetActive(false);
            if (projectileCount >= 3) Crash();
        }
    }
}
