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

        [HttpPost]
        public Book UpdateBook(int id, [FromBody]Book book)
        {
            const string connectionString =
                            @"Server=localhost\SQLEXPRESS;Database=libraryapi;Trusted_Connection=True;";

            var rv = new List<Book>();
            using (var connection = new SqlConnection(connectionString))
            {
                var query = @"UPDATE [dbo].[LibraryTable] SET
                [Title] = @Title
                ,[Author] = @Author
                ,[YearPublished] = @YearPublished
                ,[Genre] = @Genre
                ,[IsCheckedOut] = @IsCheckedOut
                ,[LastCheckedOutDate] = @LastCheckedOutDate
                ,[DueBackDate] = @DueBackDate
                WHERE Id = @Id";
                var cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", book.Author);
                cmd.Parameters.AddWithValue("@YearPublished", book.YearPublished);
                cmd.Parameters.AddWithValue("@Genre", book.Genre);
                cmd.Parameters.AddWithValue("@IsCheckedOut", book.IsCheckedOut);
                cmd.Parameters.AddWithValue("@LastCheckedOutDate",(object) book.LastCheckedOutDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DueBackDate", (object) book.DueBackDate ?? DBNull.Value);
                connection.Open();
                var reader = cmd.ExecuteNonQuery();
                connection.Close();
            }
            book.Id = id;
            return book;
        }
    }
}