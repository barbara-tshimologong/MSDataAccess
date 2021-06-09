using System;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace MSDataAccess.Test
{
    public class DataAccessTest
    {

        string _connectionString = "Data Source=LAPTOP-77CJM0P7;User ID=sa;Password='admin';Initial Catalog=AppFactory2020;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        [Fact]
        public void GetString()
        {
            //Arrange

            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = CommandType.Text,
                CommandText = "SELECT FullName FROM Person WHERE PersonId = 1"
            };

            var expected = "Barbara Engle";

            //Action

            var actual = da.GetString();
            
            //Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetInt()
        {
            //Arrange

            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT PersonTypeId FROM PersonType WHERE Description='Manager'"
            };

            var expected = 1;

            //Action

            var actual = da.GetInt();

            //Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBoolean()
        {
            //Arrange

            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT IsActive FROM Person WHERE Surname='Engle'"
            };

            var expected = true;

            //Action

            var actual = da.GetBoolean();

            //Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDouble()
        {
            //Arrange

            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT DailyAllowance FROM Person WHERE Surname='Engle'"
            };

            var expected = 0;

            //Action

            var actual = da.GetDouble();

            //Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDateTime()
        {
            //Arrange

            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = System.Data.CommandType.Text,
                CommandText = "SELECT StartDate FROM Person WHERE Surname='Engle'"
            };

            var expected = Convert.ToDateTime("2020-02-01");

            //Action

            var actual = da.GetDateTime();

            //Assert

            Assert.IsType<DateTime>(actual);
        }

        [Fact]
        public void GetList_DataReader()
        {
            //Arrange
            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandText = "FetchInternList"
            };

            //Action
            var list = da.GetList_DataReader();

            //Assert
            
            //Assert.NotEmpty(list);
            Assert.NotNull(list);
        }

        [Fact]
        public void GetList_DataAdapter()
        {
            //Arrange
            DataAccess da = new DataAccess(_connectionString)
            {
                CommandText = "SELECT * FROM Person"
            };

            //Action
            var result = da.GetCollection_DataAdapter();

            //Assert

            Assert.NotNull(result);
        }

        [Fact]
        public void NonQuery()
        {
            //Arrange
            DataAccess da = new DataAccess(_connectionString)
            {
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandText = "InsertPerson"
            };

            SqlParameter firstName = new SqlParameter
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.NVarChar,
                Value = "Barbara"
            };
            da.Params.Add(firstName);

            SqlParameter surname = new SqlParameter
            {
                ParameterName = "@Surname",
                SqlDbType = SqlDbType.NVarChar,
                Value = "Engle"
            };
            da.Params.Add(surname);

            SqlParameter dateOfBirth = new SqlParameter
            {
                ParameterName = "@DateOfBirth",
                SqlDbType = SqlDbType.Date,
                Value = Convert.ToDateTime("1966/08/20")
            };
            da.Params.Add(dateOfBirth);

            SqlParameter personTypeId = new SqlParameter
            {
                ParameterName = "@PersonTypeId",
                SqlDbType = SqlDbType.Int,
                Value = 2
            };
            da.Params.Add(personTypeId);


            //Action

            int actual = da.NonQuery();

            //Assert

            Assert.IsType<int>(actual);
        }

    }
}
