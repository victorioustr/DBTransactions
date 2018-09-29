# DBTransactions
Database object transactions
Use : 
add references dll and namespace : using Transactions;
public SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=cafeOtomasyon;Integrated Security=True;MultipleActiveResultSets=True");
        private void button1_Click(object sender, EventArgs e)
        {
            using(MSSQL.Command Com = new MSSQL.Command())
            {
                if(baglanti.State == System.Data.ConnectionState.Open) { baglanti.Close(); }
                baglanti.Open();
                var urunAdi = Com.SQLCommand("select urunAdi from urunler where id = 2", baglanti, "string");
                MessageBox.Show(urunAdi.ToString());
                var urunler = Com.SQLCommand("select urunAdi from urunler", baglanti);
                var urun = Com.SQLCommand("select * from urunler", baglanti).ExecuteReader();
                while (urun.Read())
                {
                    MessageBox.Show(urun["urunAdi"].ToString());
                }
                using(MSSQL.DataReader Reader = new MSSQL.DataReader())
                {
                    urunler.Connection = baglanti;
                    foreach(IDataRecord item in Reader.Reader(urunler))
                    {
                        listBox1.Items.Add(item.GetValue(0)); // or item.GetString(0)
                    }
                }
            }
        }
Dll file is being developed.
