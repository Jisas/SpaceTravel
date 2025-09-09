using UnityEngine.UI;
using UnityEngine;

public class ShipCollisionsManager : MonoBehaviour
{
    [Header("Collisions")]
    public LayerMask modifiersLayer;

    [Header("UI")]
    public Image defenceIndicator;

    [Header("VFX")]
    public ParticleSystem stopOrb;
    public ParticleSystem minOrb;
    public ParticleSystem x2Orb;
    public ParticleSystem ringBluEffect;
    public ParticleSystem ringYellowEffect;

    // References
    private ShipController m_ShipController;
    private BonusCoinManager bonusCoinManager;
    private ScrapManager scrapManager;
    private Ship m_Ship;

    // Private values
    private float shipStrength;

    private void Start()
    {
        m_ShipController = GetComponentInParent<ShipController>();
        bonusCoinManager = m_ShipController.BonusText;
        m_Ship = m_ShipController.CurrentShip;
        shipStrength = m_Ship.stats.strength;
        SetUpDefenceUI();
    }
    public void OnTriggerEnter(Collider other)
    {
        #region Orbs
        if (other.gameObject.layer == 8)
        {

            ModifierData data = other.GetComponent<ModifierData>();
            float duration = data.Duration;
            Color color = data.ModifierColor;
            PlayerDecorator newModifier = null;
            AudioSource BubbleAudio = null;

            if (other.CompareTag("StopScaleOrb"))
            {
                stopOrb.Play();
                minOrb.Stop();
                x2Orb.Stop();
                BubbleAudio = stopOrb.GetComponent<AudioSource>();
                newModifier = new Standby(m_ShipController.CurrentPlayer, duration, color);
            }
            else if (other.CompareTag("ScaleX2Orb"))
            {
                stopOrb.Stop();
                minOrb.Stop();
                x2Orb.Play();
                BubbleAudio = minOrb.GetComponent<AudioSource>();
                newModifier = new MaximizationX2(m_ShipController.CurrentPlayer, duration, color);
            }
            else if (other.CompareTag("MiniaturizeOrb"))
            {
                stopOrb.Stop();
                minOrb.Play();
                x2Orb.Stop();
                BubbleAudio = x2Orb.GetComponent<AudioSource>();
                newModifier = new Miniaturization(m_ShipController.CurrentPlayer, duration, color);
            }

            BubbleAudio.Play();
            other.gameObject.SetActive(false);
            m_ShipController.ApplyModifier(newModifier);
        }
        #endregion

        #region Rings
        if (other.CompareTag("BonusI"))
        {
            ringBluEffect.Play();
            AudioSource ringAudio = ringBluEffect.GetComponent<AudioSource>();
            ringAudio.Play();

            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("BonusC"))
        {
            ringYellowEffect.Play();
            AudioSource ringAudio = ringBluEffect.GetComponent<AudioSource>();
            ringAudio.Play();

            other.gameObject.SetActive(false);
            StartCoroutine(bonusCoinManager.SetBonusText());
        }
        #endregion

        #region Others
        // Asteroids
        if (other.CompareTag("Enemy"))
        {
            var crashManager = other.GetComponent<CrashManager>();
            shipStrength = shipStrength <= 0 ? 0 : shipStrength - 1;
            if (shipStrength == 0) crashManager.Dead();
            UpdateDefenceUI();
        }

        // Scrap
        if (other.CompareTag("Scrap"))
        {
            scrapManager = other.GetComponent<ScrapManager>();
            shipStrength += 1;

            if (shipStrength >= m_Ship.stats.strength)
                shipStrength = m_Ship.stats.strength;

            scrapManager.SockScrap();
            UpdateDefenceUI(false);
        }
        #endregion
    }

    private void SetUpDefenceUI()
    {
        float removeSegments;
        Material material = GetImageMaterial(defenceIndicator);

        if (material != null)
        {
            switch (shipStrength)
            {
                case 2:
                    removeSegments = 7;
                    material.SetFloat("_RemoveSegments", removeSegments);
                    break;

                case 3:
                    removeSegments = 6;
                    material.SetFloat("_RemoveSegments", removeSegments);
                    break;

                case 4:
                    removeSegments = 5;
                    material.SetFloat("_RemoveSegments", removeSegments);
                    break;
            }
        }
    }
    private void UpdateDefenceUI(bool isAsteroid = true)
    {
        const int maxSegments = 9;
        Material material = GetImageMaterial(defenceIndicator);

        if (material != null)
        {
            float removeSegments = material.GetFloat("_RemoveSegments");

            if (isAsteroid)
            {
                if (removeSegments < maxSegments)
                {
                    removeSegments++;
                    material.SetFloat("_RemoveSegments", removeSegments);
                }
            }
            else
            {
                int minSegments = 0;
                switch (shipStrength)
                {
                    case 2:
                        minSegments = 7;
                        break;

                    case 3:
                        minSegments = 6;
                        break;

                    case 4:
                        minSegments = 5;
                        break;
                }

                if (removeSegments > minSegments && removeSegments < maxSegments)
                {
                    removeSegments--;
                    material.SetFloat("_RemoveSegments", removeSegments);
                }
            }
        }
    }
    private Material GetImageMaterial(Image image) => image.material;
}