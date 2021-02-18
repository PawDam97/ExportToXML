using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int rowCount = 0;
            int wybor1, wybor2, wybor3;
            string sql = "";
            string clr = "";
            List<string> Lista = new List<string>();
            OracleConnection con;
            con = new OracleConnection();
            con.ConnectionString = "User Id=login;Password=password;Data Source = (DESCRIPTION = " +
   " (ADDRESS = (PROTOCOL = TCP)(HOST = hostip)(PORT = 1522    ))" +
   " (CONNECT_DATA =" +
     " (SERVER = DEDICATED)" +
     " (SERVICE_NAME = serviceName)" +
    ")" +
  ");";

            con.Open();

        Start:
            Console.Clear();
            try
            {
                Console.WriteLine("///////////////////////////////////////");
                Console.WriteLine("///  Connected to Oracle " + con.ServerVersion + " ///");
                Console.WriteLine("///////////////////////////////////////");
                Console.WriteLine("///        Co chcesz zrobić?        ///");
                Console.WriteLine("///////////////////////////////////////");
                Console.WriteLine("/// 1.Eksport z bazy do XML         ///");
                Console.WriteLine("/// 2.Import z XML do bazy          ///");
                Console.WriteLine("/// 3.Wyjdz z programu              ///");
                Console.WriteLine("///////////////////////////////////////");
                wybor1 = int.Parse(Console.ReadLine());
                Console.Clear();
            }
            catch (FormatException exception)
            {
                Console.Clear();
                Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                Console.WriteLine("///                        Musisz podać liczbę                         ///");
                Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }
            if (wybor1 > 3)
            {
                Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }
            if (wybor1 < 0)
            {
                Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                Console.ReadKey();
                Console.Clear();
                goto Start;
            }
            if (wybor1 == 3)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            if (wybor1 == 1)
            {

                 string[] strSql = { "SELECT  * FROM ZAWODNICY", "SELECT * FROM GRUPY_TRENINGOWE", "SELECT * FROM KONTRAKTY", "SELECT * FROM KONTUZJE", "SELECT * FROM OBIEKTY_SPORTOWE", "SELECT * FROM ROZGRYWKI", "SELECT * FROM SCOUTING", "SELECT * FROM SPECJALIZACJE", "SELECT * FROM SZTAB_SZKOLENIOWY", "SELECT * FROM TERMINARZ", "SELECT * FROM TRANSFERY", "SELECT * FROM ZESPOL" };
                string[] tables = { "ZAWODNICY", "GRUPY_TRENINGOWE", "KONTRAKTY", "KONTUZJE", "OBIEKTY_SPORTOWE", "ROZGRYWKI", "SCOUTING", "SPECJALIZACJE", "SZTAB_SZKOLENIOWY", "TERMINARZ", "TRANSFERY", "ZESPOL" };
                DataTable ds = new DataTable();
                DataSet dS = new DataSet(); 
                dS.DataSetName = "FCBARCELONA";
                dS.Tables.Add(ds);
                for (int i = 0; i < 12; i++)
                {
                    using (OracleCommand sqlComm = new OracleCommand(strSql[i], con) { CommandType = CommandType.Text })
                    {
                        OracleDataAdapter da = new OracleDataAdapter(sqlComm);
                        ds.TableName = "DATA";
                        da.Fill(ds);
                        dS.Tables[0].WriteXml("EXPORTBAZY.xml");
                    }
                }
                for (int i = 0; i < 12; i++)
                {
                    using (OracleCommand sqlComm = new OracleCommand(strSql[i], con) { CommandType = CommandType.Text })
                    {
                        OracleDataAdapter da = new OracleDataAdapter(sqlComm);
                        ds.Clear();
                        ds.TableName = tables[i];
                        da.Fill(ds);
                        try
                        {
                            dS.Tables[0].WriteXml("Tabele/" + tables[i] + ".xml");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Pliki sa aktualnie w uzyciu (import).");
                            Console.WriteLine("Kliknij aby kontynuować.");
                            Console.ReadKey();
                            goto Start;
                        }
                    }
                }
                Console.WriteLine("Dane zostały wyeksportowane pomyślnie.");
                Console.WriteLine("Kliknij aby kontynuować.");
                Console.ReadKey();
                goto Start;
            }
            if (wybor1 == 2)
            {
                try
                {
                    Console.WriteLine("///////////////////////////////////////////////");
                    Console.WriteLine("///Do ktorej tabeli chcesz zamiportować dane///");
                    Console.WriteLine("///////////////////////////////////////////////");
                    Console.WriteLine("/// 1.Zawodnicy                             ///");
                    Console.WriteLine("/// 2.Grupy treningowe                      ///");
                    Console.WriteLine("/// 3.Kontrakty                             ///");
                    Console.WriteLine("/// 4.Kontuzje                              ///");
                    Console.WriteLine("/// 5.Obiekty sportowe                      ///");
                    Console.WriteLine("/// 6.Rozgrywki                             ///");
                    Console.WriteLine("/// 7.Scouting                              ///");
                    Console.WriteLine("/// 8.Specjalizacje                         ///");
                    Console.WriteLine("/// 9.Sztab Szkoleniowy                     ///");
                    Console.WriteLine("/// 10.Terminarz                            ///");
                    Console.WriteLine("/// 11.Transfery                            ///");
                    Console.WriteLine("/// 12.Zespół                               ///");
                    Console.WriteLine("/// 0.Powrot do menu                        ///");
                    Console.WriteLine("///////////////////////////////////////////////");
                    wybor2 = int.Parse(Console.ReadLine());
                    Console.Clear();
                }
                catch (FormatException exception)
                {
                    Console.Clear();
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("///                        Musisz podać liczbę                         ///");
                    Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }
                if(wybor2 == 0)
                {
                    goto Start;
                }
                if (wybor2 > 12)
                {
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                    Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }
                if (wybor1 < 0)
                {
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                    Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                    Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }
                if (wybor2== 1)
                {
                    
                    XmlTextReader reader = new XmlTextReader("Tabele/ZAWODNICY.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from ZAWODNICY WHERE NR_KART_ZAW = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from ZAWODNICY";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 7)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] + "/" + Lista[i + 2] + "/" + Lista[i + 3] + "/" + Lista[i + 4] + "/" + Lista[i + 5].Remove(10, 15) + "/" + Lista[i + 6]);
                        sql = "Insert into ZAWODNICY (NR_KART_ZAW,IMIE,NAZWISKO,NARODOWOSC,POZYCJA,DATA_URODZENIA,ZESPOL) values (" + Lista[i] + ",'" + Lista[i+1] + "','" + Lista[i+2] + "','" + Lista[i+3] + "','" + Lista[i+4] + "',to_date('" + Lista[i + 5].Remove(10, 15) + "','RR/MM/DD'),'" + Lista[i+6] + "')";
                         OracleCommand cmd = con.CreateCommand();
                         cmd.CommandText = sql;
                          rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 2)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/GRUPY_TRENINGOWE.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from GRUPY_TRENINGOWE WHERE NR_KART_ZAW = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from GRUPY_TRENINGOWE";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 5)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i+1] + "/" + Lista[i+2] + "/" + Lista[i+3] + "/" + Lista[i + 4].Remove(10, 15) );
                        sql = "Insert into GRUPY_TRENINGOWE (NR_KART_ZAW,NR_GRUPY,NR_KART_TREN,ID_OBIEKTU,TERMIN) values (" + Lista[i] + "," + Lista[i+1] + "," + Lista[i+2] + "," + Lista[i+3] + ",to_date('" + Lista[i+4].Remove(10, 15) + "','RR/MM/DD')) ";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 3)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/KONTRAKTY.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from KONTRAKTY WHERE ID = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from KONTRAKTY";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 5)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] + "/" + Lista[i + 2] + "/" + Lista[i + 3] + "/" + Lista[i + 4].Remove(10, 15));
                        sql = "Insert into KONTRAKTY (ID,NR_KART,PENSJA,WARTOSC,KONIEC_KONTRAKTU) values (" + Lista[i] + "," + Lista[i+1] + "," + Lista[i+2] + "," + Lista[i+3] + ",to_date('" + Lista[i+4].Remove(10,15) + "','RR/MM/DD'))";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 4)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/KONTUZJE.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }catch(Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from KONTUZJE WHERE NR_KART_ZAW = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from KONTUZJE";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 6)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] + "/" + Lista[i + 2] + "/" + Lista[i + 3] + "/" + Lista[i + 4].Remove(10, 15)+ "/" + Lista[i+5]);
                        sql = "Insert into KONTUZJE (NR_KART_ZAW,NR_KART_TREN,ID_KONTUZJI,RODZAJ_KONTUZJI,KONIEC_KONTUZJI,KOSZT) values ( " + Lista[i] + "," + Lista[i+1] + "," + Lista[i+2] + ",'" + Lista[i+3] + " boli',to_date('" + Lista[i+4].Remove(10,15) + "','RR/MM/DD')," + Lista[i+5] + ")";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 5)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/OBIEKTY_SPORTOWE.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from OBIEKTY_SPORTOWE WHERE ID_OBIEKTU = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from OBIEKTY_SPORTOWE";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 4)
                    {
                        Console.WriteLine(Lista[i] +"/"+ Lista[i + 1] + "/" + Lista[i + 2] + "/" + Lista[i + 3]);
                        sql = "Insert into OBIEKTY_SPORTOWE (ID_OBIEKTU,NAZWA,GRUPY_TRENUJACE,ADRES) values (" + Lista[i] + ",'" + Lista[i+1] + "'," + Lista[i+2] + ",'" + Lista[i+3] + "')";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 6)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/ROZGRYWKI.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from ROZGRYWKI WHERE ID_ROZGRYWEK = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from ROZGRYWKI";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 2)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] );
                        sql = "Insert into ROZGRYWKI (ID_ROZGRYWEK,ROZGRYWKI) values (" + Lista[i] + ",'" + Lista[i+1] + "')";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 7)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/SCOUTING.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from SCOUTING WHERE NR_KART_TREN = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from SCOUTING";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 6)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] + "/" + Lista[i + 2]+"/"+ Lista[i + 3]+"/"+ Lista[i + 4].Remove(10, 15) + "/" + Lista[i + 5].Remove(10, 15));
                        sql = "Insert into SCOUTING (NR_KART_TREN,KOSZT,ID_PODROZY,GDZIE,DATA_WYJAZDU,DATA_POWROTU) values (" + Lista[i] + "," + Lista[i+1] + ",'" + Lista[i+2] + "','" + Lista[i + 3] + "',to_date('" + Lista[i+4].Remove(10,15) + "','RR/MM/DD'),to_date('" + Lista[i + 5].Remove(10, 15) + "','RR/MM/DD'))";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 8)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/SPECJALIZACJE.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from SPECJALIZACJE WHERE ID_SPEC = " + Lista[0];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from SPECJALIZACJE";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }
                    for (int i = 0; i < Lista.Count; i = i + 2)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1]);
                        sql = "Insert into SPECJALIZACJE (ID_SPEC,SPECJALIZACJA) values (" + Lista[i] + ",'" + Lista[i+1] + "')";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 9)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/SZTAB_SZKOLENIOWY.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from SZTAB_SZKOLENIOWY WHERE NR_KART_TREN = " + Lista[3];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from SZTAB_SZKOLENIOWY";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 5)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] + "/" + Lista[i + 2].Remove(10, 15)+ "/" + Lista[i + 3] + "/" + Lista[i + 4]);
                        sql = "Insert into SZTAB_SZKOLENIOWY(IMIE,NAZWISKO,DATA_URODZENIA,NR_KART_TREN, SPECJALIZACJA) values ('" + Lista[i] + "','" + Lista[i+1] + "',to_date('" + Lista[i + 2].Remove(10, 15) + "','RR/MM/DD')," + Lista[i + 3] + "," + Lista[i + 4] + ")";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 10)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/TERMINARZ.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from TERMINARZ WHERE NR_SPOTKANIA = " + Lista[1];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from TERMINARZ";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 6)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1] + "/" + Lista[i + 2].Remove(10, 15) + "/" + Lista[i + 3] + "/" + Lista[i + 4]+"/"+ Lista[i + 5]);
                        sql = "Insert into TERMINARZ (ZESPOL,NR_SPOTKANIA,DATA_SPOTKANIA,PRZECIWNIK,MIEJSCE,TYP_ROZGRYWEK) values (" + Lista[i] + "," + Lista[i+1] + ",to_date('" + Lista[i+2].Remove(10,15) + "','RR/MM/DD'),'" + Lista[i+3] + "','" + Lista[i + 4] + "'," + Lista[i + 5] + ")";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 11)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/TRANSFERY.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from TRANSFERY WHERE ID_TRANSFERU = " + Lista[2];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from TRANSFERY";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 7)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1].Remove(10, 15) + "/" + Lista[i + 2] + "/" + Lista[i + 3] + "/" + Lista[i + 4] + "/" + Lista[i + 5] + "/" + Lista[i + 6]);
                        sql = "Insert into TRANSFERY (NR_KART_ZAW,TERMIN,ID_TRANSFERU,SKAD,DOKAD,RODZAJ_TRANSFERU,KWOTA) values (" + Lista[i] + ",to_date('" + Lista[i + 1].Remove(10, 15) + "','RR/MM/DD'),'" + Lista[i + 2] + "','" + Lista[i + 3] + "','" + Lista[i + 4] + "','" + Lista[i + 5] + "'," + Lista[i + 6] + ")";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                if (wybor2 == 12)
                {
                    XmlTextReader reader = new XmlTextReader("Tabele/ZESPOL.xml");
                    Lista.Clear();
                    try
                    {
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Text:
                                    Lista.Add(reader.Value);
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Nie znaleziono pliku xml");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }

                    OracleCommand cmdd = con.CreateCommand();
                    try
                    {
                        cmdd.CommandText = "select Count(*) from ZESPOL WHERE ID_ZESPOLU = " + Lista[1];
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Plik XML jest pusty (zaimportowana tabela byla pusta)");
                        Console.WriteLine("Kliknij przycisk aby kontynuowac");
                        Console.ReadKey();
                        goto Start;
                    }
                    int id = Convert.ToInt32(cmdd.ExecuteScalar());
                    if (id > 0)
                    {
                        try
                        {
                            Console.WriteLine("W bazie juz istnieja dane ktore chcesz zaimportowac");
                            Console.WriteLine("Czy chcesz je usunąć?");
                            Console.WriteLine("1. Usun i zaimportuj");
                            Console.WriteLine("2. Zostaw i powróć do menu");
                            wybor3 = int.Parse(Console.ReadLine());
                        }
                        catch (FormatException exception)
                        {
                            Console.Clear();
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                        Musisz podać liczbę                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        if (wybor3 == 2)
                        {
                            goto Start;
                        }
                        if (wybor3 > 2)
                        {
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.WriteLine("///                       Nieprawidlowy wybor.                         ///");
                            Console.WriteLine("///                      Kliknij aby kontynuowac.                      ///");
                            Console.WriteLine("//////////////////////////////////////////////////////////////////////////");
                            Console.ReadKey();
                            Console.Clear();
                            goto Start;
                        }
                        Console.Clear();
                        clr = "delete from ZESPOL";
                        OracleCommand clear = con.CreateCommand();
                        clear.CommandText = clr;
                        rowCount = clear.ExecuteNonQuery();
                    }

                    for (int i = 0; i < Lista.Count; i = i + 2)
                    {
                        Console.WriteLine(Lista[i] + "/" + Lista[i + 1]);
                        sql = "Insert into ZESPOL (NAZWA,ID_ZESPOLU) values ('" + Lista[i] + "'," + Lista[i+1] + ")";
                        OracleCommand cmd = con.CreateCommand();
                        cmd.CommandText = sql;
                        rowCount = cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Pomyślnie zaimportowano dane.");
                Console.ReadLine();
                goto Start;
            }

        }
    }
}
