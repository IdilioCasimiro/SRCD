using SRCD.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace SRCD.Controllers
{
    public class DrogariaController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private DrogariaRepositorio drogariaRepositorio = new DrogariaRepositorio(new ApplicationDbContext());

        // GET: Drogaria
        public async Task<ActionResult> Index()
        {
            var drogarias = await _context.Drogarias.Select(
                c => new DrogariaViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Morada = c.Morada,
                    Email = c.Email,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Telefone1 = c.Telefone1,
                    Telefone2 = c.Telefone2
            }).ToListAsync();
            return View(drogarias);
        }

        // GET: Drogaria/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var drogaria = await _context.Drogarias.Select(c => new DrogariaViewModel()
            {
                Id = c.Id,
                Nome = c.Nome,
                Morada = c.Morada,
                Email = c.Email,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                Telefone1 = c.Telefone1,
                Telefone2 = c.Telefone2
            }).FirstOrDefaultAsync(m => m.Id == id);
            if (drogaria == null)
            {
                return HttpNotFound();
            }
            return View(drogaria);
        }

        // GET: Drogaria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drogaria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Email,Telefone1,Telefone2,Morada,Latitude,Longitude,Imagem1,Imagem2,Imagem1ContentType,Imagem2ContentType")] DrogariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Drogaria drogaria = new Drogaria()
                {
                    Nome = model.Nome,
                    Morada = model.Morada,
                    Email = model.Email,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Telefone1 = model.Telefone1,
                    Telefone2 = model.Telefone2
                };

                if (model.Imagem1 != null)
                {
                    byte[] imagem1 = new byte[model.Imagem1.ContentLength];
                    await model.Imagem1.InputStream.ReadAsync(imagem1, 0, imagem1.Length);
                    string imagem1ContentType = model.Imagem1.ContentType;
                    drogaria.Imagem1 = imagem1;
                    drogaria.Imagem1ContentType = imagem1ContentType;
                }

                if (model.Imagem2 != null)
                {
                    byte[] imagem2 = new byte[model.Imagem2.ContentLength];
                    await model.Imagem2.InputStream.ReadAsync(imagem2, 0, imagem2.Length);
                    string imagem2ContentType = model.Imagem2.ContentType;
                    drogaria.Imagem2 = imagem2;
                    drogaria.Imagem2ContentType = imagem2ContentType;
                }

                _context.Drogarias.Add(drogaria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Drogaria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var drogaria = await drogariaRepositorio.FindByIdAsync(id.Value);
            if (drogaria == null)
            {
                return HttpNotFound();
            }

            var model = new DrogariaViewModel()
            {
                Id = drogaria.Id,
                Nome = drogaria.Nome,
                Morada = drogaria.Morada,
                Email = drogaria.Email,
                Latitude = drogaria.Latitude,
                Longitude = drogaria.Longitude,
                Telefone1 = drogaria.Telefone1,
                Telefone2 = drogaria.Telefone2,
            };
            if (drogaria == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Drogaria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Email,Telefone1,Telefone2,Morada,Latitude,Longitude,Imagem1,Imagem2,Imagem1ContentType,Imagem2ContentType")] DrogariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Drogaria drogaria = new Drogaria()
                    {
                        Id = model.Id,
                        Nome = model.Nome,
                        Morada = model.Morada,
                        Email = model.Email,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude,
                        Telefone1 = model.Telefone1,
                        Telefone2 = model.Telefone2,
                    };

                    if (model.Imagem1 != null)
                    {
                        drogaria.Imagem1 = new byte[model.Imagem1.ContentLength];
                        await model.Imagem1.InputStream.ReadAsync(drogaria.Imagem1, 0, drogaria.Imagem1.Length);
                        drogaria.Imagem1ContentType = model.Imagem1.ContentType;
                    }

                    if (model.Imagem2 != null)
                    {
                        drogaria.Imagem2 = new byte[model.Imagem2.ContentLength];
                        await model.Imagem2.InputStream.ReadAsync(drogaria.Imagem2, 0, drogaria.Imagem2.Length);
                        drogaria.Imagem2ContentType = model.Imagem2.ContentType;
                    }

                    await drogariaRepositorio.UpdateAsync(drogaria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrogariaExists(model.Id))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private bool DrogariaExists(int id)
        {
            return _context.Drogarias.Any(e => e.Id == id);
        }

        // GET: Drogaria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drogaria drogaria = await _context.Drogarias.FindAsync(id);
            if (drogaria == null)
            {
                return HttpNotFound();
            }
            return View(drogaria);
        }

        // POST: Drogaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var drogarias = await _context.Drogarias.FindAsync(id);
            _context.Drogarias.Remove(drogarias);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<FileContentResult> ObterImagen1(int id)
        {
            Drogaria drogaria = await drogariaRepositorio.FindByIdAsync(id);

            if (drogaria != null && drogaria.Imagem1 != null)
            {
                return File(drogaria.Imagem1, drogaria.Imagem1ContentType);
            }
            else
            {
                return null;
            }
        }

        public async Task<FileContentResult> ObterImagen2(int id)
        {
            Drogaria drogaria = await drogariaRepositorio.FindByIdAsync(id);

            if (drogaria != null && drogaria.Imagem2 != null)
            {
                return File(drogaria.Imagem2, drogaria.Imagem2ContentType);
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> ObterDrogarias()
        {
            return Json(await drogariaRepositorio.Find(c => c.Latitude != null && c.Longitude != null).ToListAsync(), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Search(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var model = await drogariaRepositorio.Find(c => c.Nome.ToLower().Contains(name.ToLower())).Select(c => new DrogariaViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Morada = c.Morada,
                    Email = c.Email,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Telefone1 = c.Telefone1,
                    Telefone2 = c.Telefone2
                }).ToListAsync();
                return View("Index", model);
            }

            var drogarias = await _context.Drogarias.Select(
                c => new DrogariaViewModel()
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Morada = c.Morada,
                    Email = c.Email,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    Telefone1 = c.Telefone1,
                    Telefone2 = c.Telefone2
                }).ToListAsync();

            return View("Index", drogarias);
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            string culture = "pt-PT";
            CultureInfo ci = CultureInfo.GetCultureInfo(culture);

            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
