using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace NovaVidaApplication.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Nome { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? Mensalidade { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataVencimentoMensalidade { get; set; }

        public int ProfessorId { get; set; }
        public virtual Professor Professor { get; set; }
    }

    public class AlunoMapping : IEntityTypeConfiguration<Aluno> {
        
        public void Configure(EntityTypeBuilder<Aluno> builder) {

            builder.Property(x => x.Nome).HasMaxLength(350).IsRequired();
            builder.Property(x => x.Mensalidade).HasPrecision(30, 20);
        }
    }
}
