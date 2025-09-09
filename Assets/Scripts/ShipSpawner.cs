using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private float immunityTimeOnRevive;
    private ShipsDatabase shipDatabase;

    public bool WasRevive { get; set; } = false;

    void Start()
    {
        shipDatabase = Resources.Load<ShipsDatabase>("Databases/ShipsData");
        InstanceShip();
    }

    public void InstanceShip()
    {  
        GameObject ship = Instantiate(shipDatabase.FindShipInUse().shipPrefab);
        ship.transform.parent = transform;
        ship.transform.position = transform.position;

        if (WasRevive)
        {
            float timeElapsed = 0;
            var collider = ship.GetComponent<Collider>();
            collider.enabled = false;

            while (timeElapsed < immunityTimeOnRevive)
            {
                timeElapsed += Time.deltaTime * 1;
            }

            collider.enabled = true;
            WasRevive = false;
        }
    }
}
