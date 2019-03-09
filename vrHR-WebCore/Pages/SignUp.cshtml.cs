using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using vrHR_WebCore.Classes;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace vrHR_WebCore.Pages
{
    public class SignUpModel : PageModel
    {
        SqlConnection sql = new SqlConnection(Classes.Session.connection);
        [BindProperty]
        public string ErrorCustom { get; set; }
        [BindProperty]
        public HR hr { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Password Required!")]
        public string Password { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlCommand cmm = new SqlCommand("Select count(*) from HR where Username=@Username", sql);
            cmm.Parameters.AddWithValue("@Username", hr.UserName);
            int c = Convert.ToInt32(cmm.ExecuteScalarAsync());
            if (c!=0)
            {
                ErrorCustom = "Username already exists!";
            }
            if (Password == ConfirmPassword)
            {
                SqlCommand cmd = new SqlCommand("Insert into HR values(@UserName, @Password, @FirstName, @MiddleName, @LastName, @Address, @Phone)", sql);
                cmd.Parameters.AddWithValue("@UserName", hr.UserName);
                cmd.Parameters.AddWithValue("@Password", enc(Password));
                cmd.Parameters.AddWithValue("@FirstName", hr.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", hr.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", hr.LastName);
                cmd.Parameters.AddWithValue("@Address", hr.Address);
                cmd.Parameters.AddWithValue("@Phone", hr.PhoneNumber);
                int x = await cmd.ExecuteNonQueryAsync();
                if (x == 1)
                {
                    Classes.Session.Amount = 800;
                    return RedirectToPage("/Payment");
                }
                else
                {
                    return Page();
                }
            }
            return Page();
        }

        protected  int generateID()
        {
            SqlCommand cmd = new SqlCommand("select Count(*) from HR", sql);
            sql.Open();
            int r = Convert.ToInt32(cmd.ExecuteScalar());
            sql.Close();
            return ++r;
        }

        protected string enc(string Password)
        {
            Encryptor encr = new Encryptor();
            return encr.Encrypt(Password, "9tP#S*P/KK8_c68~");
        }
    }
}