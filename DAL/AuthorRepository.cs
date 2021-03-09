using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library.DAL
{
    public class AuthorRepository : IAuthorRepository
    {
        public bool AddAuthor()
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuthor()
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAllAuthor()
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string author= @"select *from[Library].[dbo].[Author] ";
                List<Author> authors = _db.Query<Author>(author).ToList();
                return authors;
            }
        }

        public List<Author> GetAllBookByAuthor()
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string ByAuthor = @"select a.[Name],
                                        b.FirstName, b.LastName, b.MiddleName,
                                        d.GenreName
                                        from [Library].[dbo].[Book] a
                                        left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                        left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                        left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId] 
                                        where b.FirstName like '' and b.LastName like '' ";
                List<Author> authors = _db.Query<Author>(ByAuthor).ToList();
                return authors;
            }
        }
    }
}
