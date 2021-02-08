using Dapper;
using Microsoft.EntityFrameworkCore;
using SODtaAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SODtaAccess.Data.Repository
{
    internal class StoreProcedureRepository  
    {
        private readonly ApplicationDbContext _db;
        private static string ConnectionString = "";

        public StoreProcedureRepository(ApplicationDbContext db)
        {
            _db = db;
            ConnectionString = db.Database.GetDbConnection().ConnectionString;
        }



        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {
            using SqlConnection sqlCon = new SqlConnection(ConnectionString);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));
        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                sqlCon.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<List<T>>  ReturnList<T>(string procedureName, DynamicParameters param = null)
        {
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                sqlCon.Open();
                return (List<T>)await sqlCon.QueryAsync<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
