using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/Game Settings Installer")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private LevelDatabase _levelDatabase;
    public LevelDatabase LevelDatabase { get { return _levelDatabase; } }

    public override void InstallBindings()
    {
        Container.BindInstance(LevelDatabase).AsSingle();
    }
}