using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    // 存档路径
    private static string savePath => Path.Combine(Application.persistentDataPath, "saveData.json");

    // 1. 存档
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("存档成功：" + savePath);
    }

    // 2. 读档
    public static SaveData Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("没有找到存档，创建新存档");
            return new SaveData(); // 返回默认数据
        }

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);
        Debug.Log("读档成功");
        return data;
    }

    // 3. 删除存档
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("已删除存档");
        }
    }
}
