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

    public void UpdateCoinText(uint coin)
    {
        sb.Clear();
        sb.AppendFormat("<size=25><font=brother-ep-20-22-electronic-typewriter-square>x</font></size> {0:00}", coin);
        coinText.text = sb.ToString();
    }
}
