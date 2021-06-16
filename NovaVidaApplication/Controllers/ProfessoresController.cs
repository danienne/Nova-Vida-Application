using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovaVidaApplication.Models;

namespace NovaVidaApplication.Controllers
{

    public class ProfessoresController : Controller
    {
        private readonly Contexto _contexto;

        public ProfessoresController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Professores.ToListAsync());
        }

        [HttpGet]
        public IActionResult CriarProfessor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarProfessor(Professor professor)
        {
            if (ModelState.IsValid)
            {
                _contexto.Professores.Add(professor);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

  
        [HttpGet]
        public IActionResult AtualizarProfessor(int? id)
        {
            if (id != null)
            {
                Professor professor = _contexto.Professores.Find(id);
                return View(professor);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public async Task<IActionResult> AtualizarProfessor(int? id, Professor professor)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Professores.Update(professor);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(professor);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
