using Desafio.TerraMedia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.TerraMedia.ORM.Mapping
{
    public class ComentarioConfiguration : IEntityTypeConfiguration<Comentario>
    {
        public void Configure(EntityTypeBuilder<Comentario> builder)
        {
            builder.ToTable("Comentarios");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.UserId).IsRequired().HasColumnType("uuid");
            builder.Property(c => c.LivroId).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Texto).IsRequired().HasMaxLength(500);
            builder.Property(c => c.DataComentario).IsRequired();
        }
    }
}
