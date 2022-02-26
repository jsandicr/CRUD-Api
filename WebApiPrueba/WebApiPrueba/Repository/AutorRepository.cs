using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPrueba.Entities;
using WebApiPrueba.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace WebApiPrueba.Repository
{
    public class AutorRepository
    {
        private readonly string connectionString;

        public AutorRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public Autor MapToValue(SqlDataReader reader)
        {
            return new Autor()
            {
                Id = (int)reader["Id"],
                Nombre = reader["Nombre"].ToString(),
                Apellido = reader["Apellido"].ToString(),
                Edad = (int)reader["Edad"]
            };
        }

        public async Task<List<Autor>> GetAll()
        {

            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd=new SqlCommand("GetAllValuesAutores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Autor>();
                    await sql.OpenAsync();

                    using(var reader=await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync()) 
                        {
                            response.Add(MapToValue(reader));
                        }
                    }

                    return response;
                }
            }
        }

        public async Task<Autor> GetById(int id)
        {
            using(SqlConnection sql=new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetValueByIdAutores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    Autor response = null;
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response = MapToValue(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insert(Autor autor)
        {
            using(SqlConnection sql=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("InsertValueAutores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", autor.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Apellido", autor.Apellido));
                    cmd.Parameters.Add(new SqlParameter("@Edad", autor.Edad));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task DeleteById(int id)
        {
            using(SqlConnection sql=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("DeleteByIdAutores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Edit(int id,Autor autor)
        {
            using(SqlConnection sql=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand("EditValueAutores", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", autor.Id));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", autor.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Apellido", autor.Apellido));
                    cmd.Parameters.Add(new SqlParameter("@Edad", autor.Edad));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }
    }
}
