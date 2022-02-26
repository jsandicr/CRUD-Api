using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPrueba.Entities;

namespace WebApiPrueba.Repository
{
    public class LibrosRepository
    {
        private readonly string connection;

        public LibrosRepository(IConfiguration configuration)
        {
            this.connection = configuration.GetConnectionString("DefaultConnection");
        }

        public Libros MapToValue(SqlDataReader reader)
        {
            return new Libros
            {
                Id = (int)reader["Id"],
                Nombre = reader["Nombre"].ToString(),
                CantPaginas = (int)reader["CantPaginas"],
                IdAutor = (int)reader["IdAutor"]
            };
        }

        public async Task<List<Libros>> GetAll()
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllValuesLibros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Libros>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }
                    }
                    return response;
                }
            }
        }
        public async Task<Libros> GetById(int id)
        {
            using(SqlConnection sql = new SqlConnection(connection))
            {
                using(SqlCommand cmd=new SqlCommand("GetValueByIdLibros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    Libros response=null;
                    await sql.OpenAsync();
                    using(var reader = await cmd.ExecuteReaderAsync())
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

        public async Task Insert(Libros libros)
        {
            using (SqlConnection sql = new SqlConnection(connection))
            {
                using(SqlCommand cmd=new SqlCommand("InsertValueLibros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", libros.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CantPaginas", libros.CantPaginas));
                    cmd.Parameters.Add(new SqlParameter("@IdAutor", libros.IdAutor));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task DeleteById(int id)
        {
            using(SqlConnection sql=new SqlConnection(connection))
            {
                using(SqlCommand cmd=new SqlCommand("DeleteByIdLibros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task Edit(int id, Libros libros)
        {
            using(SqlConnection sql=new SqlConnection(connection))
            {
                using(SqlCommand cmd=new SqlCommand("EditValueLibros",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", libros.Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CantPaginas", libros.CantPaginas));
                    cmd.Parameters.Add(new SqlParameter("@IdAutor", libros.IdAutor));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

    }
}
