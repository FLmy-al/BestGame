[System.Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    // ÅÐ¶¨¿Õ¸ñ×Ó
    public bool IsEmpty => item == null || count <= 0;

    public void Clear()
    {
        item = null;
        count = 0;
    }
}