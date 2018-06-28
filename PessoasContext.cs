using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using testes_webapi.Models;

namespace testes_webapi
{
    public class PessoasContext : DbContext
    {
        public PessoasContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}