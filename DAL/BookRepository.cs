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
        public List<Book> AllBookByAuthor(Author author)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string AuthorId = string.Format(@"select AuthorId from[Library].[dbo].[Author] 
                                                    where FirstName like '{0}' and LastName like '{1}' and MiddleName like '{2}'",
                    author.FirstName,author.LastName,author.MiddleName);
                int Id = (int)_db.ExecuteScalar(AuthorId);

                string result = string.Format(@"select * from [Library].[dbo].[Book] a
                                                where a.AuthorId = {0} ",Id);
                List<Book> AllBooksByAuthor = _db.Query<Book>(result).ToList();
                return AllBooksByAuthor;
            }
        }

        public List<Book> AllBookByGenre(Genre genre)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string GenreId = string.Format(@"select [GenreId] from[Library].[dbo].[Genre] where [GenreName] like '{0}'",genre.genreName);
                int Id = (int)_db.ExecuteScalar(GenreId, genre);

                string result = string.Format(@"select * from [Library].[dbo].[Book] a
                                    left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                    left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                    where d.GenreId = {0} ", Id);
                List<Book> AllBooksByGenre = _db.Query<Book>(result).ToList();
                return AllBooksByGenre;
            }
        }

        public bool DeleteBook(int id)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string search =string.Format( @"select count([BookBookId]) from [dbo].[LibraryCard] where [BookBookId]={0}",id);

                int row = (int)_db.ExecuteScalar(search);
                if (row <= 0)
                {
                    string searchGenre = string.Format(@"select [GenreId] from [Library].[dbo].[BookGenreLink] where [BookId]={0}", id);
                    int searchtGenre = (int)_db.ExecuteScalar(searchGenre);//Id жанра
                    string delete = string.Format(@"delete from [Library].[dbo].[Book] where [BookId]={0}
                                    delete from [Library].[dbo].[BookGenreLink] where BookId={0} and GenreId={1}", id,searchtGenre);
                    _db.Execute(delete);
                    return true;
                }
                return false;
            }
        }

        public List<LibraryCard> InsertBook(LibraryCard libraryCard)
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string AuthorBook = string.Format(@"select count(*) 
                                from[Library].[dbo].[Author]
                                where[FirstName] like '{0}' and[LastName] like'{1}' and[MiddleName] like '{2}'",
                                libraryCard.FirstName,
                                libraryCard.LastName,
                                libraryCard.MiddleName);
                int rows =(int) _db.ExecuteScalar(AuthorBook);//существуют ли строчки с таким автором

                if (rows<=0)
                {
                    string InsertAuthor = string.Format(@"insert into [Library].[dbo].[Author] ([FirstName],[LastName],[MiddleName]) 
                                        values('{0}', '{1}', '{2}')", libraryCard.FirstName, libraryCard.LastName, libraryCard.MiddleName);
                    _db.Execute(InsertAuthor);
                }
                string ResultAuthor =string.Format( @"select [AuthorId] 
                                from[Library].[dbo].[Author]
                                where[FirstName] like '{0}' and[LastName] like'{1}' and[MiddleName] like '{2}'",
                                libraryCard.FirstName,
                                libraryCard.LastName,
                                libraryCard.MiddleName);
                int ResultsAuthor =(int) _db.ExecuteScalar(ResultAuthor);//Id автора

                string GenreBook = string.Format(@"select count(*) 
	                                    from [Library].[dbo].[Genre]
	                                    where [GenreName] like '{0}'", libraryCard.GenreName);
                rows =(int) _db.ExecuteScalar(GenreBook);//существуют ли строчки с таким жанром
                if (rows <=0)
                {
                    string InsertGenre = string.Format(@"insert into [Library].[dbo].[Genre] ([GenreName]) values('{0}')", libraryCard.GenreName);
                    _db.Execute(InsertGenre);
                }

                string ResultGenre = string.Format(@"select [GenreId] 
                                from[Library].[dbo].[Genre]
                                where[GenreName] like '{0}'", libraryCard.GenreName);
                int resultGenre=(int) _db.ExecuteScalar(ResultGenre);//Id жанра

                string InsertBook = string.Format(@"insert into [Library].[dbo].[Book] ([Name],[AuthorId]) 
                                        values('{0}',{1})
                                        select [BookId]=SCOPE_IDENTITY()", libraryCard.Name, ResultsAuthor);
                _db.Execute(InsertBook);

                string ResultBook = string.Format(@"select [BookId] 
                                from[Library].[dbo].[Book]
                                where[Name] like '{0}' and AuthorId={1}", libraryCard.Name, ResultsAuthor);
                int resultBook = (int)_db.ExecuteScalar(ResultBook);//Id книги

                string BookGenre = string.Format(@"insert into [Library].[dbo].[BookGenreLink] ([BookId],[GenreId]) values({0},{1})", 
                                                                                                                        resultBook,
                                                                                                                        resultGenre);

                _db.Execute(BookGenre);
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
                _db.Execute(genresearch);


                string genre = @"select a.[Name],
                                    b.FirstName, b.LastName, b.MiddleName,
                                    d.GenreName
                                    from [Library].[dbo].[Book] a
                                    left join[Library].[dbo].[Author] b on b.AuthorId = a.AuthorId
                                    left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                    left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                    where a.BookId = @BookId";
                _db.Execute(genre);
                return true;
            }
        }
    }
}
