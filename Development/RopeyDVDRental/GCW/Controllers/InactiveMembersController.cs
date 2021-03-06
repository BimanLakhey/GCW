using GCW.Areas.Identity.Data;
using GCW.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GCW.Controllers
{
    [Authorize(Roles = "assistant,manager")]
    public class InactiveMembersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public InactiveMembersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: InactiveMembersController
        public ActionResult Index()
        {
            List<DateTime> lastDates = new List<DateTime>();
            List<DVDTitle> dvdTitles = _context.DVDTitle.ToList();
            List<DVDCopy> dvdCopies = _context.DVDCopy.ToList();
            List<Loan> loans = _context.Loan.ToList();
            List<Member> members = _context.Member.ToList();

            //for(int i = 0; i<=members.Count(); i++)
            //{
            //    var inactiveMembers = from dT in dvdTitles
            //                          join dC in dvdCopies on dT.DVDNumber equals dC.DVDNumber into table1
            //                          from dC in table1
            //                          join l in loans on dC.CopyNumber equals l.CopyNumber into table2
            //                          from l in table2
            //                          join m in members on l.MemberNumber equals m.MemberNumber into table3
            //                          from m in table3
            //                          orderby l.DateOut descending
            //                          where m.MemberNumber == members[i].MemberNumber
            //                          select l.DateOut;
            //    lastDates.Add(DateTime.Parse(inactiveMembers.ToString()));

            //}
            //Console.WriteLine(lastDates);

            var activeMembersList = (from dT in dvdTitles
                                     join dC in dvdCopies on dT.DVDNumber equals dC.DVDNumber into table1
                                     from dC in table1
                                     join l in loans on dC.CopyNumber equals l.CopyNumber into table2
                                     from l in table2
                                     join m in members on l.MemberNumber equals m.MemberNumber into table3
                                     from m in table3
                                     where l.DateOut >= DateTime.Now.AddDays(-31)
                                     select m);

            var inactiveMembersList = members.Except(activeMembersList);

            var result = (from dT in dvdTitles
                          join dC in dvdCopies on dT.DVDNumber equals dC.DVDNumber into table1
                          from dC in table1
                          join l in loans on dC.CopyNumber equals l.CopyNumber into table2
                          from l in table2
                          join m in inactiveMembersList on l.MemberNumber equals m.MemberNumber into table3
                          from m in table3
                          select new InactiveMembersViewModel
                          {
                              dvdTitle = dT,
                              dvdCopy = dC,
                              loan = l,
                              member = m,
                          }).DistinctBy(row => row.member.MemberNumber);

            return View(result);
        }

        // GET: InactiveMembersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: InactiveMembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InactiveMembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InactiveMembersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: InactiveMembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InactiveMembersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: InactiveMembersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
