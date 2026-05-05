using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private static string saveFolder => Path.Combine(Application.persistentDataPath, "saves");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);
    }

    private List<ISaveInterface> GetAllSaveableObjects()
    {
        List<ISaveInterface> list = new List<ISaveInterface>();
        MonoBehaviour[] allMono = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

        foreach (var mono in allMono)
        {
            if (mono.gameObject.scene == SceneManager.GetActiveScene()
                && !mono.gameObject.hideFlags.HasFlag(HideFlags.DontSave))
            {
                if (mono is ISaveInterface saveObj)
                {
                    list.Add(saveObj);
                }
            }
        }
        return list;
    }

    public void Save(int slot)
    {
        SaveData data = new SaveData(
            PlayerMovement.instance.transform.position,
            0,
            NarrativeManager.instance.narrativeChoice
        );

        List<ISaveInterface> saveables = GetAllSaveableObjects();
        data.allObjStates.Clear();

        foreach (var s in saveables)
        {
            ObjectState state = s.SaveState();
            state.id = s.GetUniqueId();
            data.allObjStates.Add(state);
        }

        data.inventoryItems.Clear();
        foreach (var item in BackageManager.instance.items)
        {
            if (item != null)
            {
                data.inventoryItems.Add(new Item
                {
                    id = item.id,
                    name = item.name,
                    count = item.count,
                    isStackable = item.isStackable
                });
            }
            else
            {
                data.inventoryItems.Add(null);
            }
        }

        string json = JsonUtility.ToJson(data, prettyPrint: true);
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        File.WriteAllText(path, json);

        Debug.Log($"存档成功：槽位 {slot} 物体数量：{data.allObjStates.Count}");
    }

    public void Load(int slot)
    {
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        if (!File.Exists(path))
        {
            Debug.LogWarning($"槽位 {slot} 无存档");
            return;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        // 恢复玩家
        PlayerMovement.instance.transform.position = data.playerPos;

        // 恢复剧情
        NarrativeManager.instance.narrativeChoice.Clear();
        NarrativeManager.instance.narrativeChoice.AddRange(data.playerChoices);

        // 恢复物体
        List<ISaveInterface> saveables = GetAllSaveableObjects();
        foreach (var s in saveables)
        {
            string uid = s.GetUniqueId();
            foreach (var state in data.allObjStates)
            {
                if (state.id == uid)
                {
                    s.LoadState(state);
                    break;
                }
            }
        }

        BackageManager.instance.LoadInventoryFromSave(data.inventoryItems);

        Debug.Log("读档完成，物体状态已恢复");
    }

    public void DeleteSave(int slot)
    {
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        if (File.Exists(path)) File.Delete(path);
    }

    public bool HasSave(int slot)
    {
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        return File.Exists(path);
    }
}