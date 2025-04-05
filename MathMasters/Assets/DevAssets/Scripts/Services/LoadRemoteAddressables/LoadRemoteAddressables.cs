using MathMasters.Entities;using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MathMasters
{
    public class LoadRemoteAddressables: ILoadRemoteAddressables
    {
        public void LoadAsset<T>(string address, Action<T> onSuccess, Action onFailure = null) where T : UnityEngine.Object
        {
            onFailure+= () => Debug.LogError($"Failed to load addressable: {address}");
            Addressables.LoadAssetAsync<T>(address).Completed += (handle) => OnLoaded(handle, onSuccess, onFailure);
        }

        private void OnLoaded<T>(AsyncOperationHandle<T> handle, Action<T> onSuccess, Action onFailure) where T: UnityEngine.Object
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onSuccess?.Invoke(handle.Result);
            }
            else
            {
                onFailure?.Invoke();
            }
        }
    }
}
