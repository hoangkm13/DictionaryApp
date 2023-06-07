using System;

namespace Dictionary.Service.Attributes;

/// <summary>
///     Attribute đánh dấu tên bảng
/// </summary>
public class TableAttribute : Attribute
{
    /// <summary>
    ///     Khởi tạo
    /// </summary>
    /// <param name="table">Tên bảng</param>
    public TableAttribute(string table)
    {
        Table = table;
    }

    /// <summary>
    ///     Tên bảng chi tiết
    /// </summary>
    public string Table { get; set; }
}