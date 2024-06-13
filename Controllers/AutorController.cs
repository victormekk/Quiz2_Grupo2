using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quiz2_Grupo2.Models;

namespace Quiz2_Grupo2.Controllers
{
    public class AutorController
    {
        SQLiteAsyncConnection _connection;

        //Constructor vacio
        public AutorController() { }

        //Conexion a la base de datos
        public async Task Init()
        {
            try
            {
                if (_connection is null)
                {
                    SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;

                    if (string.IsNullOrEmpty(FileSystem.AppDataDirectory))
                    {
                        return;
                    }

                    _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBPersonas"), extensiones);

                    var creacion = await _connection.CreateTableAsync<Models.Autor>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Init(): {ex.Message}");
            }
        }

        //Crear metodos crud para la clase personas
        //Create
        public async Task<int> storeAutor(Autor autor)
        {
            await Init();
            if (autor.Id == 0)
            {
                return await _connection.InsertAsync(autor);
            }
            else
            {
                return await _connection.UpdateAsync(autor);
            }
        }

        //Update
        public async Task<int> updateAutor(Autor autor)
        {
            await Init();
            return await _connection.UpdateAsync(autor);
        }

        //Read
        public async Task<List<Models.Autor>> getListAutor()
        {
            await Init();
            return await _connection.Table<Autor>().ToListAsync();
        }

        //Read Element
        public async Task<Models.Autor> getAutors(int pid)
        {
            await Init();
            return await _connection.Table<Autor>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }

        //Delete
        public async Task<int> deleteAutor(int autorID)
        {
            await Init();
            var autorToDelete = await getAutors(autorID);

            if (autorToDelete != null)
            {
                return await _connection.DeleteAsync(autorToDelete);
            }

            return 0;
        }
    }
}
