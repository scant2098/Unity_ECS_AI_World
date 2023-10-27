using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JH_ECS
{
    public class ComponentController
    {

        public static List<IComponent> GetAllComponents()
        {
            var allComponents = new List<IComponent>();
            var typesInJHECS = Assembly.GetAssembly(typeof(IComponent)).GetTypes()
                .Where(t => t.Namespace == "JH_ECS");
            var componentTypes = typesInJHECS
                .Where(t => typeof(IComponent).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var componentType in componentTypes)
            {
                var componentInstance = Activator.CreateInstance(componentType) as IComponent;
                if (componentInstance != null)
                {
                    allComponents.Add(componentInstance);
                }
            }
            return allComponents;
        }
    }
}