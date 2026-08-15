using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace StudentPortal.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection: ASP.NET Core hands us a configured
        // ApplicationDbContext automatically — we never "new" it up ourselves.
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /Student
        public async Task<IActionResult> Index()
        {
            // Include() eager-loads the related Course so student.Course.Title
            // is populated in the view, not null.
            var students = await _context.Students.Include(s => s.Course).ToListAsync();
            return View(students);
        }

        // GET /Student/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var student = await _context.Students.Include(s => s.Course)
                                                   .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            return View(student);
        }

        // GET /Student/Create
        public IActionResult Create()
        {
            // Populate the Course dropdown
            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }

        // POST /Student/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Title", student.CourseId);
                return View(student);
            }

            _context.Students.Add(student);
            await _context.SaveChangesAsync(); // this is the actual INSERT
            return RedirectToAction(nameof(Index));
        }

        // GET /Student/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Title", student.CourseId);
            return View(student);
        }

        // POST /Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.CourseId = new SelectList(_context.Courses, "Id", "Title", student.CourseId);
                return View(student);
            }

            _context.Students.Update(student); // this is the actual UPDATE
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET /Student/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.Include(s => s.Course)
                                                   .FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) return NotFound();
            return View(student);
        }

        // POST /Student/Delete/5 — note the [ActionName] trick: the form posts
        // to "Delete" but calls this differently-named method to avoid a
        // method-signature clash with the GET Delete(int id) above.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student); // this is the actual DELETE
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}