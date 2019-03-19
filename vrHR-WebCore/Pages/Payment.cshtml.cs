using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace vrHR_WebCore
{
    public class PaymentModel : PageModel
    {
        SqlConnection sql = new SqlConnection(Classes.Session.connection);
        [BindProperty]
        public Classes.PaymentModel pay { get; set; }
        [BindProperty]
        public string ErrorMessage { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            SqlCommand cmd = new SqlCommand("Select AccountNumber from Cards where CardNumber=@CardNumber and CardType=@CardType and " +
                "CVV=@CVV and Expiry=@Expiry", sql);
            cmd.Parameters.AddWithValue("@CardNumber", pay.CardNumber);
            cmd.Parameters.AddWithValue("@CardType", pay.CardType);
            cmd.Parameters.AddWithValue("@CVV", pay.CVV);
            cmd.Parameters.AddWithValue("@Expiry", pay.Expiry);
            sql.Open();
            var exists = await cmd.ExecuteScalarAsync();
            sql.Close();
            int c = Convert.ToInt32(exists);
            if (c!=0)
            {
                cmd = new SqlCommand("update Bank set Balance=Balance-@Amount where AccountNumber=@acc", sql);
                cmd.Parameters.AddWithValue("@Amount", pay.Amount);
                cmd.Parameters.AddWithValue("@acc", c);
                sql.Open();
                int p = await cmd.ExecuteNonQueryAsync();
                if (p==1) { return RedirectToPage("/Login");  }
                else { ErrorMessage = "Payment Failed!"; }
                sql.Close();
            }
            else
            {
                ErrorMessage = "Invalid Card credentials!";
            }

            return Page();
        }

        public void OnGet()
        {
            if (!Classes.Session.Paying) Response.Redirect("/");
        }
    }

    public enum cardTypes
    {
        VISA,
        MasterCard,
        RuPay
    }
}