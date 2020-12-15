using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
namespace DataAccessLayer
{
    public class DataAccess
    {

        public List<TModel> GetModels<TModel>(string connectionString, string storedProcedureName, int page, int count) where TModel : new()
        {
            var modelType = typeof(TModel);
            var modelFields = modelType.GetFields();
            var response = new List<TModel>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var transacton = sqlConnection.BeginTransaction();

                try
                {

                    var command = new SqlCommand(storedProcedureName, sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Page", page);
                    command.Parameters.AddWithValue("@Count", count);
                    command.Transaction = transacton;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var model = new TModel();
                            foreach (var modelField in modelFields)
                            {
                                var valueFromReader = reader[modelField.Name];
                                if (valueFromReader is DBNull)
                                {
                                    valueFromReader = null;
                                }
                                modelField.SetValue(model, valueFromReader);
                            }
                            response.Add(model);
                        }
                    }

                    transacton.Commit();
                    return response;
                } catch
                {
                    transacton.Rollback();
                    throw new Exception("invalid model or command");
                }
            }
        }
    }
}
