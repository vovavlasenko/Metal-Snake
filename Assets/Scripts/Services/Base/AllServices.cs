using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class AllServices
    {
        private readonly List<IService> services = new List<IService>();
        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
      services.Add(implementation);

        public TService Single<TService>() where TService : IService
        {
            Debug.Log("Reg single ");
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i] is TService)
                    return (TService)services[i];
            }

            return default;
        }

        public void Cleanup()
        {
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i] is ICleanupService)
                    ((ICleanupService)services[i]).Cleanup();
            }
        }
    }
}

