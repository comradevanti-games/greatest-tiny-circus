#nullable enable

using System;
using UnityEngine;

namespace GTC
{
    public static class Singletons
    {
        private static GameObject? host;

        public static T Get<T>() where T : class
        {
            if (host == null || !host)
                host = GameObject.FindWithTag("Singletons");
            if (host == null)
                throw new NullReferenceException(
                    "Singletons could not be found");

            return host.GetComponent<T>() ??
                   throw new NullReferenceException(
                       $"Singleton of type {typeof(T).Name} could not be found");
        }
    }
}