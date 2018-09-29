using System;
using System.Data.SqlClient;
namespace Transactions
{
    public partial class MSSQL
    {
        public class Command : IDisposable
        {
            #region ctor

            public Command()
            {

            }

            #endregion

            #region fields

            private int ExecuteScalar_Int = 0;
            private double ExecuteScalar_Double = 0;
            private float ExecuteScalar_Float = 0;
            private long ExecuteScalar_Long = 0;
            private bool ExecuteScalar_Bool;

            #endregion

            #region methods

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
            public object SQLCommand(string Command, SqlConnection Connection, Enums.TypeReturnType ReturnType)
            {
                using (SqlCommand Com = new SqlCommand())
                {
                    Com.Connection = Connection;
                    Com.CommandText = Command;
                    switch (ReturnType)
                    {
                        case Enums.TypeReturnType.String:
                            return Com.ExecuteScalar().ToString();
                        case Enums.TypeReturnType.Integer:
                            return int.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Int) ? ExecuteScalar_Int : (int?)null;
                        case Enums.TypeReturnType.Double:
                            return double.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Double) ? ExecuteScalar_Double : (double?)null;
                        case Enums.TypeReturnType.Float:
                            return float.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Float) ? ExecuteScalar_Float : (float?)null;
                        case Enums.TypeReturnType.Long:
                            return long.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Long) ? ExecuteScalar_Long : (long?)null;
                        case Enums.TypeReturnType.Boolean:
                            return bool.TryParse(Com.ExecuteScalar().ToString(), out ExecuteScalar_Bool) ? ExecuteScalar_Bool : (bool?)null;
                        default:
                            return "Return type algılanmadı !";
                    }
                }
            }

            public (bool status, TType result) SQLCommand<TType>(string Command, SqlConnection Connection)
            {
                try
                {
                    using (SqlCommand Com = new SqlCommand(Command, Connection))
                    {
                        return (true, (TType)Convert.ChangeType(Com.ExecuteScalar(), typeof(TType)));
                    }
                }
                catch
                {
                    return (false, default(TType));
                }
            }

            #endregion

            #region dispose

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

            #endregion

        }
    }
}
