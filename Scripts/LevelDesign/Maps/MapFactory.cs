namespace LevelDesign.Maps
{
  public class MapFactory
  {
    private readonly MapProvider _mapProvider;

    public MapFactory(MapProvider mapProvider)
    {
      _mapProvider = mapProvider;
    }

    public void Init()
    {
      _mapProvider.Map.Setup();
    }

    public void Dispose()
    {
      _mapProvider.Map = null;
    }

    // private void DisablePortalsToArenas()
    // {
    //   SceneTypeId type = _projectData.GetGameLoopSceneTypeId(_sceneLoader.CurrentScene);
    //
    //   if (type != SceneTypeId.Core)
    //   {
    //     return;
    //   }
    //
    //   List<SceneId> arenas = new();
    //
    //   foreach (var scene in _sceneLoader.LoadedScenes)
    //   {
    //     if (_projectData.GetGameLoopSceneTypeId(scene) == SceneTypeId.Arena)
    //       arenas.Add(scene);
    //   }

    // if (arenas.Count == 0)
    // {
    //   return;
    // }

    //  SceneId lastLoadedArena = arenas.Last();

    //  int count = 0;

    // foreach (P32ortal portal in _mapProvider.Map.Portals)
    // {
    //   if (portal.ToScene == lastLoadedArena)
    //   {
    //     portal.Deactivate();
    //     _mapProvider.Map.PlayerSpawnMarker.transform.position = portal.transform.position;
    //     count++;
    //   }
    // }
    //}
  }
}