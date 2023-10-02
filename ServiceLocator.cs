using System;
using System.Collections.Generic;


namespace QuadriJong
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> servicesList = new Dictionary<Type, object>();

        public static void RegisterService<T>(T service)
        {
            servicesList[typeof(T)] = service;
        }

        public static T GetService<T>()
        {
            return (T)servicesList[typeof(T)];
        }
    }
}
