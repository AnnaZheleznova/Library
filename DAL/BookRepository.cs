using Library.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Library.DAL
{
    public class BookRepository : IBookRepository
    {
        public List<LibraryCard> AllBookByAuthor(Author author)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string AuthorId = @"select AuthorId from[Library].[dbo].[Author] where FirstName=@FirstName and LastName=@LastName and MiddleName=@MiddleName";
                int Id =(int) _db.ExecuteScalar(AuthorId,author);

                string result = string.Format(@"select a.[Name],
                                    b.FirstName, b.LastName, b.MiddleName,
                                    d.GenreName
                                    from [Library].[dbo].[Book] a
                                    left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                    left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                    left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                    where a.AuthorId = {0} ",Id);
                List<LibraryCard> AllBooksByAuthor = _db.Query<LibraryCard>(result).ToList();
                return AllBooksByAuthor;
            }
        }

        public List<LibraryCard> AllBookByGenre(Genre genre)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string GenreId = @"select [GenreId] from[Library].[dbo].[Genre] where [GenreName]=@GenreName";
                int Id = (int)_db.ExecuteScalar(GenreId, genre);

                string result = string.Format(@"select a.[Name],
                                    b.FirstName, b.LastName, b.MiddleName,
                                    d.GenreName
                                    from [Library].[dbo].[Book] a
                                    left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                    left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                    left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                    where a.AuthorId = {0} ", Id);
                List<LibraryCard> AllBooksByGenre = _db.Query<LibraryCard>(result).ToList();
                return AllBooksByGenre;
            }
        }

        public bool DeleteBook(int id)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string search =string.Format( @"select count([BookBookId]) from [dbo].[LibraryCard] where [BookBookId]={0}",id);
                int row = (int)_db.ExecuteScalar(search);
                if (row == 0)
                {
                    string delete = string.Format(@"delete from [Library].[dbo].[Book] where [BookId]={0}", id);
                    int RowDelete = _db.Execute(delete);
                    return true;
                }
                return false;
            }
        }

        public List<LibraryCard> InsertBook(Book book, Author author, Genre genre)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string AuthorBook = @"select count(*) 
                                from[Library].[dbo].[Author]
                                where[FirstName] like '@FirstName' and[LastName] like'@LastName' and[MiddleName] like '@MiddleName'";
                int rows = (int)_db.ExecuteScalar(AuthorBook);

                if (rows==0)
                {
                    string InsertAuthor = @"insert into [Library].[dbo].[Author] ([FirstName],[LastName],[MiddleName]) 
                                        values('@FirstName', '@LastName', '@MiddleName')
                                        select[AuthorId] = SCOPE_IDENTITY()";
                    rows = (int)_db.ExecuteScalar(InsertAuthor);
                }
                string ResultAuthor = @"select [AuthorId] 
                                from[Library].[dbo].[Author]
                                where[FirstName] like '@FirstName' and[LastName] like'@LastName' and[MiddleName] like '@MiddleName'";
                int ResultsAuthor = (int)_db.ExecuteScalar(ResultAuthor);

                string GenreBook = @"select count(*) 
	                                    from [Library].[dbo].[Genre]
	                                    where [GenreName] like '@GenreName'";
                rows = (int)_db.ExecuteScalar(GenreBook);
                if (rows == 0)
                {
                    string InsertGenre = @"insert into [Library].[dbo].[Genre] ([GenreName]) values('@GenreName')
	                                        select [GenreId]=SCOPE_IDENTITY()";
                    rows = (int)_db.ExecuteScalar(InsertGenre);
                }

                string ResultGenre = @"select [GenreId] 
                                from[Library].[dbo].[Genre]
                                where[GenreName] like '@GenreName'";
                int resultGenre= (int)_db.ExecuteScalar(ResultGenre);

                string InsertBook = string.Format(@"insert into [Library].[dbo].[Book] ([Name],[AuthorId]) 
                                        values('@Name',{0})
                                        select [BookId]=SCOPE_IDENTITY()", ResultsAuthor);
                int resultBook = (int)_db.ExecuteScalar(InsertBook);

                string BookGenre = string.Format(@"insert into [Library].[dbo].[BookGenreLink] ([BookId],[GenreId]) values({0},{1})", 
                    resultBook,
                    resultGenre);

                string result = string.Format(@"select a.[Name],
                                                b.FirstName, b.LastName, b.MiddleName,
                                                d.GenreName
                                                from [Library].[dbo].[Book] a
                                                left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                                left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                                left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                                where a.AuthorId = {0} and a.BookId={1} and c.[GenreId]={2}", 
                                                ResultsAuthor, 
                                                resultBook, 
                                                resultGenre);
                List<LibraryCard> insertbook = _db.Query<LibraryCard>(result).ToList();
                return insertbook;
            }
        }

        public bool NewGenre(Book book)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string genresearch = @"select d.GenreName
                                        from [Library].[dbo].[Book] a
                                        left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                        left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                        left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                        where a.BookId = 5 and d.GenreName like'@GenreName'";
                int row = _db.Execute(genresearch);


                string genre = @"select a.[Name],
                                    b.FirstName, b.LastName, b.MiddleName,
                                    d.GenreName
                                    from [Library].[dbo].[Book] a
                                    left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                    left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                    left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                    where a.BookId = @BookId";
                return true;
            }
        }
    }
}
