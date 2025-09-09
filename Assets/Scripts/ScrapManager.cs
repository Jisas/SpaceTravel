using UnityEngine;

public class ScrapManager : MonoBehaviour
{
    public MeshRenderer mesh;
    private Collider col;

    void Start()
    {
        col = this.GetComponent<Collider>();
    }

    public void SockScrap()
    {
        col.enabled = false;
        mesh.enabled = false;
    }
}
