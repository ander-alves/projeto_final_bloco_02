using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using projeto_final_bloco_02.Model;
using projeto_final_bloco_02.Service;

namespace projeto_final_bloco_02.Controllers
{
    [Route("~/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IValidator<Produto> _produtoValidator;

        public ProdutoController(IProdutoService produtoService, IValidator<Produto> produtoValidator)
        {
            _produtoService = produtoService;
            _produtoValidator = produtoValidator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            return Ok(await _produtoService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(long id)
        {
            var response = await _produtoService.GetById(id);

            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<Produto>> GetByTitle(string nome)
        {
            return Ok(await _produtoService.GetByName(nome));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Produto produto)
        {
            var validationResult = await _produtoValidator.ValidateAsync(produto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult);
            }

            var response = await _produtoService.Create(produto);
            if (response is null)
            {
                return BadRequest("Produto não encontrado");
            }

            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Produto produto)
        {
            if (produto.Id == 0)
                return BadRequest("ID do Produto Invavalido");

            var validarProduto = await _produtoValidator.ValidateAsync(produto);

            if (!validarProduto.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, validarProduto);

            var resposta = await _produtoService.Update(produto);

            if (resposta is null)
                return NotFound("Produto ou Categoria nao encontrados");

            return Ok(resposta);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _produtoService.GetById(id);
            if (product is null)
            {
                return NotFound("Produto não encontrado");
            }

            await _produtoService.Delete(product);

            return NoContent();
        }

    }

}