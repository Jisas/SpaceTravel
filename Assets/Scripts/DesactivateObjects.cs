using UnityEngine;

public class DesactivateObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.gameObject.SetActive(false);
    }
}
