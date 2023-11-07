using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TMP_Counter : MonoBehaviour
{
    private TMP_Text _tT_ChangeText;

    [SerializeField] private Ease _animType = Ease.OutQuad;

    void Awake()
    {
        _tT_ChangeText = GetComponent<TMP_Text>();
    }

    public void ChangeTextAnim(int countNum) {
        int startNumber = 0;
        int.TryParse(_tT_ChangeText?.text ?? "", out startNumber);
        DOTween.To(() => startNumber, x => startNumber = x, countNum, 0.3f)
            .SetEase(_animType)
            .OnUpdate(() => {
                _tT_ChangeText.text = startNumber.ToString(); 
        });
    }

    public void ChangeImmediateText(int seletNum) {
        if(_tT_ChangeText) {
            _tT_ChangeText.text = seletNum.ToString();
        }
    }
}
