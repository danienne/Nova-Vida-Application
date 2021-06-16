using Microsoft.EntityFrameworkCore;


namespace NovaVidaApplication.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Professor> Professores{ get;set; }
        public DbSet<Aluno> Alunos { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes) {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConvertingConfiguration.Config(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ConvertingConfiguration {

        public static void Config(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Aluno>(new AlunoMapping());
        }
    }
}
