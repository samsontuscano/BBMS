using BBMS.Data;
using BBMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Data.SqlClient;

namespace BBMS.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.BloodGroupList = new List<SelectListItem>{ new SelectListItem{
                Text="A+",
                Value = "A+"
            }, 
            new SelectListItem{
                Text="A-",
                Value = "A-"
            },
            new SelectListItem{
                Text="B+",
                Value = "B+"
            },
            new SelectListItem{
                Text="B-",
                Value = "B-"
            },
            new SelectListItem{
                Text="O+",
                Value = "O+"
            },
            new SelectListItem{
                Text="O-",
                Value = "O-"
            },
            new SelectListItem{
                Text="AB+",
                Value = "AB+"
            },
            new SelectListItem{
                Text="AB-",
                Value = "AB-"
            }
            };
            //TempData["SavedMessage"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult SubmitData(Order order)
        {
            if (order != null)
            {
                try
                {

                    SqlConnection conn = new SqlConnection("Server=DESKTOP-T33SPTH;Database=BBMSDB;Trusted_Connection=True;");

                    SqlCommand cmd = new SqlCommand("sp_Orders_add", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ReceiverName", order.ReceiverName);
                    cmd.Parameters.AddWithValue("@Age", order.Age);
                    cmd.Parameters.AddWithValue("@LocationAddress", order.LocationAddress);
                    cmd.Parameters.AddWithValue("@BloodBroup", order.BloodBroup);
                    cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                    cmd.Parameters.AddWithValue("@OrderDateTime", order.OrderDateTime);
                    cmd.Parameters.Add("@SavedOrderId", SqlDbType.Int).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    int SavedOrderId = Convert.ToInt32(cmd.Parameters["@SavedOrderId"].Value);
                    conn.Close();
                    TempData["SavedMessage"] = "Saved Order id is" + SavedOrderId.ToString();
                }
                catch(Exception ex) 
                {
                    throw ex;
                }
                return RedirectToAction("Index", "Order");
                //return View();
            }
            else
            {
                // Authentication failed
                ViewBag.Message = "Invalid username or password. Please try again.";
                return View("Index", "Order");
            }
        }
    }    
}
