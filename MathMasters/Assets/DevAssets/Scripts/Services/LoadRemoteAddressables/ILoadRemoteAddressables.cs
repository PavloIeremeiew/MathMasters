using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MathMasters
{
    public interface ILoadRemoteAddressables
    {
        public void LoadAsset<T>(string address, Action<T> onSuccess, Action onFailure = null) where T : UnityEngine.Object;
        
    }
}
