using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobSystemExample : MonoBehaviour
{
    private NativeArray<float> inputData;
    private NativeArray<float> outputData;

    private void Start()
    {
        int dataSize = 1000000;
        inputData = new NativeArray<float>(dataSize, Allocator.Persistent);
        outputData = new NativeArray<float>(dataSize, Allocator.Persistent);

        // 填充输入数据
        for (int i = 0; i < dataSize; i++)
        {
            inputData[i] = i * i;
        }

        // 创建并调度Job
        var job = new SquareRootJob
        {
            inputData = inputData,
            outputData = outputData
        };
        JobHandle jobHandle = job.Schedule(dataSize, 64); // 并行度为64

        // 等待Job完成
        jobHandle.Complete();

        // 清理NativeArray
        inputData.Dispose();
        outputData.Dispose();
    }

    [BurstCompile]
    private struct SquareRootJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float> inputData;
        [WriteOnly] public NativeArray<float> outputData;

        public void Execute(int index)
        {
            outputData[index] = Mathf.Sqrt(inputData[index]);
        }
    }
}