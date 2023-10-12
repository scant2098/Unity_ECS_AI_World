using System.Collections.Generic;

namespace JH_ECS
{
    public class EcsSetting
    {
        private static List<string> systemorder = new List<string>();
        public static List<string> SystemOrder
        {
            get
            {
                if (systemorder.Count <= 0)
                    return SystemsController.GetAllSystemName();
                else
                    return systemorder;
            }
            set
            {
                systemorder = value;
            }
        }

    }
}