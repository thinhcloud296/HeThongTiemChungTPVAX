using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TPVAXWinform_DAL
{
    public static class DBConnect
    {
        private static string strConnect;
        static DBConnect()
        {
            try
            {
                var config = ConfigurationManager.ConnectionStrings["TPVAX_DB"];
                if (config != null)
                {
                    strConnect = config.ConnectionString;
                }
                else
                {
                    // Nếu không tìm thấy, gán chuỗi rỗng hoặc ném lỗi rõ ràng
                    throw new Exception("Không tìm thấy chuỗi kết nối 'TPVAX_DB' trong App.config.");
                }
            }
            catch (Exception ex)
            {
                // Ghi log hoặc ném tiếp lỗi để debug
                throw new Exception("Lỗi khởi tạo DBConnect: " + ex.Message);
            }
        }

        private const int DefaultTimeout = 30;

        public static SqlConnection GetConnection() => new SqlConnection(strConnect);

        public static SqlParameter Param(string name, object value, SqlDbType? type = null, int? size = null)
        {
            var p = new SqlParameter(name, value ?? DBNull.Value);
            if (type.HasValue) p.SqlDbType = type.Value;
            if (size.HasValue) p.Size = size.Value;
            return p;
        }

        public static bool TestConnection()
        {
            try { using (var c = GetConnection()) { c.Open(); return true; } }
            catch { return false; }
        }

        // --- Các hàm sẵn có của bạn (ExecuteQuery/ExecuteNonQuery/ExecuteScalar/ExecuteReader/ExecuteInTransaction) ---
        // (giữ nguyên)

        // ============================================================
        // ============  PHẦN QUAN TRỌNG: BUFFER EDITABLE  ============
        // ============================================================

        /// <summary>
        /// Một phiên làm việc với DataTable offline. Gọi Save() để đẩy INSERT/UPDATE/DELETE về SQL.
        /// </summary>
        public sealed class EditableBuffer : IDisposable
        {
            private readonly SqlDataAdapter _adapter;
            private readonly SqlCommandBuilder _builder;
            public DataTable Table { get; } = new DataTable();

            /// <param name="selectSql">
            /// Câu SELECT phải trả về đầy đủ các cột cần sửa và **có Primary Key** (để Update/Delete hoạt động).
            /// Ví dụ: SELECT * FROM dbo.HoSoTiemChung
            /// </param>
            public EditableBuffer(string selectSql)
            {
                _adapter = new SqlDataAdapter(selectSql, strConnect)
                {
                    MissingSchemaAction = MissingSchemaAction.AddWithKey,
                    // Nếu muốn giữ RowState sau Fill:
                    // AcceptChangesDuringFill = false
                };

                // Tự sinh Insert/Update/Delete dựa trên PK (yêu cầu bảng có PK)
                _builder = new SqlCommandBuilder(_adapter)
                {
                    ConflictOption = ConflictOption.OverwriteChanges // có thể đổi thành CompareAllSearchableValues nếu cần kiểm soát cạnh tranh
                };

                _adapter.Fill(Table);

                // Nếu vì lý do nào đó PK không tự nhận ra, có thể set thủ công:
                // Table.PrimaryKey = new[] { Table.Columns["MaHSTC"] };
            }

            /// <summary>Ghi mọi thay đổi trong Table về DB. Trả về số dòng ảnh hưởng.</summary>
            public int Save()
            {
                // KHÔNG gọi AcceptChanges() trước Update; sẽ mất RowState
                return _adapter.Update(Table);
            }

            /// <summary>Nạp lại dữ liệu từ DB (mất thay đổi chưa Save).</summary>
            public void Reload()
            {
                Table.Clear();
                _adapter.Fill(Table);
            }

            public void Dispose()
            {
                _builder?.Dispose();
                _adapter?.Dispose();
                Table?.Dispose();
            }
        }
        public static int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            // (Giả sử bạn có hàm GetConnection() hoặc biến 'connectionString' static)
            using (SqlConnection connection = GetConnection()) // Hoặc new SqlConnection(connectionString)
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.CommandType = commandType;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery(); // <-- Dùng hàm .ExecuteNonQuery() của .NET
                    connection.Close();

                    return rowsAffected; // Trả về số dòng bị ảnh hưởng
                }
            }
        }
        public static DataTable ExecuteQuery(string sqlOrSp,
                                     CommandType cmdType = CommandType.Text,
                                     params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sqlOrSp, conn) { CommandType = cmdType, CommandTimeout = 30 })
            using (var da = new SqlDataAdapter(cmd))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }

        /// <summary>
        /// Tạo một phiên buffer từ câu SELECT. Dùng xong nhớ Dispose (hoặc dùng with using).
        /// </summary>
        public static EditableBuffer CreateBuffer(string selectSql)
            => new EditableBuffer(selectSql);

        public static object ExecuteScalar(string sqlOrSp,
                                         CommandType cmdType = CommandType.Text,
                                         params SqlParameter[] parameters)
        {
            object result = null;

            using (var conn = GetConnection())
            using (var cmd = new SqlCommand(sqlOrSp, conn) { CommandType = cmdType, CommandTimeout = DefaultTimeout })
            {
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }

                conn.Open();
                result = cmd.ExecuteScalar(); // Gọi hàm thực thi
            }
            return result; // Trả về kết quả (có thể là null)
        }
    }
}
