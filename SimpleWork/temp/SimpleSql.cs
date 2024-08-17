using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NewPetProjectC_
{
    class SimpleSql
    {
        
        private readonly SqlConnection _sqlConnection = null;
        private SqlCommand _sqlCommand = null;
        private SqlDataReader _sqlDataReader = null;

        public SqlDataReader DataReader { get { return _sqlDataReader; } }

        public SimpleSql(string dataBaseName, string tableName) 
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[dataBaseName].ConnectionString);
        }

        /// <summary>
        /// Return string that can be use to place into DB
        /// </summary>
        /// <param name="uncorrectString"></param>
        /// <returns></returns>
        static public string GetRightSqlString(string uncorrectString)
        {
            string newDescription = string.Empty;
            string[] splittedDescription = uncorrectString.Split('\n');
            for (int i = 0; i < splittedDescription.Length; i++)
            {
                char[] stringChars;

                if (splittedDescription[i] != string.Empty) stringChars = splittedDescription[i].ToCharArray();
                else break;

                for (int j = 0; j < stringChars.Length; j++)
                {
                    if (stringChars[j] == '\'' || stringChars[j] == '\"') stringChars[j] = ' ';
                }
                newDescription += new string(stringChars);
            }
            return newDescription;
        }


        /// <summary>
        /// Open sqlConnection and check _sqlConnection.State and ConnectionState.Open
        /// </summary>
        public void Join() 
        {
            _sqlConnection.Open();
            if (_sqlConnection.State != ConnectionState.Open) SimpleMessageBoxes.SentFatalErrorMessageBox("Подключение не установлено");
        
        } 
        public void CloseConnection() => _sqlConnection.Close();

        public int ExecuteCommand() => _sqlCommand.ExecuteNonQuery();
        public void ExecuteRead() => _sqlDataReader = _sqlCommand.ExecuteReader();
        public void DispoceCommand() => _sqlCommand.Dispose();

        public void SentSqlCommand(string command) => _sqlCommand = new SqlCommand(command, _sqlConnection);


    }
}
