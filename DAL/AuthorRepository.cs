using Dapper;
using Library.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DAL
{
    public class AuthorRepository : IAuthorRepository
    {
        //public List<Author> AddAuthor(Author author)//6.3.3
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        string addAuthor = string.Format(@"insert into [Library].[dbo].[Author] (FirstName,LastName,MiddleName) values ('{0}','{1}','{2}') select AuthorId=SCOPE_IDENTITY()",
        //                                                                        author.FirstName, author.LastName, author.MiddleName);

        //        int id =(int)_db.Execute(addAuthor);
        //        if (author.Books.ElementAt(0).Name != null)
        //        {
        //            string addBook = string.Format(@"insert into [Library].[dbo].[Book] (Name, AuthorId) values ('{0}',{1})", author.Books.ElementAt(0).Name, id);
        //            _db.Execute(addBook);
        //        }
        //        string result = string.Format(@"select * from [Library].[dbo].[Book] b
        //                                        left join [Library].[dbo].[Author] a on a.AuthorId=b.AuthorId 
        //                                        where a.AuthorId={0}",id);
        //        List<Author> authors=_db.Query<Author>(result).ToList();
        //        return authors;
        //    }
        //}

        //public bool DeleteAuthor(Author author)//6.3.4
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        string authorSearch = string.Format(@"select AuthorId from [Library].[dbo].[Author] 
        //                            where FirstName like '{0}' and LastName like '{1}' and MiddleName like '{2}'",
        //                            author.FirstName,
        //                            author.LastName,
        //                            author.MiddleName);
        //        int Id = (int)_db.ExecuteScalar(authorSearch);
        //        string AuthorBook = string.Format(@"select count(*) from [Library].[dbo].[Book] where AuthorId={0}",Id);
        //        int rows = (int)_db.ExecuteScalar(AuthorBook);
        //        if (rows<=0)
        //        {
        //            string Delete = string.Format(@"delete from [Library].[dbo].[Author] where AuthorId={0}",Id);
        //            _db.Execute(Delete);
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        //public List<Author> GetAllAuthor()//6.3.1
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        string author= @"select *from[Library].[dbo].[Author] ";
        //        List<Author> authors = _db.Query<Author>(author).ToList();
        //        return authors;
        //    }
        //}

        //public List<LibraryCard> GetAllBookByAuthor(Author author)//6.3.2
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        string ByAuthor = string.Format(@"select * from [Library].[dbo].[Book] a
        //                                left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
        //                                left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
        //                                left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId] 
        //                                where b.FirstName like '{0}' and b.LastName like '{1}' and b.MiddleName like '{2}'",
        //                                author.FirstName,
        //                                author.LastName,
        //                                author.MiddleName);
        //        List<LibraryCard> authors = _db.Query<LibraryCard>(ByAuthor).ToList();
        //        return authors;
        //    }
        //}
        public List<Author> AddAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuthor(Author author)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAllAuthor()
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAllBookByAuthor(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
