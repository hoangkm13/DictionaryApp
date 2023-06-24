using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dictionary.Service.DtoEdit.Authentication;
using Dictionary.Service.Exceptions;
using Dictionary.Service.Interfaces.Repo;
using Dictionary.Service.Model;
using Dictionary.Service.Properties;
using Microsoft.Extensions.Configuration;

namespace Dictionary.Repo.Repo;

/// <summary>
///     Repository người dùng
/// </summary>
public class UserRepo : BaseRepo, IUserRepo
{
    private IConfiguration _configuration;

    /// <summary>
    ///     Phương thức khởi tạo
    /// </summary>
    /// <param name="configuration">Config của project</param>
    public UserRepo(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

    public async Task<UserInfo> GetUserInfo(Guid id)
    {
        var res = await Provider.QueryAsync<UserInfo>("Proc_GetUserInfo",
            new Dictionary<string, object> { { "$UserId", id } }, CommandType.StoredProcedure);
        return res?.FirstOrDefault();
    }

    public async Task<UserEntity> Login(LoginModel model)
    {
        var sql = string.Format(@"SELECT * FROM {0} 
                    WHERE (email=@email OR phone=@phone)
                    LIMIT 1;",
            GetTableName(typeof(UserEntity)));
        var param = new Dictionary<string, object>
        {
            { "email", model.account },
            { "phone", model.account }
            // {"password", model.password }
        };
        var result = await Provider.QueryAsync<UserEntity>(sql, param);
        var res = result?.FirstOrDefault();
        if (res == null)
            throw new ValidateException("Tài khoản không tồn tại, vui lòng kiểm tra lại", model,
                int.Parse(ResultCode.WrongAccount));


        var verified = BCrypt.Net.BCrypt.Verify(model.password, res.password);
        if (!verified)
            throw new ValidateException("Mật khẩu không chính xác, vui lòng kiểm tra lại", model,
                int.Parse(ResultCode.WrongPassword));

        if (res.is_block) throw new ValidateException("Tài khoản của bạn không có quyền truy cập", model, 209);
        return result.FirstOrDefault();
    }

    public override async Task<ApiResult> GetDataTable<T>(FilterTable filterTable)
    {
        var table = GetTableName(typeof(UserEntity));
        var columnSql = ParseColumn(string.Join(",", filterTable.fields));

        var param = new Dictionary<string, object>();
        var where = ParseWhere(filterTable.filter, param);

        IDbConnection cnn = null;
        IList result = null;
        var totalRecord = 0;
        try
        {
            cnn = Provider.GetOpenConnection();

            var sb = new StringBuilder(
                $"SELECT user_id, CONCAT(first_name, ' ', last_name) AS user_name, email, phone, " +
                $" CASE WHEN gender = 1 THEN 'Nam' " +
                $" WHEN gender = 2 THEN 'Nữ' " +
                $" ELSE 'Khác' END as gender_name, " +
                $" IF(is_block = true, 'Đã chặn', 'Đang hoạt động') as active" +
                $" FROM {table} " +
                $" WHERE role <> 2");
            var sqlSummary = new StringBuilder($"SELECT COUNT(*) FROM {table} WHERE role <> 2");

            if (!string.IsNullOrWhiteSpace(where))
            {
                sb.Append($" WHERE {where}");
                sqlSummary.Append($" WHERE {where}");
            }


            // Sắp xếp
            if (filterTable.sortBy?.Count > 0 && filterTable.sortType?.Count > 0)
            {
                sb.Append(" ORDER BY ");
                for (var i = 0; i < filterTable.sortBy.Count; i++)
                {
                    sb.Append($" {filterTable.sortBy[i]} {filterTable.sortType[i]}");
                    if (i != filterTable.sortBy.Count - 1) sb.Append(",");
                }
            }
            else
            {
                sb.Append(" ORDER BY user_name DESC");
            }

            if (filterTable.page > 0 && filterTable.size > 0)
                sb.Append($" LIMIT {filterTable.size} OFFSET {(filterTable.page - 1) * filterTable.size}");

            result = await Provider.QueryAsync(cnn, sb.ToString(), param);
            totalRecord = await cnn.ExecuteScalarAsync<int>(sqlSummary.ToString(), param);
        }
        finally
        {
            Provider.CloseConnection(cnn);
        }

        return new ApiResult(200, Resources.getDataSuccess, "", result, totalRecord);
    }
}