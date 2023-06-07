using System;

namespace Dictionary.Service.Attributes;

/// <summary>
///     Đánh dấu bỏ qua cập nhật
///     Gắn với các trường tự sinh dữ liệu từ db
/// </summary>
public class IgnoreUpdateAttribute : Attribute
{
}