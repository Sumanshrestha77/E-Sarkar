using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Esarkar.Models;

namespace Esarkar
{
    public class RequestController : Controller
    {
        private readonly MeroDbContext _context;

        public RequestController(MeroDbContext context)
        {
            _context = context;
        }

        // GET: Request
        public async Task<IActionResult> Index()
        {
            string userEmail = User.Identity.Name; // Assuming email is stored in the user's identity
            string allowedEmail = "admin@gmail.com"; // The allowed email

            if (string.Equals(userEmail, allowedEmail, StringComparison.OrdinalIgnoreCase))
            {
                return _context.RequestModel != null ?
                          View(await _context.RequestModel.ToListAsync()) :
                          Problem("Entity set 'MeroDbContext.RequestModel'  is null.");
            }
            else
            {
                // Redirect or display an error message for unauthorized access
                return RedirectToAction("AccessDenied");
                //
            }
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }

        // GET: Request/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RequestModel == null)
            {
                return NotFound();
            }

            var requestModel = await _context.RequestModel
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (requestModel == null)
            {
                return NotFound();
            }

            return View(requestModel);
        }

        // GET: Request/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Request/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,Email,Phone,Document,Reason")] RequestModel requestModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(requestModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success", "Request");
            }
            return View(requestModel);
        }

        // GET: Request/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RequestModel == null)
            {
                return NotFound();
            }

            var requestModel = await _context.RequestModel.FindAsync(id);
            if (requestModel == null)
            {
                return NotFound();
            }
            return View(requestModel);
        }

        // POST: Request/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,Email,Phone,Document,Reason")] RequestModel requestModel)
        {
            if (id != requestModel.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(requestModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestModelExists(requestModel.RequestId))
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
            return View(requestModel);
        }

        // GET: Request/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RequestModel == null)
            {
                return NotFound();
            }

            var requestModel = await _context.RequestModel
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (requestModel == null)
            {
                return NotFound();
            }

            return View(requestModel);
        }

        // POST: Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RequestModel == null)
            {
                return Problem("Entity set 'MeroDbContext.RequestModel'  is null.");
            }
            var requestModel = await _context.RequestModel.FindAsync(id);
            if (requestModel != null)
            {
                _context.RequestModel.Remove(requestModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestModelExists(int id)
        {
          return (_context.RequestModel?.Any(e => e.RequestId == id)).GetValueOrDefault();
        }
    }
}
