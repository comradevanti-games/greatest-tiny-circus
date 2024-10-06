#nullable enable

using System;
using UnityEngine;

namespace GTC
{
    public static class Singletons
    {
        private static GameObject? host;

        private static GameObject? Host
        {
            get
            {
                if (host == null || !host)
                    host = GameObject.FindWithTag("Singletons");
                return host;
            }
        }

        public static T? TryGet<T>() where T : class => Host?.GetComponent<T>();

        public static T Get<T>() where T : class =>
            TryGet<T>() ??
            throw new NullReferenceException(
                $"Singleton of type {typeof(T).Name} could not be found");
    }
}