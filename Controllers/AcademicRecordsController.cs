using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab6.Models.DataAccess;

namespace Lab6.Controllers
{
    public class AcademicRecordsController : Controller
    {
        private readonly StudentRecordContext _context;

        public AcademicRecordsController(StudentRecordContext context)
        {
            _context = context;
        }

        // GET: AcademicRecords


        public async Task<IActionResult> Index(string sort)
        {

            var studentRecords = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);


            if (sort != null)
            {
                HttpContext.Session.SetString("sort", sort);
            }
            else if (HttpContext.Session.GetString("sort") != null)
            {
                sort = HttpContext.Session.GetString("sort");
            }

            if (sort == "course")
            {
                //studentRecords = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<AcademicRecord, Student>)studentRecords.OrderBy(s => s.CourseCodeNavigation.Title);

            }
            else if (sort == "student")
            {
                //studentRecords = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<AcademicRecord, Student>)studentRecords.OrderBy(s => s.Student.Name).ToList();
            }

            return View(await studentRecords.ToListAsync());

        }


        public async Task<IActionResult> EditAll()
        {
            var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);
            return View(await studentRecordContext.ToListAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAll(List<AcademicRecord> myrecords)
        {
            
           

            if (ModelState.IsValid)
            {
                foreach (var re in myrecords)
                {
                    _context.Update(re);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            var studentRecordContext = _context.AcademicRecords.Include(a => a.CourseCodeNavigation).Include(a => a.Student);
           

            foreach (var re in myrecords)
            {
                _context.Update(re);

            }
            return View(await studentRecordContext.ToListAsync());



        }

        private bool AcademicRecordExists(object studentId)
        {
            throw new NotImplementedException();
        }



        // GET: AcademicRecords/Create
        public IActionResult Create()
        {
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: AcademicRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        // GET: AcademicRecords/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.AcademicRecords == null)
            {
                return NotFound();
            }

            var academicRecord = await _context.AcademicRecords.FindAsync(id);
            if (academicRecord == null)
            {
                return NotFound();
            }
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }

        // POST: AcademicRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CourseCode,StudentId,Grade")] AcademicRecord academicRecord)
        {
            if (id != academicRecord.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(academicRecord);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicRecordExists(academicRecord.StudentId))
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
            ViewData["CourseCode"] = new SelectList(_context.Courses, "Code", "Code", academicRecord.CourseCode);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", academicRecord.StudentId);
            return View(academicRecord);
        }



        private bool AcademicRecordExists(string id)
        {
            return _context.AcademicRecords.Any(e => e.StudentId == id);
        }
    }
}
