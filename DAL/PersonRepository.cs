﻿using Dapper;
using Library.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Library.DAL
{
    public class PersonRepository : IPersonRepository
    {
        //public List<LibraryCard> GetPersonBooks(int Id)//6.1.5
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        var persons = _db.Query<LibraryCard>(string.Format(@"select b.*, a.*, d.*
        //                                                                from[Library].[dbo].[LibraryCard]
        //                                                                left join[Library].[dbo].[Book] b on b.[BookId] =[BookBookId]
        //                                                                left join[Library].[dbo].[BookGenreLink] c on c.[BookId] = b.BookId
        //                                                                left join[Library].[dbo].[Genre] d on d.GenreId = c.[GenreId]
        //                                                                left join[Library].[dbo].[Author] a on a.AuthorId = b.AuthorId
        //                                                                where PersonPersonId = {0}", Id)).ToList();
        //        return persons;
        //    }
        //}

        //public List<Person>  InsertPerson(Person person)//6.1.1
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        List<Person> NewPerson = _db.Query<Person>(string.Format(@"insert into [Library].[dbo].[Person]
        //                            ([BirthDay],[FirstName],[LastName],[MiddleName]) 
        //                            values('{0}','{1}','{2}','{3}')
        //                            select*from [Library].[dbo].[Person] where PersonId= SCOPE_IDENTITY()", 
        //                            person.BirthDay,
        //                            person.FirstName,
        //                            person.LastName,
        //                            person.MiddleName)).ToList();
        //        return NewPerson;
        //    }
        //}

        //public List<Person> UpdatePerson(Person person)//6.1.2
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        List<Person> UpdatePerson = _db.Query<Person>(string.Format(@"update [Library].[dbo].[Person] 
        //                                        set[BirthDay] = '{0}',
        //                                        [FirstName] = '{1}',
        //                                        [LastName] = '{2}',
        //                                        [MiddleName] = '{3}'
        //                                        where[PersonId] = {4}
        //                                        select * from [Library].[dbo].[Person] where PersonId= ", 
        //                                        person.BirthDay, 
        //                                        person.FirstName,
        //                                        person.LastName,
        //                                        person.MiddleName, 
        //                                        person.Id)).ToList();

        //            return UpdatePerson;
        //    }
        //}

        //public bool DeletePersonFIO(Person person)//6.1.4
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        int rowsAffected = _db.Execute(string.Format(@"DELETE FROM [Library].[dbo].[Person] 
        //                                            WHERE FirstName+LastName+MiddleName = '{0}'+'{1}'+'{2}'", 
        //                                            person.FirstName,
        //                                            person.LastName,
        //                                            person.MiddleName));

        //        if (rowsAffected > 0)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //}

        //public bool DeletePersonId(int Id)//6.1.3
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        int rowsAffected = _db.Execute(string.Format(@"DELETE FROM [Library].[dbo].[Person] 
        //                                                        WHERE PersonId = {0}", Id));

        //        if (rowsAffected > 0)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //}

        //public List<LibraryCard> InsertLibraryCard(int bookId, int personId)//6.1.6
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        List<LibraryCard> GetBook = _db.Query<LibraryCard>(string.Format(@"insert into [Library].[dbo].[LibraryCard]
        //                            ([BookBookId],[PersonPersonId]) 
        //                            values('{0}','{1}')
        //                            select a.FirstName, a.LastName, a.MiddleName, b.Name
        //                            from  [Library].[dbo].[LibraryCard]
        //                            left join [Library].[dbo].[Person] a  on [PersonId]=[PersonPersonId]
        //                            left join [Library].[dbo].[Book] b on [BookId]=[BookBookId]
        //                            where PersonPersonId={1}", bookId, personId)).ToList();
        //        return GetBook;
        //    }
        //}

        //public List<LibraryCard> DeleteLibraryCard(int bookId, int personId)//6.1.7
        //{
        //    using (IDbConnection _db = new SqlConnection("Server=localhost\\SQLEXPRESS01;Database=Library;Trusted_Connection=True;"))
        //    {
        //        List<LibraryCard> PutBook = _db.Query<LibraryCard>(string.Format(@"delete from [Library].[dbo].[LibraryCard]
        //                                where BookBookId={0} and PersonPersonId={1}
        //                                select a.FirstName, a.LastName, a.MiddleName, b.Name
        //                                from  [Library].[dbo].[LibraryCard]
        //                                left join [Library].[dbo].[Person] a  on [PersonId]=[PersonPersonId]
        //                                left join [Library].[dbo].[Book] b on [BookId]=[BookBookId]
        //                                where PersonPersonId={1}",bookId, personId)).ToList();
        //        return PutBook;
        //    }
        //}
        public List<LibraryCard> DeleteLibraryCard(int bookId, int personId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeletePersonFIO(Person person)
        {
            throw new System.NotImplementedException();
        }

        public bool DeletePersonId(int Id)
        {
            throw new System.NotImplementedException();
        }

        public List<LibraryCard> GetPersonBooks(int Id)
        {
            throw new System.NotImplementedException();
        }

        public List<LibraryCard> InsertLibraryCard(int bookId, int personId)
        {
            throw new System.NotImplementedException();
        }

        public List<Person> InsertPerson(Person person)
        {
            throw new System.NotImplementedException();
        }

        public List<Person> UpdatePerson(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}

