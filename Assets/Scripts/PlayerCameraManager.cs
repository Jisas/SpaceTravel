using Cinemachine;
using UnityEngine;

public class PlayerCameraManager : MonoBehaviour
{
    public float minMaxDutch = 20.0f;
    private CinemachineVirtualCamera _camera;

    private GameManager m_GameManager;
    private ShipController m_ShipController;
    private Transform _target;

    void Start()
    {
        m_GameManager = GameManager.Instance;
        _camera = GetComponent<CinemachineVirtualCamera>();
        _target = _camera.m_LookAt;
        m_ShipController = FindAnyObjectByType<ShipController>();
        //_camera.m_Lens.FieldOfView = m_GameManager.GetSettingsData().cameraFOV;
    }

    void Update()
    {
        if (m_ShipController.CurrentShip.type == ShipType.Exploration && m_ShipController.SkillIsInUse)
            return;

        UpdateDutch();
    }

    private void UpdateDutch()
    {
        // Obtenemos la rotación actual del objeto a seguir
        float targetRotation = _target.rotation.eulerAngles.z;

        // Ajustamos la rotación para que esté entre 0 y 360
        if (targetRotation > 180)
            targetRotation -= 360;

        // Interpolamos linealmente entre el valor actual de Dutch y la rotación actual
        float dutch = Mathf.Lerp(_camera.m_Lens.Dutch, targetRotation, Time.deltaTime);

        // Limitamos el valor de Dutch entre minDutch y maxDutch
        dutch = Mathf.Clamp(dutch, -minMaxDutch, minMaxDutch);

        // Asignamos el valor calculado a la propiedad Dutch de la cámara
        _camera.m_Lens.Dutch = dutch;
    }
}