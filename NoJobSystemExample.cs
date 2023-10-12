using UnityEngine;

public class NoJobSystemExample : MonoBehaviour
{
    private float[] inputData;
    private float[] outputData;

    private void Start()
    {
        int dataSize = 1000000;
        inputData = new float[dataSize];
        outputData = new float[dataSize];

        // 填充输入数据
        for (int i = 0; i < dataSize; i++)
        {
            inputData[i] = i * i;
        }
        // 串行计算平方根
        for (int i = 0; i < dataSize; i++)
        {
            outputData[i] = Mathf.Sqrt(inputData[i]);
        }

        // 清理数组
        inputData = null;
        outputData = null;
    }
}