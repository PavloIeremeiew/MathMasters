using MathMasters;
using MathMasters.Services;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

public class UIDocumentInstaller : MonoInstaller
{
    [SerializeField] private GameObject _prefab;
    public override void InstallBindings()
    {
        UIDocument uIDocument = Instantiate(_prefab).GetComponent<UIDocument>();
        Container.Bind<UIDocument>().FromInstance(uIDocument).AsSingle();
        Container.Bind<ProgressBarUI>().FromInstance(new ProgressBarUI(uIDocument)).AsTransient();
    }
}