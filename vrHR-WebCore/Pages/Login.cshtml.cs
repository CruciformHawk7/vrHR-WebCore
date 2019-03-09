using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vrHR_WebCore.Classes;

namespace vrHR_WebCore.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Classes.LoginModel Login { get; set; }
        SqlConnection sql = new SqlConnection(Session.connection);
        public async Task<IActionResult> OnPostAsync()
        {
            SqlCommand cmd = new SqlCommand("Select Count(*) from HR where Username=@username and Passsword=@Password");
            cmd.Parameters.AddWithValue("@username", Login.UserName);
            cmd.Parameters.AddWithValue("@Password", enc(Login.Password));
            sql.Open();
            var x = await cmd.ExecuteScalarAsync();
            int p = Convert.ToInt32(x);
            int t = 2;
            string conn = "";
            if (p==1)
            {
                if (Session.SessionCode == null)
                {
                    while (t != 0)
                    {
                        conn = GenerateID(14);
                        cmd = new SqlCommand("Select count(*) from connections where ConnID=@conn");
                        var s = await cmd.ExecuteScalarAsync();
                        t = Convert.ToInt32(s);
                    }
                    Session.LoggedInUserName = Login.UserName;
                    Session.isLoggedIn = true;
                    Session.SessionCode = conn;
                }
                else
                {
                    Response.Redirect("/");
                }
            }
            return Page();
        }

        protected string enc(string Password)
        {
            Encryptor encr = new Encryptor();
            return encr.Encrypt(Password, "9tP#S*P/KK8_c68~");
        }

        private static Random random = new Random();
        public static string GenerateID(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}