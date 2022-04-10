// See https://aka.ms/new-console-template for more information
public class Audit
{
    public long ID { get; set; } // bigint, not null
    public DateTime? Updatetime { get; set; } // datetime, null
    public string? UserName { get; set; } // varchar(50), null
    public string? TableName { get; set; } // varchar(50), null
    public int? DeleteCount { get; set; } // int, null
    public string? DeleteXML { get; set; } // XML(.), null
    public int? InsertCount { get; set; } // int, null
    public string? InsertXML { get; set; } // XML(.), null
}