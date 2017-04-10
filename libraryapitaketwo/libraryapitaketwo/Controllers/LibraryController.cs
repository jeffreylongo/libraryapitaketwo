using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using libraryapitaketwo.Models;
using System.Data.SqlClient;

namespace libraryapitaketwo.Controllers
{
    public class LibraryController : ApiController
    {
        [HttpGet]
        public List<Book> GetAllBooks()
        {
            const string connectionString = 
                            @"Server=localhost\SQLEXPRESS;Database=libraryapi;Trusted_Connection=True;";

            var rv = new List<Book>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = "SELECT * FROM LibraryTable";
                var cmd = new SqlCommand(query, connection);
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rv.Add(new Book(reader));
                }
                connection.Close();
            }
            return rv;
        }

    }
}