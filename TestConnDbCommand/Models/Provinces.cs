using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TestConnDbCommand.Models
{
    public class Provinces : MainModel
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string NameInThai { get; set; }
        public string NameInEnglish { get; set; }

        public DataTable GetDt(int rowStart, int rowLimit, string orderSp) { 
            SqlConnection conn = new SqlConnection(connStr);

            DataTable dt = new DataTable();
            string cmdText = "SELECT * FROM Provinces";
            SqlCommand cmdObj = new SqlCommand(cmdText, conn);
            SqlDataAdapter ad = new SqlDataAdapter(cmdObj);
            ad.Fill(rowStart, rowLimit, dt);

            return dt;
        }

        public DataRow GetRow(int prvId)
        {
            SqlConnection conn = new SqlConnection(connStr);

            DataRow dr = null;
            DataTable dt = new DataTable();
            string cmdText = "SELECT TOP 1 * FROM Provinces prv WHERE prv.Id = @pr_prv_id";
            SqlCommand cmdObj = new SqlCommand(cmdText, conn);
            cmdObj.Parameters.AddWithValue("@pr_prv_id", prvId);
            SqlDataAdapter ad = new SqlDataAdapter(cmdObj);
            ad.Fill(0, 1, dt);

            if(dt.Rows.Count > 0) {
                dr = dt.Rows[0];
            }

            return dr;
        }

        public Provinces AddRow(int inpCode, string nameTh, string nameEn)
        {
            Provinces modelAdd = new Provinces{ 
                Code = inpCode
                ,NameInThai = nameTh
                ,NameInEnglish = nameEn
            };

            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter();
            
            string cmdText = @"INSERT INTO Provinces(
                                    Code
                                   ,NameInThai
                                   ,NameInEnglish
                            ) VALUES (
                                    @pr_code
                                   , @pr_name_th
                                   , @pr_name_en
                            )";
            SqlCommand cmdObj = new SqlCommand(cmdText, conn);
            cmdObj.Parameters.AddWithValue("@pr_code", inpCode);
            cmdObj.Parameters.AddWithValue("@pr_name_th", nameTh);
            cmdObj.Parameters.AddWithValue("@pr_name_en", nameEn);

            da.InsertCommand = cmdObj;
            conn.Open();
            da.InsertCommand.ExecuteNonQuery();
            conn.Close();

            return modelAdd;
        }

        public Provinces EditRow(int inpId, int inpCode, string nameTh, string nameEn)
        {
            Provinces modelEdit = new Provinces
            {
                Code = inpCode
                ,
                NameInThai = nameTh
                ,
                NameInEnglish = nameEn
            };

            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter();

            string cmdText = @"UPDATE Provinces
                                    SET NameInThai = @pr_name_th
                                WHERE Id = @pr_id";
            SqlCommand cmdObj = new SqlCommand(cmdText, conn);
            cmdObj.Parameters.AddWithValue("@pr_id", inpId);
            cmdObj.Parameters.AddWithValue("@pr_name_th", nameTh+":"+inpId);

            da.UpdateCommand = cmdObj;
            conn.Open();
            da.UpdateCommand.ExecuteNonQuery();
            conn.Close();

            return modelEdit;
        }

        public Provinces DelRow(int inpId)
        {
            Provinces modelEdit = new Provinces
            {
                Id = inpId
            };

            SqlConnection conn = new SqlConnection(connStr);
            SqlDataAdapter da = new SqlDataAdapter();

            string cmdText = @"DELETE FROM Provinces WHERE Id = @pr_id";
            SqlCommand cmdObj = new SqlCommand(cmdText, conn);
            cmdObj.Parameters.AddWithValue("@pr_id", inpId);

            da.DeleteCommand = cmdObj;
            conn.Open();
            da.DeleteCommand.ExecuteNonQuery();
            conn.Close();

            return modelEdit;
        }
    }

}