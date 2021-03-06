using Dapper;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using WindowsFormsFRep.Model;

namespace WindowsFormsFRep.Service
{
    public class DataBaseAccessService
    {
        public const string UserId = "z31451";
        public const string Password = "SEV";
        public const string Provider = "MSDAORA";
        public const string DataSource = "TEST9";

        private OleDbConnectionStringBuilder oracleConnectionStringBuilder;

        public DataBaseAccessService()
        {
            this.OracleConnectionStringBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = Provider,
                DataSource = DataSource,
            };
            OracleConnectionStringBuilder["User ID"] = UserId;
            OracleConnectionStringBuilder["Password"] = Password;
        }

        public OleDbConnectionStringBuilder OracleConnectionStringBuilder { get => oracleConnectionStringBuilder; set => oracleConnectionStringBuilder = value; }

        public List<Fields> SelectFieldsFromDateBase()
        {
            //(string dateFrom, string dateTo)
            //string command = " SELECT DISTINCT(C.CALC#) CALC, " +
            //                 "   C.CALCNAME CALCNAME, " +
            //                 "   COUNT(S.CALC#)  COUNT " +
            //                 "   FROM SUPLPROBA S, CALC C, PROD P " +
            //                 "   WHERE S.DOCDATE  between to_date(" + " ' " + dateFrom + " ' " + ", 'dd/mm/yyyy') and " +
            //                 "                            to_date(" + " ' " + dateTo + " ' " + ", 'dd/mm/yyyy') " +
            //                 "   AND C.CALC#=S.CALC# AND S.PROD#=P.PROD# AND P.PRODNAME NOT LIKE ('%БРИК%') " +
            //                 "   GROUP BY C.CALC#,C.CALCNAME,S.CALC# " +
            //                 "   UNION SELECT DISTINCT(C.CALC#) CALC, " +
            //                 "  'Брикет' CALCNAME, " +
            //                 "   COUNT(S.CALC#)  COUNT " +
            //                 "   FROM SUPLPROBA S, CALC C, prod p " +
            //                  "  WHERE S.DOCDATE  between to_date(" + " ' " + dateFrom + " ' " + ", 'dd/mm/yyyy') and " +
            //                  "                           to_date(" + " ' " + dateTo + " ' " + ", 'dd/mm/yyyy') " +
            //                  "  AND C.CALC#=S.CALC# " +
            //                  "  and s.prod#=p.prod# " +
            //                  "  and s.calc#=600 and p.prodname like ('%БРИК%') " +
            //                  "  GROUP BY C.CALC#,C.CALCNAME,S.CALC# ";

            //string command = " SELECT CALC# CALC, CALCNAME CALCNAME, unit# COUNT FROM CALC"; WAREDEFECT

            string command = " SELECT DOCNO CALC, DBUSER CALCNAME, DEPT# COUNT FROM WAREDEFECT";

            List<Fields> result;

            using (OleDbConnection db = new OleDbConnection(oracleConnectionStringBuilder.ConnectionString))
            {
                result = db.Query<Fields>(command).ToList();
            }
            return result;
        }

        public int DeleteFromDatabase(string calc)
        {
            using (OleDbConnection db = new OleDbConnection(oracleConnectionStringBuilder.ConnectionString))
            {
                string sqlcommand  = " DELETE FROM WAREDEFECT WHERE docno = " + calc;                
                try 
                {                  
                    int row = db.Execute(sqlcommand); 
                    return row;
                }
                catch(Exception ex)
                {
                    return 0;
                }
            }
        }
     
            
    }
}
