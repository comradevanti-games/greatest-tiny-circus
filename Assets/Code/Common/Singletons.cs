#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace GTC
{
    public static class Singletons
    {
        private static GameObject? host;

        private static GameObject Host
        {
            get
            {
                if (host == null || !host)
                    host = GameObject.FindWithTag("Singletons");
                if (host == null)
                    throw new NullReferenceException(
                        "Singletons could not be found");
                return host;
            }
        }

        public static IEnumerable<MonoBehaviour> All() =>
            Host.GetComponents<MonoBehaviour>();

        public static T Get<T>() where T : class =>
            Host.GetComponent<T>() ??
            throw new NullReferenceException(
                $"Singleton of type {typeof(T).Name} could not be found");
    }
}