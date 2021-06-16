using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NovaVidaApplication.Models;

namespace NovaVidaApplication.Controllers
{
    public class AlunosController : Controller
    {
        private readonly Contexto _contexto;

        public AlunosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                return View(await _contexto.Alunos.Where(x => x.ProfessorId == id).Include(x => x.Professor).ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarAluno(int? id)
        {
            try
            {
                if (Request.Form.Files.Count() > 0 && id != null)
                {
                    byte[] bytes = await GetByteArrayFromImageAsync(Request.Form.Files[0]);
                    Professor professor = _contexto.Professores.Find(id);
                    List<Aluno> alunos = CarregarAlunos(bytes, professor);
                    foreach (Aluno aluno in alunos)
                    {
                        _contexto.Alunos.Add(aluno);
                        await _contexto.SaveChangesAsync();
                    }
                    return Json(new { isValid = true });
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Json(new { isValid = false, errorMessage = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletarAluno(int? id)
        {
            if (id != null)
            {
                Aluno aluno = _contexto.Alunos.Find(id);
                int ProfessorId = aluno.ProfessorId;
                _contexto.Alunos.Remove(aluno);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = ProfessorId });
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<byte[]> GetByteArrayFromImageAsync(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                await file.CopyToAsync(target);
                return target.ToArray();
            }
        }

        private static List<Aluno> CarregarAlunos(byte[] bytes, Professor professor)
        {
            List<Aluno> valuesCollection = new List<Aluno>(); ;
            using (MemoryStream memory = new MemoryStream(bytes))
            {
                using (var f = new StreamReader(memory))
                {
                    string line = string.Empty;
                    while ((line = f.ReadLine()) != null)
                    {
                        var parts = line.Split("||");
                        Aluno aluno = new Aluno()
                        {
                            Nome = parts[0],
                            Mensalidade = Convert.ToDecimal(parts[1]),
                            DataVencimentoMensalidade = Convert.ToDateTime(parts[2]),
                            ProfessorId = professor.ProfessorId,
                            Professor = professor
                        };

                        valuesCollection.Add(aluno);
                    }
                }
            }
            return valuesCollection;
        }
    }
}
