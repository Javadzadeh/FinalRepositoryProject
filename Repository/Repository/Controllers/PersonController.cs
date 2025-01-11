using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Models.DomainModels;
using Repository.Models.Framworks.Contract;
using System;

namespace Repository.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonRepository _personRepository;

        #region - ctor -
        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        #endregion


        #region - Index() - 
        public async Task<IActionResult> Index()
        {
            return View(await _personRepository.Select());
        }
        #endregion



        #region -  Create() - 
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email")] Person person)
        {
            if (ModelState.IsValid)
            {
                _personRepository.Insert(person);
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }
        #endregion

        #region - Edit() -
        // GET: People/Edit/5
        public async Task<IActionResult> Edit(Person p)
        {
            if (p.Id == null)
            {
                return NotFound();
            }

            //var person = await _personRepository.Person.FindAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            return View(p);
        }
        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,Email")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _personRepository.Edit(person);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ((person.Id) == null)
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
            return View(person);
        }
        #endregion

        #region - Delete() -
        // GET: People/Delete/5
        public async Task<IActionResult> Delete(Person person)
        {
            if (person.Id == null)
            {
                return NotFound();
            }
           // return View(await _personRepository.Details(Guid? Id));
            //var p = await _personRepository.Delete.
            //    FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Person person)
        {
            //var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _personRepository.Delete(person);
            }

            //await _personRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        #endregion

        #region - Details-
        // GET: People/Details/5
        public async Task<IActionResult> Details (Guid? Id)
        {
            return View(await _personRepository.Details(Id));
            
        }
        #endregion


    }
}
