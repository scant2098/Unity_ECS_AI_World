using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class InputHelper
    {
        private static Array keyCodes;
        public static List<KeyCode> keyPresseds=new List<KeyCode>(); // 存储按下的键
        public static void CheckInput()
        {
            Profiler.BeginSample("CheckInput");
            keyPresseds.RemoveAll(key => !Input.GetKey(key));
            if (Input.anyKey)
            {
                if (keyCodes == null)
                { 
                    keyCodes = Enum.GetValues(typeof(KeyCode));
                }
                for (int i = 0; i < keyCodes.Length; i++)
                {
                    KeyCode keyCode = (KeyCode)keyCodes.GetValue(i);
                    if (Input.GetKey(keyCode))
                    {
                        if (!keyPresseds.Contains(keyCode))
                        {
                            keyPresseds.Add(keyCode);
                        }
                    }
                }
            }
            Profiler.EndSample();
        }
    }
}