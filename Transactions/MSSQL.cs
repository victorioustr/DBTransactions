using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
namespace Transactions
{
    public class MSSQL
    {
        public class Command : IDisposable
        {
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            public virtual void Dispose(bool _IDispose)
            {
                if (_IDispose)
                {
                    ExecuteScalar_Bool = false;
                    ExecuteScalar_Double = 0;
                    ExecuteScalar_Float = 0;
                    ExecuteScalar_Int = 0;
                    ExecuteScalar_Long = 0;
                }
            }
            private int ExecuteScalar_Int = 0;
            private double ExecuteScalar_Double = 0;
            private float ExecuteScalar_Float = 0;
            private long ExecuteScalar_Long = 0;
            private bool ExecuteScalar_Bool;
            /// <summary>
            /// Bu fonksiyon ile kısa yoldan SqlCommand nesnesini kullanabilirsin.
            /// </summary>
            /// <param name="Command">SQLCommand fonksiyonu için bir sql komutu gir örn:"select * from table"</param>
            /// <param name="Connection">SQLConnection parametresine oluşturduğun bağlantıyı gir.</param>
            /// <returns>Geriye dönen veri SQLCommand nesnesi türünde olacak.Örneğin veri okuma işlemi için şu şekilde kullanabilirsin : var Command = SQLCommand("komut",baglanti).ExecuteReader();</returns>
            /// <seealso cref="MSSQL.Command.SQLCommand(string, SqlConnection)"/>
            public SqlCommand SQLCommand(string Command, SqlConnection Connection)
            {
                using (SqlCommand Com = new SqlCommand())
                {
                    Com.Connection = Connection;
                    Com.CommandText = Command;
                    return Com;
                }
            }
            /// <summary>
            /// Bu fonksiyon ile SqlCommand nesnesinin ExecuteScalar() fonksiyonunu kısa yoldan kullanabilirsin.
            /// </summary>
            /// <param name="Command">SQLCommand fonksiyonu için bir sql komutu gir örn:"select UserName from table where UserID = 1"</param>
            /// <param name="Connection">SQLConnection parametresine oluşturduğun bağlantıyı gir.</param>
            /// <param name="ReturnType">Geri dönüş tipini gir, desteklenenler : string,int,double,float,long,bool. Örn: "int" veya "string"</param>
            /// <returns>Bu fonksiyon sql komutu ile istenilen TEK bir veriyi geri döndürür, örnek kullanımı : var Data = SQLCommand("select UserName from table where UserID = 1",Connection,"string"); data objesinin içerisinde komuttan dönen veri olacak. </returns>
            /// <seealso cref="MSSQL.Command.SQLCommand(string, SqlConnection, string)"/>
            public object SQLCommand(string Command, SqlConnection Connection, string ReturnType)
            {
                using (SqlCommand Com = new SqlCommand())
                {
                    Com.Connection = Connection;
                    Com.CommandText = Command;
                    if (ReturnType != string.Empty)
                    {
                        switch (ReturnType)
                        {
                            case "string":
                                return Com.ExecuteScalar().ToString();
                            case "int":
                                if (int.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Int))
                                    return ExecuteScalar_Int;
                                else
                                    return null;
                            case "double":
                                if (double.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Double))
                                    return ExecuteScalar_Double;
                                else
                                    return null;
                            case "float":
                                if (float.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Float))
                                    return ExecuteScalar_Float;
                                else
                                    return null;
                            case "long":
                                if (long.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Long))
                                    return ExecuteScalar_Long;
                                else
                                    return null;
                            case "bool":
                                if (bool.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Bool))
                                    return ExecuteScalar_Bool;
                                else
                                    return null;
                            default:
                                return "Return type algılanmadı !";
                        }
                    }
                    else
                    {
                        return "Algılanmadı";
                    }
                }
            }
        }
        public class DataReader : IDisposable
        {
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
            /// <summary>
            /// Bağlantısı,komutu tanımlanmış SqlCommand nesnesini parametre olarak ekle.Fonksiyon Sql komutundan dönen verileri sana yansıtacak.
            /// </summary>
            /// <param name="Command">SqlCommand nesnesi</param>
            /// <returns>Geriye belirsiz tipte verilerin değerlerini döndürür.</returns>
            /// <seealso cref="MSSQL.DataReader.Reader(SqlCommand)"/>
            public IEnumerable<object> Reader(SqlCommand Command)
            {
                using(SqlDataReader Read = Command.ExecuteReader())
                {
                    while(Read.Read())
                    {
                        yield return Read;
                    }
                }
            }
        }
    }
}
