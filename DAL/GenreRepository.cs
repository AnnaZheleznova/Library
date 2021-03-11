using Dapper;
using Library.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class GenreRepository : IGenreRepository
    {
        public bool AddGenres(Genre genre)//6.4.2
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string genreAdd = string.Format(@"insert into [Library].[dbo].[Genre] (GenreName) values('{0}')",genre.genreName);
                _db.Execute(genreAdd);
                return true;
            }
        }

        public List<Genre> GetGenres()//6.4.1
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string genreAll = @"select *from[Library].[dbo].[Genre] ";
                List<Genre> genres = _db.Query<Genre>(genreAll).ToList();
                return genres;
            }
        }

        public int Statistic(Genre genre)//6.4.3
        {
            using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
            {
                string searchId = string.Format(@"select GenreId from [Library].[dbo].Genre  where GenreName like '{0}'",genre.genreName);
                int Id = (int)_db.ExecuteScalar(searchId);
                string genreAll = string.Format(@"select count(*)
                                    from [Library].[dbo].[Book] a
                                    left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = a.BookId
                                    left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
                                    where d.GenreId = {0}",Id);
                int genres =(int) _db.ExecuteScalar(genreAll);
                return genres;
            }
        }
    }
}
