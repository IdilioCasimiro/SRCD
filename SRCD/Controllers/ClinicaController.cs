using SRCD.Models;
using SRCD.Models.Repositorio;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SRCD.Controllers
{
    public class ClinicaController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        private ClinicaRepositorio clinicaRepositorio = new ClinicaRepositorio(new ApplicationDbContext());

        // GET: Clinica
        public async Task<ActionResult> Index()
        {
            var clinicas = await _context.Clinicas.Select(
                c => new ClinicaViewModel()
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
            return View(clinicas);
        }

        // GET: Clinica/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var clinica = await _context.Clinicas.Select(c => new ClinicaViewModel()
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

            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // GET: Clinica/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clinica/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nome,Email,Telefone1,Telefone2,Morada,Latitude,Longitude,Imagem1,Imagem2,Imagem1ContentType,Imagem2ContentType")] ClinicaViewModel model)
        {
            if (ModelState.IsValid)
            {
                Clinica clinica = new Clinica()
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
                    clinica.Imagem1 = imagem1;
                    clinica.Imagem1ContentType = imagem1ContentType;
                }

                if (model.Imagem2 != null)
                {
                    byte[] imagem2 = new byte[model.Imagem2.ContentLength];
                    await model.Imagem2.InputStream.ReadAsync(imagem2, 0, imagem2.Length);
                    string imagem2ContentType = model.Imagem2.ContentType;
                    clinica.Imagem2 = imagem2;
                    clinica.Imagem2ContentType = imagem2ContentType;
                }

                _context.Clinicas.Add(clinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Clinica/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = await _context.Clinicas.FindAsync(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Clinica/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nome,Email,Telefone1,Telefone2,Morada,Latitude,Longitude,Imagem1,Imagem2,Imagem1ContentType,Imagem2ContentType")] Clinica clinica)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(clinica).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(clinica);
        }

        // GET: Clinica/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clinica clinica = await _context.Clinicas.FindAsync(id);
            if (clinica == null)
            {
                return HttpNotFound();
            }
            return View(clinica);
        }

        // POST: Clinica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Clinica clinica = await _context.Clinicas.FindAsync(id);
            _context.Clinicas.Remove(clinica);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
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
            Clinica clinica = await clinicaRepositorio.FindByIdAsync(id);

            if (clinica != null && clinica.Imagem1 != null)
            {
                return File(clinica.Imagem1, clinica.Imagem1ContentType);
            }
            else
            {
                return null;
            }
        }

        public async Task<FileContentResult> ObterImagen2(int id)
        {
            Clinica clinica = await clinicaRepositorio.FindByIdAsync(id);

            if (clinica != null && clinica.Imagem2 != null)
            {
                return File(clinica.Imagem2, clinica.Imagem2ContentType);
            }
            else
            {
                return null;
            }
        }

        public async Task<JsonResult> ObterClinicas()
        {
            return Json(await clinicaRepositorio.Find(c => c.Latitude != null && c.Longitude != null).ToListAsync());
        }

        public async Task<ActionResult> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var model = await clinicaRepositorio.Find(c => c.Nome.ToLower().Contains(name.ToLower())).Select(c => new ClinicaViewModel()
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

            var clinicas = await _context.Clinicas.Select(
    c => new ClinicaViewModel()
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

            return View("Index", clinicas);
        }

        public async Task<ActionResult> EnviarEmail(MailMessageModel model)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("idvaniocasimiro@gmail.com", "Id!li0cas");
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(model.Email);
                mailMessage.To.Add(model.Destinatario);
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = "<p><strong>Nome do cliente: </strong>" + model.Nome + "</p>"
                    + "<p><strong>Email do cliente: </strong>" + model.Email + "</p>"
                    + "<p><strong>Nº de animais: </strong>" + model.NAnimais + "</p>"
                    + "<p><strong>Data da consulta: </strong>" + model.Data.Day + "/" + model.Data.Month + "/" + model.Data.Year + "</p>"
                    + "<p><strong>Mensagem: </strong>" + model.Mensagem + "</p>";
                mailMessage.Subject = "SRCD - Pedido de marcação de consulta";
                await client.SendMailAsync(mailMessage);

                TempData["StatusMessage"] = "Email enviado!";
                return RedirectToAction("Details", new { Id = model.ClinicId });
            }
            catch (Exception)
            {
                TempData["StatusMessage"] = "Ocorreu um erro ao enviar o email. Preencha todos os campos e verifique a sua conexão a internet, por favor.";
                return RedirectToAction("Details", new { Id = model.ClinicId });
            }
        }
    }
}
