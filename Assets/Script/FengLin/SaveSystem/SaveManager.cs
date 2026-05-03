using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    // 存档路径
    private static string saveFolder => Path.Combine(Application.persistentDataPath, "saves");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }

        // 确保存档文件夹存在
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);
    }

    // === 保存到指定槽位（1、2、3...）===
    public void Save(int slot)
    {
        SaveData data = new SaveData(PlayerMovement.instance.transform.position, 0, NarrativeManager.instance.narrativeChoice);

        string json = JsonUtility.ToJson(data, prettyPrint: true);
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        File.WriteAllText(path, json);

        Debug.Log($"存档成功：槽位 {slot}");
    }

    // === 从指定槽位读取 ===
    public SaveData Load(int slot)
    {
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        if (!File.Exists(path))
        {
            Debug.LogWarning($"槽位 {slot} 无存档");
            return null;
        }

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log($"读档成功：槽位 {slot}");
        return data;
    }

    //删除存档
    public void DeleteSave(int slot)
    {
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("已删除存档");
        }
    }

    // === 判断某个槽位是否存在存档 ===
    public bool HasSave(int slot)
    {
        string path = Path.Combine(saveFolder, $"save{slot}.json");
        return File.Exists(path);
    }
}
