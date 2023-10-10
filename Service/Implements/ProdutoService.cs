using Microsoft.EntityFrameworkCore;
using projeto_final_bloco_02.Data;
using projeto_final_bloco_02.Model;

namespace projeto_final_bloco_02.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetById(long id)
        {
            try
            {
                var produto = await _context.Produtos
                    .FirstOrDefaultAsync(p => p.Id == id);

                return produto;
            }
            catch
            {
                return null;
            }
        }


        public async Task<IEnumerable<Produto>> GetByName(string nome)
        {
            var produtosComNome = await _context.Produtos
                 .Where(p => p.Nome.Contains(nome))
                 .ToListAsync();

            return produtosComNome;
        }


        public async Task<Produto?> Create(Produto produto)
        {

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<Produto> Update(Produto produto)
        {
            var produtoUpdate = await _context.Produtos.FindAsync(produto.Id);

            if (produtoUpdate is null)
            {
                return null;
            }
            _context.Entry(produtoUpdate).State = EntityState.Detached;
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
