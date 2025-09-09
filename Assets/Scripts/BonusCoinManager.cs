using System.Collections;
using UnityEngine;
using TMPro;

public class BonusCoinManager : MonoBehaviour
{
    public TextMeshProUGUI text;

    private TimerAndCoin timerAndCoin;
    private int bonusC = 0;

    public int BonusCoin { get => bonusC; }

    private void Start()
    {
        timerAndCoin = FindAnyObjectByType<TimerAndCoin>();
    }

    public IEnumerator SetBonusText()
    {
        bonusC = 5 + (timerAndCoin.CurrentTime / 120);
        text.text = $"+{bonusC}";

        yield return new WaitForSeconds(2.0f);
        text.text = "";
    }
}
