using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        {

            try
            {
                //return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();   *sem filtro
                return _context.Categorias.Include(c => c.Produtos).Where(c => c.CategoriaId <= 5).ToList(); //com filtro
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno");
            }

        }


        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _context.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno");
            }


        }

        [HttpPost("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {

            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria de id= {id} não encontrada...");
                }

                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno");
            }


        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria == null)
                {
                    return BadRequest();
                }
                _context.Categorias.Add(categoria);
                _context.SaveChanges();

                return new CreatedAtRouteResult("Obter Categoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno...");
            }
            
            

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest();
                }

                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno...");
            }

            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound("Categoria não encontrada...");
                }

                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno...");
            }



        }

    }
}
