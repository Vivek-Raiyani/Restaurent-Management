namespace pizzashop.data.ViewModels.TableSection;

public class TableVM
{
    public int TableId { get; set; }

    public string TableName { get; set;}=null!;

    public string Status { get; set; } = null!;

    public short Capacity { get; set; } 

    public int SectionId { get; set; }
}
