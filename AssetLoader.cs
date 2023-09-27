using System.Collections;
using System.Collections.Generic;
using System;

public class AssetLoader
{
    public Dictionary<string, AsyncOperationHandle<GameObject>> operationDictionary;
    public List<string> keys = new List<string> {"UnitAttributeTemplate"} ;
    public Action Ready;

    public AssetLoader() {}

    public IEnumerator LoadAndAssociateResultWithKey(IList<string> keys)
    {
        if (operationDictionary == null)
            operationDictionary = new Dictionary<string, AsyncOperationHandle<GameObject>>();

        AsyncOperationHandle<IList<IResourceLocation>> locations
            = Addressables.LoadResourceLocationsAsync(keys,
                Addressables.MergeMode.Union, typeof(GameObject));

        yield return locations;

        var loadOps = new List<AsyncOperationHandle>(locations.Result.Count);

        foreach (IResourceLocation location in locations.Result)
        {
            AsyncOperationHandle<GameObject> handle =
                Addressables.LoadAssetAsync<GameObject>(location);
            handle.Completed += obj => operationDictionary.Add(location.PrimaryKey, obj);
            loadOps.Add(handle);
        }

        yield return Addressables.ResourceManager.CreateGenericGroupOperation(loadOps, true);

        Ready.Invoke();
    }

    private void OnAssetsReady()
    {
        foreach (var item in operationDictionary)
        {
            Debug.Log($"{item.Key} = {item.Value.Result.name}");
        }
    }

    private void OnDestroy()
    {
        foreach (var item in operationDictionary)
        {
            Addressables.Release(item.Value);
        }
    }
}
