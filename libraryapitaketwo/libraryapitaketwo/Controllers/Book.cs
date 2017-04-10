using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace libraryapitaketwo.Models

{
    public class Book
    {
        public Book() { }
        public Book(SqlDataReader reader)
        {
            this.Id = (int)reader["id"];
            this.Title = reader["Title"].ToString();
            this.Author = reader["Author"].ToString();
            this.YearPublished = reader["YearPublished"] as int?;
            this.Genre = reader["Genre"].ToString();
            this.IsCheckedOut = reader["IsCheckedOut"] as bool?;
            this.LastCheckedOutDate = reader["LastCheckedOutDate"] as DateTime?;
            this.DueBackDate = reader["DueBackDate"] as DateTime?;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? YearPublished { get; set; }
        public string Genre { get; set; }
        public bool? IsCheckedOut { get; set; }
        public DateTime? LastCheckedOutDate { get; set; }
        public DateTime? DueBackDate { get; set; }
    }
}