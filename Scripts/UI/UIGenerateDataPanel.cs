using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UniRx;

public class UIGenerateDataPanel : MonoBehaviour
{
    public TextMeshProUGUI TMP_RoleGenerateData;
    void Start()
    {
        WorldGenerator.Instance._generateCount.Subscribe(_ =>
        {
           RefreshUI();
        }).AddTo(this);
    }
    private void RefreshUI()
    {
        TMP_RoleGenerateData.text = "正在生成角色:" + WorldGenerator.Instance._generateCount.Value + "/" + 100000;
    }

}
