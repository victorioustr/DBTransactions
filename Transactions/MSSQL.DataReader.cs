using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Transactions
{
    public partial class MSSQL
    {
        public class DataReader : IDisposable
        {
            #region ctor

            public DataReader()
            {

            }

            #endregion

            #region methods

            /// <summary>
            /// Bağlantısı,komutu tanımlanmış SqlCommand nesnesini parametre olarak ekle.Fonksiyon Sql komutundan dönen verileri sana yansıtacak.
            /// </summary>
            /// <param name="Command">SqlCommand nesnesi</param>
            /// <returns>Geriye belirsiz tipte verilerin değerlerini döndürür.</returns>
            /// <seealso cref="MSSQL.DataReader.Reader(SqlCommand)"/>
            public IEnumerable<object> Reader(SqlCommand Command)
            {
                using (SqlDataReader Read = Command.ExecuteReader())
                {
                    while (Read.Read())
                    {
                        yield return Read;
                    }
                }
            }

            #endregion

            #region dispose

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            public virtual void Dispose(bool _Dispose)
            {
                if (_Dispose)
                {

                }
            }

            #endregion
        }
    }
}
