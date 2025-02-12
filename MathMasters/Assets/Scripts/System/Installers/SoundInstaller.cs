using MathMasters;
using UnityEngine;
using Zenject;

public class SoundInstaller : MonoInstaller
{
    [SerializeField] private GameObject _prefab;
    public override void InstallBindings()
    {
        SoundManager audioSource = Instantiate(_prefab).GetComponent<SoundManager>();
        DontDestroyOnLoad(audioSource.gameObject);
        Container.Bind<SoundManager>().FromInstance(audioSource).AsSingle();
    }
}