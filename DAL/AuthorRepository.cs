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

        public bool DeleteAuthor(Author author)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string authorSearch = string.Format(@"select AuthorId from [Library].[dbo].[Author] 
                                    where FirstName like '{0}' and LastName like '{1}' and MiddleName like '{2}'",
                                    author.FirstName,
                                    author.LastName,
                                    author.MiddleName);
                int Id = (int)_db.ExecuteScalar(authorSearch);
                string AuthorBook = string.Format(@"select count(*) from [Library].[dbo].[Book] where AuthorId={0}",Id);
                int rows = (int)_db.ExecuteScalar(AuthorBook);
                if (rows<=0)
                {
                    string Delete = string.Format(@"delete from [Library].[dbo].[Author] where AuthorId={0}",Id);
                    _db.Execute(Delete);
                    return true;
                }
                return false;
            }
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

        public List<LibraryCard> GetAllBookByAuthor(Author author)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string ByAuthor = string.Format(@"select * from [Library].[dbo].[Book] a
                                        left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                        left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                        left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId] 
                                        where b.FirstName like '{0}' and b.LastName like '{1}' and b.MiddleName like '{2}'",
                                        author.FirstName,
                                        author.LastName,
                                        author.MiddleName);
                List<LibraryCard> authors = _db.Query<LibraryCard>(ByAuthor).ToList();
                return authors;
            }
        }
    }
}
