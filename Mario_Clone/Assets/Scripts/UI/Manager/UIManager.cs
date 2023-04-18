using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonMonoBehaviour<UIManager>
{
    StringBuilder sb = new StringBuilder();
    [SerializeField]
    TMP_Text coinText;
    [SerializeField]
    TMP_Text timerText;

    public void UpdateCoinText(uint coin)
    {
        sb.Clear();
        sb.AppendFormat("{0:00}", coin);
        coinText.text = sb.ToString();
    }

    public void UpdateTimerText(short timer)
    {
        sb.Clear();
        sb.Append(timer);
        timerText.text = sb.ToString();
    }
}
