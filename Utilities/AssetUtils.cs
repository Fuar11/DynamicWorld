using ComplexLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace DynamicWorld.Utilities
{
    internal static class AssetUtils
    {

        static Dictionary<string, GameObject> cachedPrefabs = new Dictionary<string, GameObject>();

        public static GameObject GetPrefab(string prefabName)
        {
            if (!cachedPrefabs.ContainsKey(prefabName))
            {
                GeneratePrefab(prefabName);
            }
            else if(cachedPrefabs.ContainsKey(prefabName) && cachedPrefabs[prefabName] == null)
            {
                cachedPrefabs.Remove(prefabName);
                GeneratePrefab(prefabName);
            }
            return GameObject.Instantiate(cachedPrefabs[prefabName]);
        }

        private static void GeneratePrefab(string prefabName)
        {
            GameObject go = new GameObject();
            go.name = prefabName;

            MeshFilter mf = go.AddComponent<MeshFilter>();
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            MeshCollider mc = go.AddComponent<MeshCollider>();

            switch (prefabName)
            {
                case "OBJ_ModMineRock24":
                    mf.sharedMesh = Addressables.LoadAssetAsync<Mesh>("Assets/ArtAssets/Env/Objects/OBJ_MineRocks/OBJ_MineRock24.fbx").WaitForCompletion();
                    mr.sharedMaterial = Addressables.LoadAssetAsync<Material>("Assets/ArtAssets/Materials/Unique/OBJ_MineRocksA_Mat.mat").WaitForCompletion();
                    mc.sharedMesh = mf.sharedMesh;
                    break;
                case "OBJ_ModMineRock23":
                    Main.Logger.Log($"Creating prefab for {prefabName}", FlaggedLoggingLevel.Debug);
                    mf.sharedMesh = Addressables.LoadAssetAsync<Mesh>("Assets/ArtAssets/Env/Objects/OBJ_MineRocks/OBJ_MineRock23.fbx").WaitForCompletion();
                    mr.sharedMaterial = Addressables.LoadAssetAsync<Material>("Assets/ArtAssets/Materials/Unique/OBJ_MineRocksA_Mat.mat").WaitForCompletion();
                    mc.sharedMesh = mf.sharedMesh;
                    break;
            }

            cachedPrefabs.Add(prefabName, go);
        }

    }
}
