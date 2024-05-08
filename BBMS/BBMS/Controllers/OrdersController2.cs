using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBMS.Data;
using BBMS.Models;

namespace BBMS.Controllers
{
    public class OrdersController2 : Controller
    {
        private readonly BBMSContext _context;

        public OrdersController2(BBMSContext context)
        {
            _context = context;
        }

        // GET: OrdersController2
        public async Task<IActionResult> Index()
        {
            string user = (string) TempData["user"];
            ViewBag.Username = user;
            TempData["user"] = user;
            return View(await _context.Orders.ToListAsync());
        }

        // GET: OrdersController2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string user = (string) TempData["user"];
            ViewBag.Username = user;
            TempData["user"] = user;
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: OrdersController2/Create
        public IActionResult Create()
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

            string user = (string)TempData["user"];
            ViewBag.Username = user;
            TempData["user"] = user;
            return View();
        }

        // POST: OrdersController2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Orderid,ReceiverName,Age,LocationAddress,BloodBroup,Quantity,OrderDateTime,OrderStatus")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = order.Orderid });
            }
            string data = (string)TempData["user"];
            ViewBag.User = data;
            TempData["user"] = data;
            return View(order);
        }

        // GET: OrdersController2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string user = (string) TempData["user"];
            ViewBag.Username = user;
            TempData["user"] = user;
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: OrdersController2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Orderid,ReceiverName,Age,LocationAddress,BloodBroup,Quantity,OrderDateTime,OrderStatus")] Order order)
        {
            string user = (string) TempData["user"];
            ViewBag.Username = user;
            TempData["user"] = user;
            if (id != order.Orderid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Orderid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: OrdersController2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            string user = (string) TempData["user"];
            ViewBag.Username = user;
            TempData["user"] = user;
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: OrdersController2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Orderid == id);
        }

        public async Task<IActionResult> Logout()
        {
            TempData["user"] = null;
            ViewBag.Username = null;
            TempData["user"] = null;
            //return RedirectToAction("Welcome", "Login");
            return RedirectToAction("Logout", "Login");
        }
    }
}
