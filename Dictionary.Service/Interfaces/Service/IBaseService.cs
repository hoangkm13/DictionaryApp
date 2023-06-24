using System.Collections;
using System.Threading.Tasks;
using Dictionary.Service.DtoEdit.Authentication;
using Dictionary.Service.Model;

namespace Dictionary.Service.Interfaces.Service;

public interface IBaseService
{
    /// <summary>
    ///     Thêm dữ liệu
    /// </summary>
    /// <param name="entity">dữu liệu</param>
    Task<object> InsertAsync<T>(object entity);


    /// <summary>
    ///     Cập nhật dữ liệu
    /// </summary>
    /// <param name="entity">dữu liệu</param>
    Task<object> UpdateAsync<T>(object entity);


    /// <summary>
    ///     Xóa dữ liệu
    /// </summary>
    /// <param name="entity">dữu liệu</param>
    Task<bool> DeleteAsync(object entity);


    /// <summary>
    ///     Lấy dữ liệu Combobox
    /// </summary>
    Task<IList> GetComboboxPaging<T>(string columns, string filter, string sort);

    /// <summary>
    ///     Lấy dữ liệu bảng
    /// </summary>
    Task<ApiResult> GetDataTable<T>(FilterTable filterTable);

    /// <summary>
    ///     Lưu dữ liệu
    /// </summary>
    Task<T> SaveData<T>(T model, int mode);
}