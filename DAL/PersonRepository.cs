using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;

namespace Library.DAL
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDbConnection _db;

        public PersonRepository()
        {
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        public List<Person> GetPerson()
        {
            return this._db.Query<Person>("select * from [Library].[dbo].[person]").ToList();
        }

        public bool InsertPerson(Person person)
        {
            int rowsAffected = this._db.Execute(@"insert into [Library].[dbo].[person]
                                    ([birth_day],[first_name],[last_name],[middle_name]) 
                                    values('@bithDay','@firstName','@lastName','@middleName')
                                    select*from [Library].[dbo].[person] where person_id= SCOPE_IDENTITY()",
                                    new
                                    {
                                        birthDay = person.birthDay,
                                        firstName = person.firstName,
                                        lastName = person.lastName,
                                        middleName = person.middleName
                                    });
            if (rowsAffected > 0)
            {
                return true;
            }

            return false;

        }

        public bool UpdatePerson(Person person)
        {
            int rowsAffected = this._db.Execute(
                        @"update [Library].[dbo].[person] 
                            set[birth_day] = '@bithDay',
                            [first_name] = '@firstName',
                            [last_name] = '@lastName',
                            [middle_name] = '@middleName'
                            where[person_id] =" + person.personId, person);

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeletePersonFIO(string fio)
        {
            int rowsAffected = this._db.Execute(@"DELETE FROM [Customer] WHERE first_Name+last_Name+middle_Name = @FIOperson",
                new { FIOperson = fio });

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeletePersonId(int Id)
        {
            int rowsAffected = this._db.Execute(@"DELETE FROM [Customer] WHERE CustomerID = @personId",
                new { personId = Id });

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public bool GetBookPerson(int bookId, int personId)
        {
            int rowsAffected = this._db.Execute(@"insert into [Library].[dbo].[library_card]
                                    ([book_book_id],[person_person_id]) 
                                    values('@bookbookId','@personpersonId')
                                    select 'fio'=a.first_name+' '+a.last_name+' '+a.middle_name,
                                    'bookName'=b.name
                                    from  [Library].[dbo].[library_card]
                                    left join [Library].[dbo].[person] a  on [person_id]=[person_person_id]
                                    left join [Library].[dbo].[book] b on [book_id]=[book_book_id]
                                    where person_person_id=@personId",
                                                new { bookbookId= bookId, personpersonId= personId });
            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public bool PutBookPerson(int bookId, int personId)
        {
            int rowsAffected = this._db.Execute(@"delete from [Library].[dbo].[library_card]
                                        where book_book_id=@bookbookId and person_person_id=@personpersonId
                                        select 'fio'=a.first_name+' '+a.last_name+' '+a.middle_name,
                                        'bookName'=b.name
                                        from  [Library].[dbo].[library_card]
                                        left join [Library].[dbo].[person] a  on [person_id]=[person_person_id]
                                        left join [Library].[dbo].[book] b on [book_id]=[book_book_id]
                                        where person_person_id=@personId",
                                                new { bookbookId = bookId, personpersonId = personId });
            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

    }
}

